using System;
using System.Collections.Generic;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
using NAudio.Midi;
using SharpDX;
using T3.Core;
using T3.Core.Logging;
using Vector2 = System.Numerics.Vector2;

namespace T3.Operators.Types.Id_59a0458e_2f3a_4856_96cd_32936f783cc5
{
    public class MidiInput : Instance<MidiInput>, IDisposable
    {
        [Output(Guid = "01706780-D25B-4C30-A741-8B7B81E04D82", DirtyFlagTrigger = DirtyFlagTrigger.Animated)]
        public readonly Slot<float> Result = new Slot<float>();

        [Input(Guid = "D79F9A16-3A81-4A8B-989F-D4DF1EC6A6E1")]
        public readonly InputSlot<float> Value = new InputSlot<float>();

        [Input(Guid = "7C681EE6-D071-4284-8585-1C3E03A089EA")]
        public readonly InputSlot<bool> TeachTrigger = new InputSlot<bool>();

        [Input(Guid = "23C34F4C-4BA3-4834-8D51-3E3909751F84")]
        public readonly InputSlot<string> Device = new InputSlot<string>();

        [Input(Guid = "9B0D32DE-C53C-4DF6-8B29-5E68A5A9C5F9")]
        public readonly InputSlot<int> Channel = new InputSlot<int>();

        [Input(Guid = "DF81B7B3-F39E-4E5D-8B97-F29DD576A76D")]
        public readonly InputSlot<int> Control = new InputSlot<int>();

        [Input(Guid = "AAD1E576-F144-423F-83B5-5694B1119C23")]
        public readonly InputSlot<Vector2> OutputRange = new InputSlot<Vector2>();

        [Input(Guid = "4636D6CF-8233-4281-8840-5BA079B5F1A6")]
        public readonly InputSlot<float> DefaultMidiValue = new InputSlot<float>();

        private bool UsePickup = false;

        [Input(Guid = "3B350FF2-004C-457B-983D-21D11A01D170")]
        public readonly InputSlot<bool> AllowPresets = new InputSlot<bool>();

        [Input(Guid = "6C15E743-9A70-47E7-A0A4-75636817E441")]
        public readonly InputSlot<bool> PrintLogMessages = new InputSlot<bool>();

        [Input(Guid = "F650985F-00A7-452A-B3E4-69A8E9A78C3F")]
        public readonly InputSlot<Size2> ControlRange = new InputSlot<Size2>();

        public MidiInput()
        {
            Result.UpdateAction = Update;

            CloseMidiDevices();
            _instances.Add(this);
            ScanAndRegisterToMidiDevices();
        }

        protected override void Dispose(bool isDisposing)
        {
            if (!isDisposing)
                return;

            UnregisterHandlersForInstance(this);
        }

        private void Update(EvaluationContext context)
        {
            var v = Value.GetValue(context);

            _trainedDeviceName = Device.GetValue(context);
            _trainedChannel = Channel.GetValue(context);
            _trainedControllerId = Control.GetValue(context);
            _allowPresets = AllowPresets.GetValue(context);

            _controlRange = ControlRange.GetValue(context);

            _printLogMessages = PrintLogMessages.GetValue(context);

            var teachTrigger = !TeachTrigger.GetValue(context);
            var teachJustTriggered = !teachTrigger && !_oldTeachTrigger;
            _oldTeachTrigger = !teachTrigger;
            if (teachJustTriggered)
            {
                CloseMidiDevices();
                ScanAndRegisterToMidiDevices(logInformation: true);
                _teachingActive = true;
                _lastMatchingSignal = null;
                _currentControllerValue = 0;
            }

            if (_lastMatchingSignal != null)
            {
                lock (this)
                {
                    if (_teachingActive)
                    {
                        Device.TypedInputValue.Value = _lastMessageDevice.ProductName;
                        Device.Input.IsDefault = false;
                        Device.DirtyFlag.Invalidate();

                        Channel.TypedInputValue.Value = _lastMatchingSignal.Channel;
                        Channel.Input.IsDefault = false;
                        Channel.DirtyFlag.Invalidate();

                        Control.TypedInputValue.Value = _lastMatchingSignal.ControllerId;
                        Control.Input.IsDefault = false;
                        Control.DirtyFlag.Invalidate();

                        _trainedDeviceName = _lastMessageDevice.ProductName;
                        _trainedChannel = (int)_lastMatchingSignal.Channel;
                        _trainedControllerId = (int)_lastMatchingSignal.ControllerId;
                        _teachingActive = false;

                        TeachTrigger.TypedInputValue.Value = false;
                    }

                    _currentControllerValue = _lastMatchingSignal.ControllerValue;
                    _currentControllerId = _lastMatchingSignal.ControllerId;
                    _isDefaultValue = false;
                    _lastMatchingSignal = null;
                }
            }


            if (_isDefaultValue)
            {
                Result.Value = DefaultMidiValue.GetValue(context);
                return;
            }

            var outRange = OutputRange.GetValue(context);
            Result.Value = UseControlRange
                               ? _currentControllerId
                               : MathUtils.Remap(_currentControllerValue, 0, 127, outRange.X, outRange.Y);
        }

