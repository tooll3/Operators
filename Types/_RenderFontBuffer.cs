using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using T3.Core;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
using T3.Operators.Utils.BmFont;

namespace T3.Operators.Types.Id_c5707b79_859b_4d53_92e0_cbed53aae648
{
    public class _RenderFontBuffer : Instance<_RenderFontBuffer>
    {
        // Inputs ----------------------------------------------------
        [Input(Guid = "F2DD87B1-7F37-4B02-871B-B2E35972F246")]
        public readonly InputSlot<string> Text = new InputSlot<string>();

        [Input(Guid = "E827FDD1-20CA-473C-99EE-B839563690E9")]
        public readonly InputSlot<string> Filepath = new InputSlot<string>();

        // [Input(Guid = "7259DB77-CC7E-4BE4-8CB4-2D6DAB54EB31")]
        // public readonly InputSlot<bool> TriggerUpdate = new InputSlot<bool>();

        [Input(Guid = "1CDE902D-5EAA-4144-B579-85F54717356B")]
        public readonly InputSlot<Vector4> Color = new InputSlot<Vector4>();

        // [Input(Guid = "43A65A57-35B1-4816-8F1D-8A192F908603")]
        // public readonly InputSlot<Vector4> Shadow = new InputSlot<Vector4>();

        [Input(Guid = "5008E9B4-083A-4494-8F7C-50FE5D80FC35")]
        public readonly InputSlot<float> Size = new InputSlot<float>();

        [Input(Guid = "E05E143E-8D4C-4DE7-8C9C-7FA7755009D3")]
        public readonly InputSlot<float> Spacing = new InputSlot<float>();

        [Input(Guid = "9EB4E13F-0FE3-4ED9-9DF1-814F075A05DA")]
        public readonly InputSlot<float> LineHeight = new InputSlot<float>();

        [Input(Guid = "C4F03392-FF7E-4B4A-8740-F93A581B2B6B")]
        public readonly InputSlot<Vector2> Position = new InputSlot<Vector2>();
        
        [Input(Guid = "FFD2233A-8F3E-426B-815B-8071E4C779AB")]
        public readonly InputSlot<float> Slant = new InputSlot<float>();

        [Input(Guid = "14829EAC-BA59-4D31-90DC-53C7FC56CC30")]
        public readonly InputSlot<int> VerticalAlign = new InputSlot<int>();

        [Input(Guid = "E43BC887-D425-4F9C-8A86-A32C761DE0CC")]
        public readonly InputSlot<int> HorizontalAlign = new InputSlot<int>();


        
        // Outputs ---------------------------------------------------------

        [Output(Guid = "3D2F53A3-F1F0-489B-B20B-BADB09CDAEBE")]
        public readonly Slot<SharpDX.Direct3D11.Buffer> Buffer = new Slot<SharpDX.Direct3D11.Buffer>();

        [Output(Guid = "A0ECA9CE-35AA-497D-B5C9-CDE52A7C8D58")]
        public readonly Slot<int> VertexCount = new Slot<int>();

        // [Output(Guid = "973aebfa-e15d-4943-a9b8-91e6702329d0")]
        // public readonly Slot<string> Result = new Slot<string>();

        public _RenderFontBuffer()
        {
            Buffer.UpdateAction = Update;
            //Result.UpdateAction = Update;
        }

        private Font _font = null;

