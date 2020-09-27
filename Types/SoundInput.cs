using System;
using System.Collections.Generic;
using System.Linq;
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
        [Output(Guid = "B3EFDF25-4692-456D-AA48-563CFB0B9DEB", DirtyFlagTrigger = DirtyFlagTrigger.Animated)]
        public readonly Slot<List<float>> FftBuffer = new Slot<List<float>>();

        [Output(Guid = "b438986f-6ef9-40d5-8a2c-b00c01578ebc", DirtyFlagTrigger = DirtyFlagTrigger.Animated)]
        public readonly Slot<float> Result = new Slot<float>();

        [Output(Guid = "D7D2A87C-4231-4F8B-904F-6E5F5D01B1D8", DirtyFlagTrigger = DirtyFlagTrigger.Animated)]
        public readonly Slot<float> AvailableData = new Slot<float>();

        public SoundInput()
        {
            Result.UpdateAction = Update;
            FftBuffer.UpdateAction = Update;
            AvailableData.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            if (_analyzer == null)
                _analyzer = new Analyzer();

            _analyzer.SetDeviceIndex((int)Input1.GetValue(context));

            //if (_analyzer.SpectrumData.Count < 16)
            //    return;

            //Result.Value = _analyzer.SpectrumData[2];
            FftBuffer.Value = _analyzer.FftBuffer.ToList();
            AvailableData.Value = _analyzer.AvailableData;
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
            _timer.Interval = TimeSpan.FromMilliseconds(1 / 44.1f * 1000);
            _timer.Tick += TimerUpdateEventHandler;
            // ReSharper disable once RedundantDelegateCreation
            _wasapiProcedure = new WasapiProcedure(Process); // capture to avoid freeing by GC
            _initialized = false;
            Init();
        }

        /// <summary>
        /// This property returns the length of the available data.
        /// This might be useful for eventually discover the reason for
        /// the frequent connection problems to sound provides like streaming from Firefox 
        /// </summary>
        public float AvailableData { get; private set; }

        public void SetDeviceIndex(int index)
        {
            if (index == _deviceIndex)
                return;

            _deviceIndex = index;
            _initialized = false;
            SetEnableWasapi(true);
        }

        private void SetEnableWasapi(bool enable)
        {
            if (!enable)
            {
                BassWasapi.Stop();
                //BassWasapi.Free();
                _timer.Stop();
                _timer.IsEnabled = false;
                return;
            }

            if (!_initialized)
            {
                var str = _deviceList[_deviceIndex % _deviceList.Count];
                var array = str.Split(' ');
                _wasapiDeviceIndex = Convert.ToInt32(array[0]);
                Log.Debug($"Initializing WASAPI for {str}... #{_wasapiDeviceIndex}");
                if (!BassWasapi.Init(_wasapiDeviceIndex, 
                                     Frequency: 0, 
                                     Channels: 0,
                                     Flags: WasapiInitFlags.Buffer,
                                     Buffer: 0.004f, // was 1
                                     Period: 0.004f,
                                     Procedure: _wasapiProcedure, 
                                     User: IntPtr.Zero))
                {
                    Log.Error("Can't initialize WASAPI:" + Bass.LastError);
                    return;
                }

                _initialized = true;
            }

            BassWasapi.Start();
            System.Threading.Thread.Sleep(500);
            _timer.IsEnabled = true;
            _timer.Start();
        }

        private void Init()
        {
            InitBass();
            SetEnableWasapi(true);
        }

        private void InitBass()
        {
            for (var i = 0; i < BassWasapi.DeviceCount; i++)
            {
                var device = BassWasapi.GetDeviceInfo(i);
                if (device.IsEnabled && device.IsLoopback)
                {
                    _deviceList.Add(string.Format($"{i} - {device.Name}"));
                }
            }

            Bass.Configure(Configuration.UpdateThreads, false);

            var result = Bass.Init(Device: 0, Frequency: 44100, Flags: DeviceInitFlags.Default, Win: IntPtr.Zero);
            if (result)
            {
                Log.Debug("Successfully initialized BASS.Init()");
            }

            if (!result)
            {
                Log.Error("Bass initialization failed:" + Bass.LastError);
            }
        }

        private void TimerUpdateEventHandler(object sender, EventArgs e)
        {
            BassWasapi.GetData(null, (int)DataFlags.Available);
            
            // get FFT data. Return value is -1 on error
            var ret = BassWasapi.GetData(FftBuffer, (int)DataFlags.FFT1024);    // Note: The DataFlags seems to be offset by one (FFT256 only fills 128 entries)
            if (ret < 0)
                return;
            
            int x;
            var b0 = 0;

            // Compute the spectrum data, the code is taken from a bass_wasapi sample.
            // SpectrumData.Clear();
            // for (x = 0; x < SpectrumLineCount; x++)
            // {
            //     float peak = 0;
            //     var b1 = (int)Math.Pow(2, x * 10.0 / (SpectrumLineCount - 1));
            //
            //     if (b1 > FftBuffer.Length - 2)
            //         b1 = FftBuffer.Length - 2;
            //
            //     if (b1 <= b0)
            //         b1 = b0 + 1;
            //
            //     for (; b0 < b1; b0++)
            //     {
            //         if (peak < FftBuffer[1 + b0])
            //             peak = FftBuffer[1 + b0];
            //     }
            //
            //     var y = (int)(Math.Sqrt(peak) * 3 * 255 - 4);
            //     if (y > 255)
            //         y = 255;
            //
            //     if (y < 0)
            //         y = 0;
            //
            //     SpectrumData.Add(y);
            // }

            // Get audio level
            var level = BassWasapi.GetLevel();
            var left = level &= 0xffff;
            var right = level >> 16;
            LeftLevel = 65384f / left;
            RightLevel = 65384f / right;

            if (level == _lastLevel && level != 0) _hangCounter++;
            _lastLevel = level;

            // Required, because some programs hang the output. If the output hangs for a 75ms
            // this piece of code re initializes the output so it doesn't make a glitched sound for long.
            if (_hangCounter > 120)
            {
                Log.Warning("Looks like sound got lost. Trying to restart.");
                _hangCounter = 0;
                Free();

                InitBass();
                _initialized = false;
                SetEnableWasapi(true);
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

        private int _deviceIndex;
        private readonly DispatcherTimer _timer = new DispatcherTimer(); //timer that refreshes the display

        //public readonly List<float> SpectrumData = new List<float>();
        public readonly float[] FftBuffer = new float[1024];

        private readonly WasapiProcedure _wasapiProcedure;
        public float RightLevel;
        public float LeftLevel;
        private int _lastLevel;
        private int _hangCounter;

        private readonly List<string> _deviceList = new List<string>();
        private bool _initialized;
        private int _wasapiDeviceIndex;
        private const int SpectrumLineCount = 16;
    }
}