        public static void ScanAndRegisterToMidiDevices(bool logInformation = false)
        {
            for (int index = 0; index < MidiIn.NumberOfDevices; index++)
            {
                var deviceInfo = MidiIn.DeviceInfo(index);

                if (logInformation)
                    Log.Debug("Scanning " + deviceInfo.ProductName);

                MidiIn newMidiIn = null;
                try
                {
                    newMidiIn = new MidiIn(index);
                }
                catch (NAudio.MmException e)
                {
                    Log.Error(" > " + e.Message + " " + MidiIn.DeviceInfo(index).ProductName);
                    continue;
                }

                // if (newMidiIn == null)
                // {
                //     Log.Error("Could not get device for Id" + index);
                //     continue;
                // }

                foreach (var midiInstance in _instances)
                {
                    newMidiIn.MessageReceived += midiInstance.MessageReceivedHandler;
                    newMidiIn.ErrorReceived += midiInstance.ErrorReceivedHandler;
                }

                newMidiIn.Start();
                _midiInsWithDevices[newMidiIn] = deviceInfo;
            }
        }

        private static void CloseMidiDevices()
        {
            foreach (var midiIn in _midiInsWithDevices.Keys)
            {
                foreach (var midiInputInstance in _instances)
                {
                    midiIn.MessageReceived -= midiInputInstance.MessageReceivedHandler;
                    midiIn.ErrorReceived -= midiInputInstance.ErrorReceivedHandler;
                }

                try
                {
                    midiIn.Stop();
                    midiIn.Close();
                    midiIn.Dispose();
                }
                catch (Exception e)
                {
                    Log.Debug("exception: " + e);
                }
            }

            _midiInsWithDevices.Clear();
        }

        private static void UnregisterHandlersForInstance(MidiInput instance)
        {
            if (!_instances.Contains(instance))
                return;

            foreach (var midiIn in _midiInsWithDevices.Keys)
            {
                midiIn.MessageReceived -= instance.MessageReceivedHandler;
                midiIn.ErrorReceived -= instance.ErrorReceivedHandler;
            }

            _instances.Remove(instance);
            if (_instances.Count == 0)
            {
                CloseMidiDevices();
            }
        }

        private bool _onlyAcceptControlChanges = true;
        private bool _onlyAcceptNotes;

        private void ErrorReceivedHandler(object sender, MidiInMessageEventArgs msg)
        {
        }