        private void Update(EvaluationContext context)
        {
            //var triggerUpdate = TriggerUpdate.GetValue(context);
            Log.Debug("_RenderFontBuffer.update()");
            Log.Debug("Filepath isDirty:" + Filepath.DirtyFlag.IsDirty);
            

            if (Filepath.DirtyFlag.IsDirty || _font == null)
            {
                var filepath = Filepath.GetValue(context);
                //Log.Debug(File.ReadAllText(filepath));

                var serializer = new XmlSerializer(typeof(Font));
                try
                {
                    var stream = new FileStream(filepath, FileMode.Open);
                    _font = (Font)serializer.Deserialize(stream);
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

        // private string _text;
        // private float _characterSpacing;
        // private float _lineHeight;
        // private float _slant;
        // private int _horizontalAlign;
        // private int _verticalAlign;
        // private float _size;

        private float _lastWidth;

        private void UpdateMesh(EvaluationContext context)
        {
            var _text = Text.GetValue(context);
            if (string.IsNullOrEmpty(_text))
                return;

            if (_font == null)
                return;

            
            var horizontalAlign = (int)HorizontalAlign.GetValue(context);
            var verticalAlign = (int)VerticalAlign.GetValue(context);
            var characterSpacing = Spacing.GetValue(context);
            var slant = Slant.GetValue(context);
            var lineHeight = LineHeight.GetValue(context);
            var size = (float)(Size.GetValue(context)* _font.Info.Size/870486.0);    // Scaling to match 1080p 72DPI pt font sizes 
            var position = Position.GetValue(context);

            var numLinesInText = _text.Split('\n').Length;

            //var normal = new Vector3(0.0f, 0.0f, -1.0f);
            var color = Color.GetValue(context);
            //var tangent = new Vector3(1.0f, 0.0f, 0.0f);
            //var binormal = new Vector3(0.0f, -1.0f, 0.0f);

            //var _font = _font;
            float textureWidth = _font.Common.ScaleW;
            float textureHeight = _font.Common.ScaleH;
            float cursorX = 0;
            float cursorY = 0;

            switch (verticalAlign)
            {
                // Top
                case 0:
                    break;
                // Middle
                case 1:
                    cursorY = _font.Common.LineHeight * lineHeight * (numLinesInText - 2f) / 2;
                    break;
                // Bottom
                case 2:
                    cursorY = _font.Common.LineHeight * lineHeight * numLinesInText;
                    break;
            }

            //const float scale = 0.01f;
            var maxWidth = float.NegativeInfinity;
            var lineWidth = float.NaN;

            if (_bufferContent == null || _bufferContent.Length != _text.Length)
            {
                Log.Debug("Updating buffer size");
                _bufferContent = new BufferLayout[_text.Length];
            }

            uint outputIndex = 0;
            for (var charIndex = 0; charIndex < _text.Length; charIndex++, outputIndex++)
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

                        var charInfo2 = _font.Chars.SingleOrDefault(c => c.Id == (int)charForWidth);

                        if (charInfo2 == null)
                        {
                            continue;
                        }
 
                        
                        var kerning2 = 0.0f;

                        if (charIndex + lookAheadIndex < _text.Length - 1 && _text[charIndex + lookAheadIndex + 1] != '\n')
                        {
                            var nextCharInfo2 = (from @char in _font.Chars
                                                 where @char.Id == (int)_text[charIndex + lookAheadIndex + 1]
                                                 select @char).SingleOrDefault();
                            if (nextCharInfo2 != null)
                            {
                                var kerningInfo2 = (from d in _font.Kernings
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
                        lineWidth += characterSpacing;
                    }

                    switch (horizontalAlign)
                    {
                        case 1:
                            cursorX -= lineWidth / 2 - characterSpacing / 2;
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
                    cursorY -= _font.Common.LineHeight * lineHeight;
                    cursorX = 0;
                    lineWidth = float.NaN;
                    continue;
                }

                var charInfo = (from @char in _font.Chars
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

                var sizeWidth = charInfo.Width * size;
                var sizeHeight = charInfo.Height * size;
                var x = position.X + (cursorX + charInfo.XOffset) * size;
                var y = position.Y + (cursorY - charInfo.YOffset) * size;

                //Log.Debug($"{charToDraw} => {sizeHeight:0.00}x{sizeHeight:0.00}  @ {x:0.000}  {y:0.000}");
                _bufferContent[outputIndex] = new BufferLayout()
                                                  {
                                                      Position = new Vector3(x, y, 0),
                                                      Size = sizeHeight,
                                                      Orientation = new Vector3(0, 1, 0),
                                                      AspectRatio = sizeWidth / sizeHeight,
                                                      Color = color,
                                                      UvMinMax = new Vector4(
                                                                             charInfo.X / textureWidth, //uLeft = 
                                                                             charInfo.Y / textureHeight, //vTop = 
                                                                             (charInfo.X + charInfo.Width) / textureWidth, //uRight 
                                                                             (charInfo.Y + charInfo.Height) /
                                                                             textureHeight //vBottom                              
                                                                            ),
                                                      BirthTime = (float)context.TimeInBars,
                                                      Speed = 0,
                                                      Id = outputIndex,
                                                  };

                var kerning = 0.0f;
                if (charIndex < _text.Length - 1)
                {
                    var kerningInfo = _font.Kernings.SingleOrDefault(d => d.First == charInfo.Id && d.Second == (int)_text[charIndex + 1]);
                    kerning = kerningInfo?.Amount ?? 0;
                }

                cursorX += kerning;
                cursorX += charInfo.XAdvance;
                cursorX += characterSpacing;
                // numTriangles += 2;
            }

            ResourceManager.Instance().SetupStructuredBuffer(_bufferContent, ref Buffer.Value);
            Buffer.Value.DebugName = nameof(_RenderFontBuffer);

            VertexCount.Value = _text.Length * 6;

            _lastWidth = maxWidth;
        }

        private BufferLayout[] _bufferContent;

        // Must be multiple of 16
        [StructLayout(LayoutKind.Explicit, Size = 32)]
        public struct BufferLayout
        {
            [FieldOffset(0)]
            public Vector3 Position;

            [FieldOffset(3 * 4)]
            public float Size;

            [FieldOffset(4 * 4)]
            public Vector3 Orientation;

            [FieldOffset(7 * 4)]
            public float AspectRatio;

            [FieldOffset(8 * 4)]
            public Vector4 Color;

            [FieldOffset(12 * 4)]
            public Vector4 UvMinMax;

            [FieldOffset(16 * 4)]
            public float BirthTime;

            [FieldOffset(17 * 4)]
            public float Speed;

            [FieldOffset(18 * 4)]
            public uint Id;
        }
    }
}