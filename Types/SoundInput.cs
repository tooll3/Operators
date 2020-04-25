using System;
using System.Collections.Generic;
using System.Windows.Threading;
using ManagedBass;
using ManagedBass.Wasapi;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_b72d968b_0045_408d_a2f9_5c739c692a66
{
    public class SoundInput : Instance<SoundInput>
    {
        [Output(Guid = "b438986f-6ef9-40d5-8a2c-b00c01578ebc")]
        public readonly Slot<float> Result = new Slot<float>();

        public SoundInput()
        {
            Result.UpdateAction = Update;
            Result.DirtyFlag.Trigger |= DirtyFlagTrigger.Always;
        }
        
        private void Update(EvaluationContext context)
        {
            if (_analyzer == null)
                _analyzer = new Analyzer();

            _analyzer.SetDeviceIndex((int)Input1.GetValue(context));

            if (_analyzer.SpectrumData.Count < 16)
                return;

            Result.Value = _analyzer.SpectrumData[2];
        }

        private static Analyzer _analyzer;

        [Input(Guid = "e8a10146-ef7f-459c-a1f8-eef621a2c522")]
        public readonly InputSlot<float> Input1 = new InputSlot<float>();
    }

    /// <summary>
    /// This is based on https://www.codeproject.com/Articles/797537/
    /// Audio Spectrum by @webmaster442
    /// </summary>
    internal class Analyzer
    {
        public Analyzer()
        {
            _timer.Interval = TimeSpan.FromMilliseconds(25); //40hz refresh rate
            _timer.Tick += TimerUpdateEventHandler;
            _wasapiProcedure = new WasapiProcedure(Process); // capture to avoid freeing by GC
            _initialized = false;
            Init();
        }

        private int _deviceIndex = 0;

        public void SetDeviceIndex(int index)
        {
            if (index == _deviceIndex)
                return;

            _deviceIndex = index;
            _initialized = false;
            SetEnable(true);
        }

        private void SetEnable(bool enable)
        {
            _enable = enable;
            if (enable)
            {
                if (!_initialized)
                {
                    var str = (_deviceList[_deviceIndex % _deviceList.Count] as string);
                    var array = str.Split(' ');
                    _wasapiDeviceIndex = Convert.ToInt32(array[0]);
                    Log.Debug($"Initializing WASAPI for {str}... #{_wasapiDeviceIndex}");
                    bool result = BassWasapi.Init(_wasapiDeviceIndex, 0, 0,
                                                  WasapiInitFlags.Buffer,
                                                  1f, 0.05f,
                                                  _wasapiProcedure, IntPtr.Zero);
                    if (!result)
                    {
                        Log.Error("Can't initialize WASAPI:" + Bass.LastError);
                    }
                    else
                    {
                        _initialized = true;
                    }
                }

                BassWasapi.Start();
            }
            else
            {
                BassWasapi.Stop();
            }

            System.Threading.Thread.Sleep(500);
            _timer.IsEnabled = enable;
        }

        
        private void Init()
        {
            var result = false;
            for (var i = 0; i < BassWasapi.DeviceCount; i++)
            {
                var device = BassWasapi.GetDeviceInfo(i);
                if (device.IsEnabled && device.IsLoopback)
                {
                    _deviceList.Add(string.Format($"{i} - {device.Name}"));
                }
            }

            Bass.Configure(Configuration.UpdateThreads, false);

            result = Bass.Init(Device: 0, Frequency: 44100, Flags: DeviceInitFlags.Default, Win: IntPtr.Zero);
            if (result)
            {
                Log.Debug("Successfully initialized BASS.Init()");
            }

            if (!result)
            {
                Log.Error("Bass initialization failed:" + Bass.LastError);
            }

            SetEnable(true);
        }

        private void TimerUpdateEventHandler(object sender, EventArgs e)
        {
            // get FFT data. Return value is -1 on error
            var ret = BassWasapi.GetData(_fftBuffer, _fftBuffer.Length);
            if (ret < 0)
                return;

            int x, y;
            var b0 = 0;

            // Compute the spectrum data, the code is taken from a bass_wasapi sample.
            SpectrumData.Clear();
            for (x = 0; x < SpectrumLineCount; x++)
            {
                float peak = 0;
                var b1 = (int)Math.Pow(2, x * 10.0 / (SpectrumLineCount - 1));
                if (b1 > 1023) b1 = 1023;
                if (b1 <= b0) b1 = b0 + 1;
                for (; b0 < b1; b0++)
                {
                    if (peak < _fftBuffer[1 + b0]) peak = _fftBuffer[1 + b0];
                }

                y = (int)(Math.Sqrt(peak) * 3 * 255 - 4);
                if (y > 255) y = 255;
                if (y < 0) y = 0;
                SpectrumData.Add((byte)y);
            }

            // Get audio level
            var level = BassWasapi.GetLevel();
            var left = level &= 0xffff;
            var right = level >> 16;
            LeftLevel = 65384f / left;
            RightLevel = 65384f / right;

            if (level == LastLevel && level != 0) _hangCounter++;
            LastLevel = level;

            // Required, because some programs hang the output. If the output hangs for a 75ms
            // this piece of code re initializes the output so it doesn't make a glitched sound for long.
            if (_hangCounter > 10)
            {
                Log.Warning("Looks like sound got lost. Trying to restart.");
                _hangCounter = 0;
                Free();
                
                //Bass.BASS_Init(0, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
                Bass.Init();
                _initialized = false;
                SetEnable(true);
            }
        }

        /// <summary>
        /// WASAPI callback, required for continuous recording
        /// </summary>
        private static int Process(IntPtr buffer, int length, IntPtr user)
        {
            return length;
        }

        //cleanup
        private static void Free()
        {
            BassWasapi.Free();
            Bass.Free();
        }

        private bool _enable;
        private readonly DispatcherTimer _timer = new DispatcherTimer(); //timer that refreshes the display

        public readonly List<byte> SpectrumData = new List<byte>();
        private readonly float[] _fftBuffer = new float[1024];

        private WasapiProcedure _wasapiProcedure;
        public float RightLevel = 0;
        public float LeftLevel = 0;
        public int LastLevel = 0;
        private int _hangCounter = 0;
        
        private readonly List<string> _deviceList = new List<string>();
        private bool _initialized;
        private int _wasapiDeviceIndex;
        private const int SpectrumLineCount = 16;
    }
}