using System;
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
            //StartRecording();
            StartWasapi();
        }

        private const int BufferSize = 10;
        private void Update(EvaluationContext context)
        {
            //Bass.ChannelGetData(_recHandle, _buffer,  Length: BufferSize);
            //var level = Bass.ChannelGetLevel(_recHandle);
            //Result.Value = level /10000f;
            
            //BassWasapi.GetData(_floatBuffer, _floatBufferLength);
            BassWasapi.GetLevel(_floatBuffer, 10, LevelRetrievalFlags.All);
            Result.Value = _floatBuffer[0] + 0.1f;
        }

        private readonly int[] _buffer = new int[BufferSize];

        private const int _floatBufferLength = 100;
        private float[] _floatBuffer = new float[_floatBufferLength];
        
        //private BassWasapi;
        private const int Undefined = -1;
        private void StartWasapi()
        {
            WasapiDeviceInfo devInfo;

            
            var defaultDeviceIndex = Undefined;
            for (var i = 0; BassWasapi.GetDeviceInfo(i, out devInfo); ++i)
            {
                if (devInfo.IsEnabled && devInfo.IsLoopback)
                {
                    defaultDeviceIndex = i;
                    Log.Debug($"Device: {i}:" + devInfo);
                    break;
                }
            }

            if (defaultDeviceIndex == Undefined)
            {
                Log.Warning("Can't find default wasapi device");
                return;
            }
            
            //BassWasapi.CurrentDevice = 0;
            //if (BassWasapi.Init(defaultDeviceIndex,Frequency: 48000, Channels:2 , Flags: WasapiInitFlags.Buffer, Buffer: 0f, Period: 0f))
            if (!BassWasapi.Init(-2))
            {
                Log.Debug("init WASAPI failed");
            }

            BassWasapi.Start();
            
            // BassWasapi.GetInfo(out WasapiInfo info);
            // Log.Debug("Wasapi:" + info);
            //
            // BassWasapi.Start();
            // BassWasapi.GetData(_floatBuffer, _floatBufferLength);
            // var deviceInfo= BassWasapi.GetDeviceInfo(deviceIndex);
            // Log.Debug("Device: " + deviceInfo);

            //var _wasapi = BassWasapi;
            //Private _wasapi As BassWasapiHandler
            //    ...
            // not playing anything via BASS, so don't need an update thread
            //Bass.UpdatePeriod = 0;
            // ' setup BASS - "no sound" device
            //Bass.Init(0, 48000, DeviceInitFlags.Default, IntPtr.Zero);
            //     ...


            // assign WASAPI input in shared-mode
            // _wasapi = new BassWasapiHandler(-2, false, 48000, 2, 0f, 0f)
            //var xx = new WasapiProcedure()
            // ' init and start WASAPI
            // _wasapi.Init()
            // Dim recordStream As Integer = _wasapi.InputChannel
            // ' double check, that the device is not muted externally
            //     If _wasapi.DeviceMute Then
            // _wasapi.DeviceMute = False
            // End If
            // _wasapi.Start()
            // End If
            //     ...
            // ' now you can use recordStream to setup any DSP/FX etc.            
        }
        
        //private  RECORDPROC _myRecProc; // make it global, so that the GC can not remove it 
        private void StartRecording()
        {
            if (!Bass.RecordInit(-1))
                return;
            
            _recHandle = Bass.RecordStart(44100, 2, BassFlags.RecordPause, null, IntPtr.Zero);
            Bass.ChannelPlay(_recHandle, false);
        }
        
        /*
        private void StartRecordingWithCallback()
        {
            if (!Bass.RecordInit(-1))
                return;
            
            _myRecProc = OnRecordingUpdate;
            _recHandle = Bass.RecordStart(44100, 2, BassFlags.RecordPause, _myRecProc, IntPtr.Zero);
            Bass.ChannelPlay(_recHandle, false);
        }
        
        private bool OnRecordingUpdate(int handle, IntPtr buffer, int length, IntPtr user)
        {
            if (length <= 0 || buffer == IntPtr.Zero)
                return true;
            
            // Increase the rec buffer as needed 
            if (_recbuffer == null || _recbuffer.Length < length)
                _recbuffer = new byte[length];
                
            // Copy from managed to unmanaged memory
            //Marshal.Copy(buffer, _recbuffer, 0, length);
            //_bytesWritten += length;
                
            // write to file
            // ... insert here...
                
            // Stop recording after a certain Amount (just to demo) 
            var continueRecording = (_byteswritten < 800000);
            return continueRecording;
        }
        
        private int _bytesWritten = 0;
        private byte[] _recbuffer; // local recording buffer
        
        */
        
        private int _recHandle;


        [Input(Guid = "e8a10146-ef7f-459c-a1f8-eef621a2c522")]
        public readonly InputSlot<float> Input1 = new InputSlot<float>();

        [Input(Guid = "81b675d8-e6e2-4661-b0d5-8e3bc2767265")]
        public readonly InputSlot<float> Input2 = new InputSlot<float>();

        private RecordProcedure _myRecProc;

        //         [Input(Guid = "{D7478BAA-41B4-4F83-873B-6267AA93BFA9}")]
        //         public readonly InputSlot<float> Input3 = new InputSlot<float>();
        // 
        //         [Input(Guid = "{99A53560-8F62-4240-9ED4-800525CF2EF3}")]
        //         public readonly InputSlot<float> Input4 = new InputSlot<float>();
    }
}