using System;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_c5707b79_859b_4d53_92e0_cbed53aae648
{
    public class LoadFont : Instance<LoadFont>
    {
        // Inputs
        [Input(Guid = "F2DD87B1-7F37-4B02-871B-B2E35972F246")]
        public readonly InputSlot<string> Text = new InputSlot<string>();

        [Input(Guid = "E827FDD1-20CA-473C-99EE-B839563690E9")]
        public readonly InputSlot<string> Filepath = new InputSlot<string>();

        // [Input(Guid = "7259DB77-CC7E-4BE4-8CB4-2D6DAB54EB31")]
        // public readonly InputSlot<bool> TriggerUpdate = new InputSlot<bool>();

        [Input(Guid = "1CDE902D-5EAA-4144-B579-85F54717356B")]
        public readonly InputSlot<Vector4> Color = new InputSlot<Vector4>();

        [Input(Guid = "43A65A57-35B1-4816-8F1D-8A192F908603")]
        public readonly InputSlot<Vector4> Shadow = new InputSlot<Vector4>();

        [Input(Guid = "E05E143E-8D4C-4DE7-8C9C-7FA7755009D3")]
        public readonly InputSlot<float> Spacing = new InputSlot<float>();

        [Input(Guid = "9EB4E13F-0FE3-4ED9-9DF1-814F075A05DA")]
        public readonly InputSlot<float> LineHeight = new InputSlot<float>();

        [Input(Guid = "FFD2233A-8F3E-426B-815B-8071E4C779AB")]
        public readonly InputSlot<float> Slant = new InputSlot<float>();

        [Input(Guid = "14829EAC-BA59-4D31-90DC-53C7FC56CC30")]
        public readonly InputSlot<int> VerticalAlign = new InputSlot<int>();

        [Input(Guid = "E43BC887-D425-4F9C-8A86-A32C761DE0CC")]
        public readonly InputSlot<int> HorizontalAlign = new InputSlot<int>();

        [Output(Guid = "973aebfa-e15d-4943-a9b8-91e6702329d0")]
        public readonly Slot<string> Result = new Slot<string>();

        public LoadFont()
        {
            Result.UpdateAction = Update;
        }

        private BmFont.Font _font = null;
        private bool _openedOnce = false;

        private void Update(EvaluationContext context)
        {
            //var triggerUpdate = TriggerUpdate.GetValue(context);

            if (!_openedOnce && (Filepath.DirtyFlag.IsDirty || _font == null))
            {
                var filepath = Filepath.GetValue(context);
                Log.Debug(File.ReadAllText(filepath));

                XmlSerializer serializer = new XmlSerializer(typeof(BmFont.Font));
                _openedOnce = true;
                try
                {
                    var stream = new FileStream(filepath, FileMode.Open);
                    _font = (BmFont.Font)serializer.Deserialize(stream);
                    Log.Debug("loaded font with character count:" + _font.Chars.Length);
                    stream.Close();
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to load font {filepath} " + e + "\n" + e.Message);
                }
            }

            UpdateMesh(context);
        }

        private string _text;
        private float _characterSpacing;
        private float _lineHeight;
        private float _slant;
        private int _horizontalAlign;
        private int _verticalAlign;

        private float _lastWidth;

        private void UpdateMesh(EvaluationContext context)
        {
            var somethingChanged = false;

            if (Text.DirtyFlag.IsDirty)
            {
                _text = Text.GetValue(context);
                if (_text == "")
                    _text = " "; // prevent crash error

                somethingChanged = true;
            }

            if (VerticalAlign.DirtyFlag.IsDirty)
            {
                _horizontalAlign = (int)HorizontalAlign.GetValue(context);
                somethingChanged = true;
            }

            if (VerticalAlign.DirtyFlag.IsDirty)
            {
                _verticalAlign = (int)VerticalAlign.GetValue(context);
                somethingChanged = true;
            }

            if (Spacing.DirtyFlag.IsDirty)
            {
                _characterSpacing = Spacing.GetValue(context);
                somethingChanged = true;
            }

            if (Slant.DirtyFlag.IsDirty)
            {
                _slant = Slant.GetValue(context);
                somethingChanged = true;
            }

            if (LineHeight.DirtyFlag.IsDirty)
            {
                _lineHeight = LineHeight.GetValue(context);
                somethingChanged = true;
            }

            if (!somethingChanged)
            {
                return;
            }

            var numCharactersInText = _text.Length;
            var numLinesInText = _text.Split('\n').Length;

            var normal = new Vector3(0.0f, 0.0f, -1.0f);
            var color = Color.GetValue(context);
            var tangent = new Vector3(1.0f, 0.0f, 0.0f);
            var binormal = new Vector3(0.0f, -1.0f, 0.0f);
            
            var fontFile = _font;
            float textureWidth = fontFile.Common.ScaleW;
            float textureHeight = fontFile.Common.ScaleH;
            float cursorX = 0;
            float cursorY = 0;

            switch (_verticalAlign)
            {
                case 1:
                    cursorY = fontFile.Common.LineHeight * _lineHeight * (numLinesInText - 2f) / 2;
                    break;
                case 2:
                    cursorY = fontFile.Common.LineHeight * _lineHeight * numLinesInText;
                    break;
            }

            const float scale = 0.01f;
            var maxWidth = float.NegativeInfinity;
            var lineWidth = float.NaN;

            for (int charIndex = 0; charIndex < _text.Length; ++charIndex)
            {
                // Compute line width for horizontal Alignment                    
                if (float.IsNaN(lineWidth))
                {
                    var lookAheadIndex = 0;
                    lineWidth = 0;

                    while (charIndex + lookAheadIndex < _text.Length)
                    {
                        var charForWidth = _text[charIndex + lookAheadIndex];
                        lookAheadIndex++;
                        if (charForWidth == '\n' || charForWidth == '\r')
                            break;

                        var charInfo2 = fontFile.Chars.SingleOrDefault(c => c.Id == (int)charForWidth);

                        if (charInfo2 == null)
                        {
                            continue;
                        }
                        var kerning2 = 0.0f;

                        if (charIndex + lookAheadIndex < _text.Length - 1 && _text[charIndex + lookAheadIndex + 1] != '\n')
                        {
                            var nextCharInfo2 = (from @char in fontFile.Chars
                                                 where @char.Id == (int)_text[charIndex + lookAheadIndex + 1]
                                                 select @char).SingleOrDefault();
                            if (nextCharInfo2 != null)
                            {
                                var kerningInfo2 = (from d in fontFile.Kernings
                                                    where d.First == charInfo2.Id
                                                    where d.Second == nextCharInfo2.Id
                                                    select d).SingleOrDefault();
                                if (kerningInfo2 != null)
                                {
                                    kerning2 = kerningInfo2.Amount;
                                }
                            }
                        }

                        lineWidth += kerning2;
                        lineWidth += charInfo2.XAdvance;
                        lineWidth += _characterSpacing;
                    }

                    switch (_horizontalAlign)
                    {
                        case 1:
                            cursorX -= lineWidth / 2 - _characterSpacing / 2;
                            break;
                        case 2:
                            cursorX -= lineWidth;
                            break;
                    }

                    if (lineWidth > maxWidth)
                    {
                        maxWidth = lineWidth;
                    }
                }

                var charToDraw = _text[charIndex];
                if (charToDraw == '\n')
                {
                    cursorY -= fontFile.Common.LineHeight * _lineHeight;
                    cursorX = 0;
                    lineWidth = float.NaN;
                    continue;
                }

                var charInfo = (from @char in fontFile.Chars
                                where @char.Id == (int)charToDraw
                                select @char).SingleOrDefault();
                if (charInfo == null)
                {
                    continue;
                }

                // float uLeft = charInfo.X/textureWidth;
                // float vTop = charInfo.Y/textureHeight;
                // float uRight = (charInfo.X + charInfo.Width)/textureWidth;
                // float vBottom = (charInfo.Y + charInfo.Height)/textureHeight;

                var sizeWidth = charInfo.Width * scale;
                var sizeHeight = charInfo.Height * scale;
                var x = cursorX + charInfo.XOffset;
                var y = cursorY + charInfo.YOffset;

                Log.Debug($"{charToDraw} => {sizeHeight:0.00}x{sizeHeight:0.00}  @ {x:0.000}  {y:0.000}");
                
                var kerning = 0.0f;
                if (charIndex < _text.Length - 1)
                {
                    var kerningInfo = fontFile.Kernings.SingleOrDefault(d => d.First == charInfo.Id && d.Second == (int)_text[charIndex + 1]);
                    kerning = kerningInfo?.Amount ?? 0;
                }

                cursorX += kerning;
                cursorX += charInfo.XAdvance;
                cursorX += _characterSpacing;
                // numTriangles += 2;
            }

            _lastWidth = maxWidth;
        }
        
        
        private BufferLayout[] _bufferContent;

        // Must be multiple of 16
        [StructLayout(LayoutKind.Explicit, Size = 32)]
        public struct BufferLayout
        {
            public BufferLayout(Vector2 pos, Vector2 uv, float highlight)
            {
                Pos = pos;
                Uv = uv;
                Highlight = highlight;
            }

            [FieldOffset(0)]
            public Vector2 Pos;
            
            [FieldOffset(2*4)]
            public Vector2 Uv;
            
            [FieldOffset(4*4)]
            public float Highlight;
        }       
        
    }
}


