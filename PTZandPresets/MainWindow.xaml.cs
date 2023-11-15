using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using VideoOS.Platform;
using VideoOS.Platform.ConfigurationItems;
using VideoOS.Platform.Data;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.UI;
using VideoOS.Platform.UI.Controls;

namespace PTZandPresets
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : VideoOSWindow, INotifyPropertyChanged
    {
        Item _selectedCamera = null;
        bool _updatingFromCode = false;

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        #region Properties controlling the UI
        public bool CameraSelected => _selectedCamera != null;

        public string CameraName => _selectedCamera == null ? "Select Camera..." : _selectedCamera.Name;

        /// <summary>
        /// We take a ptz preset and see if it is a preset imported from device, if it is we know that imported presets are used and we cannot set a preset.
        /// Imported ptz preset are set in the device.
        /// </summary>
        /// <returns></returns>
        public bool ArePresetsSettable
        {
            get
            {
                if (_selectedCamera == null)
                {
                    return false;
                }
                bool result = true;
                var camera = new VideoOS.Platform.ConfigurationItems.Camera(_selectedCamera.FQID);
                PtzPresetFolder ptzPresetFolder = camera.PtzPresetFolder;
                if (ptzPresetFolder == null) return false;

                PtzPreset ptzPreset = ptzPresetFolder.PtzPresets.FirstOrDefault();
                if (ptzPreset != null)
                {
                    // If for a preset the DevicePresetInternalId is not blank the preset is imported from device.
                    result = ptzPreset.DevicePresetInternalId == "";
                }
                return result;
            }
        }

        public bool PresetSelected => ArePresetsSettable && _presetsDropDown.SelectedItem != null;

        public ObservableCollection<Item> CameraPresets { get; } = new ObservableCollection<Item>();

        public ObservableCollection<EventSequence> CameraSequences { get; } = new ObservableCollection<EventSequence>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion


        #region Absolute PTZ
        private void GetAbsolutePositionClicked(object sender, RoutedEventArgs e)
        {
            Collection<object> objResult = EnvironmentManager.Instance.SendMessage(
                        new Message(MessageId.Control.PTZGetAbsoluteRequest), _selectedCamera.FQID);

            PTZGetAbsoluteRequestData positionData = (PTZGetAbsoluteRequestData)objResult[0];
            _panBox.Text = positionData.Pan.ToString(System.Globalization.CultureInfo.InvariantCulture);
            _tiltBox.Text = positionData.Tilt.ToString(System.Globalization.CultureInfo.InvariantCulture);
            _zoomBox.Text = positionData.Zoom.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        private void SetAbsolutePositionClicked(object sender, RoutedEventArgs e)
        {
            PTZMoveAbsoluteCommandData absoluteCommand = new PTZMoveAbsoluteCommandData();
            double.TryParse(_panBox.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out absoluteCommand.Pan);
            double.TryParse(_tiltBox.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out absoluteCommand.Tilt);
            double.TryParse(_zoomBox.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out absoluteCommand.Zoom);

            EnvironmentManager.Instance.PostMessage(new Message(MessageId.Control.PTZMoveAbsoluteCommand, absoluteCommand), _selectedCamera.FQID);
        }
        #endregion

        #region move in steps
        private void MoveUpClicked(object sender, RoutedEventArgs e)
        {
            EnvironmentManager.Instance.PostMessage(new Message(MessageId.Control.PTZMoveCommand, PTZMoveCommandData.Up), _selectedCamera.FQID);
        }

        private void MoveLeftClicked(object sender, RoutedEventArgs e)
        {
            EnvironmentManager.Instance.PostMessage(new Message(MessageId.Control.PTZMoveCommand, PTZMoveCommandData.Left), _selectedCamera.FQID);
        }

        private void MoveCenterClicked(object sender, RoutedEventArgs e)
        {
            var bmp = _imageViewer.GetCurrentDisplayedImageAsBitmap();
            if (bmp == null)
                return;

            PTZCenterCommandData centerCommandData = new PTZCenterCommandData();

            centerCommandData.CenterX = Convert.ToDouble(bmp.Width / 2 + 10);
            centerCommandData.CenterY = Convert.ToDouble(bmp.Height / 2 + 10);
            centerCommandData.RefWidth = Convert.ToDouble(bmp.Width);
            centerCommandData.RefHeight = Convert.ToDouble(bmp.Height);
            centerCommandData.Zoom = -1.0;

            EnvironmentManager.Instance.PostMessage(new Message(MessageId.Control.PTZCenterCommand, centerCommandData), _selectedCamera.FQID);
        }

        private void MoveRightClicked(object sender, RoutedEventArgs e)
        {
            EnvironmentManager.Instance.PostMessage(new Message(MessageId.Control.PTZMoveCommand, PTZMoveCommandData.Right), _selectedCamera.FQID);
        }

        private void MoveDownClicked(object sender, RoutedEventArgs e)
        {
            EnvironmentManager.Instance.PostMessage(new Message(MessageId.Control.PTZMoveCommand, PTZMoveCommandData.Down), _selectedCamera.FQID);
        }
        #endregion

        #region Continuous PTZ
        private void StartMoveClicked(object sender, RoutedEventArgs e)
        {
            PTZMoveStartCommandData2 data = new PTZMoveStartCommandData2();
            data.Pan = _panContinuousSlider.Value;
            data.Tilt = _tiltContinuousSlider.Value;
            data.Zoom = _zoomContinuousSlider.Value;
            data.PanSpeed = _panSpeedSlider.Value;
            data.TiltSpeed = _tiltSpeedSlider.Value;
            data.ZoomSpeed = _zoomSpeedSlider.Value;
            EnvironmentManager.Instance.PostMessage(new Message(MessageId.Control.PTZMoveStartCommand, data), _selectedCamera.FQID);
        }

        private void StopMoveClicked(object sender, RoutedEventArgs e)
        {
            EnvironmentManager.Instance.PostMessage(new Message(MessageId.Control.PTZMoveStopCommand), _selectedCamera.FQID);
        }

        private void ReverseTiltValuesUnchecked(object sender, RoutedEventArgs e)
        {
            EnvironmentManager.Instance.EnvironmentOptions["PtzReverseTiltForContinuesMoves"] = false.ToString();
        }

        private void ReverseTiltValuesChecked(object sender, RoutedEventArgs e)
        {
            EnvironmentManager.Instance.EnvironmentOptions["PtzReverseTiltForContinuesMoves"] = true.ToString();
        }
        #endregion

        #region Presets handling
        private void ListPresets()
        {
            // We do not want the selection changed callback to execute at this point, only when the end user changes the selection.
            _updatingFromCode = true;
            CameraPresets.Clear();
            foreach ( var item in _selectedCamera.GetChildren())
            {
                CameraPresets.Add(item);
            }

            _updatingFromCode = false;
        }

        private void SelectedPresetChanged(object sender, RoutedEventArgs e)
        {
            if (_updatingFromCode)
                return;

            // This is the Item which represents the preset we selected in the combo box.
            Item selectedPreset = _presetsDropDown.SelectedItem as Item;

            if (selectedPreset != null)
            {
                // This constucts a "trigger" message
                Message triggerMessage =
                    new Message(MessageId.Control.TriggerCommand);

                // This sends the trigger message to the preset, eventually causing the camera to actually move.
                EnvironmentManager.Instance.PostMessage(triggerMessage, selectedPreset.FQID);
            }
            OnPropertyChanged(nameof(PresetSelected));
        }

        private void AddPresetClicked(object sender, RoutedEventArgs e)
        {
            //get current coordinates
            Collection<object> objResult = EnvironmentManager.Instance.SendMessage(
                    new Message(MessageId.Control.PTZGetAbsoluteRequest), _selectedCamera.FQID);

            PTZGetAbsoluteRequestData datRequestData = (PTZGetAbsoluteRequestData)objResult[0];
            double pan = datRequestData.Pan;
            double tilt = datRequestData.Tilt;
            double zoom = datRequestData.Zoom;
            objResult.Clear();
            try
            {
                var camera = new VideoOS.Platform.ConfigurationItems.Camera(_selectedCamera.FQID);
                PtzPresetFolder folder = camera.PtzPresetFolder;
                folder.AddPtzPreset(_presetNameBox.Text, "", pan, tilt, zoom);
            }
            catch (Exception ex)
            {
                EnvironmentManager.Instance.Log(true, "Create Preset", ex.Message);
                VideoOSMessageBox.Show(this, "Exception in Create preset", "Exception in Create preset", ex.Message, VideoOSMessageBox.Buttons.OK, VideoOSMessageBox.ResultButtons.OK, new VideoOSIconBuiltInSource() { Icon = VideoOSIconBuiltInSource.Icons.Error_Combined });
            }
            RefreshPresetsList();
        }

        private void UpdatePresetClicked(object sender, RoutedEventArgs e)
        {
            //get current coordinates
            Collection<object> objResult = EnvironmentManager.Instance.SendMessage(
                new Message(MessageId.Control.PTZGetAbsoluteRequest), _selectedCamera.FQID);

            PTZGetAbsoluteRequestData datRequestData = (PTZGetAbsoluteRequestData)objResult[0];
            double pan = datRequestData.Pan;
            double tilt = datRequestData.Tilt;
            double zoom = datRequestData.Zoom;
            objResult.Clear();

            Item currentlySelectedPresetItem = _presetsDropDown.SelectedItem as Item;
            string currentlySelectedPresetName = currentlySelectedPresetItem.Name;

            try
            {
                var camera = new VideoOS.Platform.ConfigurationItems.Camera(_selectedCamera.FQID);
                PtzPresetFolder folder = camera.PtzPresetFolder;
                PtzPreset ptzPreset = folder.PtzPresets.Where(x => x.Name == currentlySelectedPresetName).FirstOrDefault();
                if (ptzPreset != null)
                {
                    ptzPreset.Pan = pan;
                    ptzPreset.Tilt = tilt;
                    ptzPreset.Zoom = zoom;
                    ptzPreset.Save();
                }
            }
            catch (Exception ex)
            {
                EnvironmentManager.Instance.Log(true, "Update Preset", ex.Message);
                VideoOSMessageBox.Show(this, "Exception in Update preset", "Exception in Update preset", ex.Message, VideoOSMessageBox.Buttons.OK, VideoOSMessageBox.ResultButtons.OK, new VideoOSIconBuiltInSource() { Icon = VideoOSIconBuiltInSource.Icons.Error_Combined });
            }
        }

        private void DeletePresetClicked(object sender, RoutedEventArgs e)
        {
            Item currentlySelectedPresetItem = _presetsDropDown.SelectedItem as Item;
            string currentlySelectedPresetName = currentlySelectedPresetItem.Name;
            try
            {
                var camera = new VideoOS.Platform.ConfigurationItems.Camera(_selectedCamera.FQID);
                PtzPresetFolder folder = camera.PtzPresetFolder;
                PtzPreset ptzPreset = folder.PtzPresets.Where(x => x.Name == currentlySelectedPresetName).FirstOrDefault();
                if (ptzPreset != null)
                {
                    folder.RemovePtzPreset(ptzPreset.Path);
                }
            }
            catch (Exception ex)
            {
                EnvironmentManager.Instance.Log(true, "Remove Preset", ex.Message);
                VideoOSMessageBox.Show(this, "Exception in Remove preset", "Exception in Remove preset", ex.Message, VideoOSMessageBox.Buttons.OK, VideoOSMessageBox.ResultButtons.OK, new VideoOSIconBuiltInSource() { Icon = VideoOSIconBuiltInSource.Icons.Error_Combined });
            }
            RefreshPresetsList();
        }

        /// <summary>
        /// When we have added or deleted a PTZ preset the configuration is no longer up to date.
        /// Specifically the _camera.GetChildren() will not return the right PTZ Presets.
        /// In this method we make sure the configuration is refreshed (re-read from server),
        /// reload configuration is done pertaining to the recording server to which the camera belongs
        /// and _camera item is renewed.
        /// Finally we re-write the list of preset refreshing the combobox using the ListPresets() method that is used to fill it at start-up also.
        /// </summary>
        private void RefreshPresetsList()
        {
            Item recordingServer = Configuration.Instance.GetItem(_selectedCamera.FQID.ServerId.Id, Kind.Server);
            VideoOS.Platform.SDK.Environment.ReloadConfiguration(recordingServer.FQID);
            _selectedCamera = Configuration.Instance.GetItem(_selectedCamera.FQID);
            ListPresets();
        }
        #endregion

        #region Sequences handling
        private void ListSequences()
        {
            // We do not want the selection changed callback to execute at this point, only when the end user changes the selection.
            _updatingFromCode = true;
            CameraSequences.Clear();
            SequenceDataSource sequenceDataSource = new SequenceDataSource(_selectedCamera);
            List<object> sequences = sequenceDataSource.GetData(DateTime.UtcNow, TimeSpan.FromHours(8), 25, TimeSpan.FromHours(0), 0);
            foreach (SequenceData sd in sequences)
            {
                CameraSequences.Add(sd.EventSequence);
            }

            _updatingFromCode = false;
        }

        private void SequenceSelectionChanged(object sender, RoutedEventArgs e)
        {
            VideoOSMessageBox.Show(this, "No action taken", "No action taken", "No action taken.\r\nThis sample only shows how to obtain the sequences\r\nRefer to the VideoViewer sample for display of recorded video", VideoOSMessageBox.Buttons.OK);
        }
        #endregion

        #region Other UI event handlers
        private void SelectCameraClicked(object sender, RoutedEventArgs e)
        {
            ItemPickerWpfWindow itemPicker = new ItemPickerWpfWindow()
            {
                KindsFilter = new List<Guid> { Kind.Camera },
                SelectionMode = SelectionModeOptions.AutoCloseOnSelect,
                Items = Configuration.Instance.GetItems(),
            };

            itemPicker.IsVisibleCallback = IsItemVisible;

            if (itemPicker.ShowDialog().Value)
            {
                _selectedCamera = itemPicker.SelectedItems.First();
                ListPresets();
                ListSequences();
                ShowCamera();
                OnPropertyChanged(nameof(CameraSelected));
                OnPropertyChanged(nameof(CameraName));
                OnPropertyChanged(nameof(ArePresetsSettable));
            }
        }

        private void ViewCameraPropertiesClicked(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> kvp in _selectedCamera.Properties)
            {
                sb.Append(string.Format("{0} : {1}\r\n", kvp.Key, kvp.Value));
            }

            string msg = sb.ToString();
            if (msg.Length > 0)
                VideoOSMessageBox.Show(this, "Camera Properties", "Camera Properties", sb.ToString(), VideoOSMessageBox.Buttons.OK);
            else
                VideoOSMessageBox.Show(this, "No properties for this camera", "No properties for this camera", string.Empty, VideoOSMessageBox.Buttons.OK);
        }

        private void CloseButtonClicked(object sender, RoutedEventArgs e)
        {
            VideoOS.Platform.SDK.Environment.RemoveAllServers();
            Close();
        }
        #endregion

        #region Helper methods

        private bool IsItemVisible(Item item)
        {
            return item.FQID.FolderType != FolderType.No ||
                item.Properties.ContainsKey("PTZ") && string.Compare(item.Properties["PTZ"], "Yes", true) == 0;
        }

        private void ShowCamera()
        {
            _imageViewer.CameraFQID = _selectedCamera.FQID;
            _imageViewer.EnableVisibleHeader = true;
            _imageViewer.EnableMouseControlledPtz = true;
            _imageViewer.EnableMousePtzEmbeddedHandler = true;
            _imageViewer.Initialize();
            _imageViewer.Connect();
            _imageViewer.Selected = true;
        }
        #endregion

    }
}