        private void MessageReceivedHandler(object sender, MidiInMessageEventArgs msg)
        {
            lock (this)
            {
                var midiIn = sender as MidiIn;
                if (midiIn == null || msg.MidiEvent == null)
                    return;

                if (!_midiInsWithDevices.ContainsKey(midiIn))
                    return;

                MidiSignal newSignal = null;
                var acceptControlChanges = !_onlyAcceptNotes; // && _outputMode != OutputModes.MidiSync;
                var acceptNotes = _onlyAcceptControlChanges; // _inputMode != InputModes.ControlChangesOnly && _outputMode != OutputModes.MidiSync;

                var device = _midiInsWithDevices[midiIn];

                if (acceptControlChanges && msg.MidiEvent is ControlChangeEvent controlEvent)
                {
                    if (_printLogMessages)
                        Log.Debug("" + controlEvent + "  ControlValue :" + controlEvent.ControllerValue);
                    
                    newSignal = new MidiSignal()
                                    {
                                        Channel = controlEvent.Channel,
                                        ControllerId = (int)controlEvent.Controller,
                                        ControllerValue = (int)controlEvent.ControllerValue,
                                    };
                }

                if (acceptNotes && msg.MidiEvent is NoteEvent noteEvent)
                {
                    if (noteEvent.CommandCode == MidiCommandCode.NoteOn)
                    {
                        if (_printLogMessages)
                            Log.Debug("" + noteEvent + "  ControlValue :" + noteEvent.NoteNumber);

                        newSignal = new MidiSignal()
                                        {
                                            Channel = noteEvent.Channel,
                                            ControllerId = noteEvent.NoteNumber,
                                            ControllerValue = noteEvent.Velocity,
                                        };
                    }
                    else if (noteEvent.CommandCode == MidiCommandCode.NoteOff)
                    {
                        newSignal = new MidiSignal()
                                        {
                                            Channel = noteEvent.Channel,
                                            ControllerId = noteEvent.NoteNumber,
                                            ControllerValue = 0,
                                        };
                    }
                }
                else if (msg.MidiEvent.CommandCode == MidiCommandCode.TimingClock
                         && device.ProductName == _trainedDeviceName)
                {
                    //_timingMsgCount++;
                }

                if (newSignal != null)
                {
                    var matchesDevice = String.IsNullOrEmpty(_trainedDeviceName) || device.ProductName == _trainedDeviceName;
                    var matchesChannel = _trainedChannel < 0 || newSignal.Channel == _trainedChannel;

                    var matchesSingleController = _trainedControllerId < 0 || newSignal.ControllerId == _trainedControllerId;
                    var matchesControlRange = _trainedControllerId < 0 ||
                                              (newSignal.ControllerId >= _controlRange.Width && newSignal.ControllerId <= _controlRange.Height);

                    var matchesController = UseControlRange
                                                ? matchesControlRange
                                                : matchesSingleController;

                    if (_teachingActive || (matchesDevice && matchesChannel && matchesController))
                    {
                        _lastMatchingSignal = newSignal;
                        _lastMessageDevice = device;
                        _isDefaultValue = false;
                    }
                }
            }
        }

        private Size2 _controlRange;
        private bool UseControlRange => _controlRange.Width > 0 || _controlRange.Height > 0;

        private static List<MidiInput> _instances = new List<MidiInput>();
        private static Dictionary<MidiIn, MidiInCapabilities> _midiInsWithDevices = new Dictionary<MidiIn, MidiInCapabilities>();

        private bool _gotSyncSignal = false;

        private class MidiSignal
        {
            public int Channel;
            public int ControllerId;
            public int ControllerValue;
        }

        private Vector2 _outputRange;

        #region implement IMidiInput ------------------------------
        public string GetDevice()
        {
            return _trainedDeviceName;
        }

        public float GetChannel()
        {
            return _trainedChannel;
        }

        public float GetControl()
        {
            return _trainedControllerId;
        }

        public float CurrentMidiValue
        {
            get { return _currentControllerValue; }
            set
            {
                _currentControllerValue = value;
                _isDefaultValue = false;
                //Changed = true;
                //_valueHasBeenChanged = true;
                //Log.Debug(this, "Setting value to :" + value);
                //_waitingForPickup = false;
            }
        }

        public float TargetMidiValue { get { return _currentControllerValue; } set { CurrentMidiValue = value; } }

        private bool _allowPresets;
        #endregion

        private bool _printLogMessages = false;
        private bool _isDefaultValue = true;
        private bool _oldTeachTrigger;
        private bool _teachingActive;
        private String _trainedDeviceName;
        private int _trainedChannel = -1;
        private int _trainedControllerId = -1;
        private MidiSignal _lastMatchingSignal;
        private MidiInCapabilities _lastMessageDevice;

        private float _currentControllerValue;
        private int _currentControllerId;

        // private float _previousValue;
    }
}