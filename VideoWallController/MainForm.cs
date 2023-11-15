using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.UI;

namespace VideoWallController
{
    public partial class MainForm : Form
    {
        ItemPickerWpfWindow itemPicker;
        Item _selectedItem;
        private VideoOS.Platform.Messaging.Message msg;
        private FQID _presetActivateFQID, 
            _monitorRemoveCamerasFQID, 
            _monitorSetLayoutAndCamerasFQID, _layoutSetLayoutAndCamerasFQID,
            _monitorShowTextFQID, 
            _layoutSetLayoutFQID, _monitorSetLayoutFQID, 
            _monitorSetCamerasFQID,
            _monitorApplyXmlFQID,
            _changeModeFQID;
        private decimal _positionInViewSetLayoutAndCameras, _positionInViewShowText, _positionInViewSetCameras;
        private Collection<FQID> _cameraSetLayoutAndCamerasFQIDCollection, _cameraRemoveCamerasFQIDCollection, _cameraSetCamerasFQIDCollection;
        private string _textShowText;
        private string _clientID;

        private MessageCommunication _messageCommunication;
        private object _msgComRec;

        public MainForm()
        {
            InitializeComponent();
            _clientID = $"{Environment.MachineName}-VideoWallControllerSample/{Process.GetCurrentProcess().Id}/{DateTime.UtcNow.Hour}:{DateTime.UtcNow.Minute}";
            changeModeSpeedComboBox.SelectedIndex = 2;
            playbackModeComboBox.SelectedIndex = 0;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            _cameraSetLayoutAndCamerasFQIDCollection = new Collection<FQID>();
            _cameraRemoveCamerasFQIDCollection = new Collection<FQID>();
            _cameraSetCamerasFQIDCollection = new Collection<FQID>();
            _positionInViewSetLayoutAndCameras = setPositionSetLayoutAndCamerasNumeric.Value;
            _positionInViewShowText = setPositionShowTextNumeric.Value;
            _positionInViewSetCameras = setPositionSetCamerasNumeric.Value;
            MessageCommunicationManager.Start(EnvironmentManager.Instance.MasterSite.ServerId);
            _messageCommunication = MessageCommunicationManager.Get(EnvironmentManager.Instance.MasterSite.ServerId);
            _msgComRec = _messageCommunication.RegisterCommunicationFilter(VideoWallMessageCommunicationHandler, new CommunicationIdFilter(MessageId.Control.VideoWallIndication));
        }

        private void ShowMessage(string message, VideoWallIndicationData data)
        {
            BeginInvoke(new MethodInvoker(delegate () {
                messageListenerListBox.Items.Add(new TagItem(message, data));
            }
            ));
        }

        private object VideoWallMessageCommunicationHandler(VideoOS.Platform.Messaging.Message message, FQID destination, FQID sender)
        {
            var data = message.Data as VideoWallIndicationData;
            var action = data.ActionId;
            var itemFQID = data.FQID;
            string itemKind = null;
            if (itemFQID.Kind == Kind.VideoWall)
                itemKind = "Smart wall";
            else if (itemFQID.Kind == Kind.VideoWallMonitor)
                itemKind = "Smart wall monitor";
            else if (itemFQID.Kind == Kind.View)
                itemKind = "View";
            if (string.Equals(action, "DELETED") || itemFQID.Kind == Kind.View)
                ShowMessage(string.Format(@"{0}  Message: {1} of GUID: {2} is {3}", DateTime.Now, itemKind, itemFQID.ObjectId.ToString(), action), data);
            else
                ShowMessage(string.Format(@"{0}  Message: {1} {2} is {3}", DateTime.Now, itemKind, Configuration.Instance.GetItem(itemFQID).Name, action), data);
            return null;
        }

        private void OnClose(object sender, EventArgs e)
        {
            _messageCommunication.UnRegisterCommunicationFilter(_msgComRec);
            VideoOS.Platform.SDK.Environment.RemoveAllServers();
            Close();
        }

        private void OnSelectedContentChanged(object sender, EventArgs e)
        {
            if (messageListenerListBox.SelectedItem != null)
            {
                TagItem tagItem = messageListenerListBox.SelectedItem as TagItem;
                if (tagItem != null)
                {
                    textBoxApplyXml.Text = tagItem.Data.ToXml();
                }
            }
        }


        private FQID SelectItem(Guid kind, Button button)
        {
            itemPicker = new ItemPickerWpfWindow()
            {
                KindsFilter = new List<Guid> { kind },
                SelectionMode = SelectionModeOptions.AutoCloseOnSelect,
        };

            if (kind == Kind.Camera)
            {
                itemPicker.Items = Configuration.Instance.GetItems();
            }
            else
            {
                itemPicker.Items = Configuration.Instance.GetItems(ItemHierarchy.SystemDefined);
            }

            if (itemPicker.ShowDialog().Value)
            {
                _selectedItem = itemPicker.SelectedItems.First();
                button.Text = _selectedItem.Name;
                return _selectedItem.FQID;
            }

            return null;        
        }

        private void SelectCameraToListBox(Button button, ListBox listBox, Collection<FQID> cameraFQIDCollection)
        {
            itemPicker = new ItemPickerWpfWindow()
            {
                KindsFilter = new List<Guid> { Kind.Camera },
                SelectionMode = SelectionModeOptions.AutoCloseOnSelect,
                Items = Configuration.Instance.GetItems()
            };

            if (itemPicker.ShowDialog().Value)
            {
                _selectedItem = itemPicker.SelectedItems.First();
                button.Text = "Select more cameras";
                cameraFQIDCollection.Add(_selectedItem.FQID);
                listBox.Items.Add(_selectedItem.Name);
            }
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown num = (NumericUpDown)sender;
            if (num.Name == "setPositionSetLayoutAndCamerasNumeric")
            {
                _positionInViewSetLayoutAndCameras = setPositionSetLayoutAndCamerasNumeric.Value;
                if (_monitorSetLayoutAndCamerasFQID != null && _cameraSetLayoutAndCamerasFQIDCollection.Count != 0 && _layoutSetLayoutAndCamerasFQID != null)
                    sendSetLayoutAndCamerasButton.Enabled = true;
            }
            else if (num.Name == "setPositionShowTextNumeric")
            {
                _positionInViewShowText = setPositionShowTextNumeric.Value;
                if (_monitorShowTextFQID != null && !string.IsNullOrEmpty(_textShowText) && _positionInViewShowText != default(decimal))
                    sendShowTextButton.Enabled = true;
            }
            else if (num.Name == "setPositionSetCamerasNumeric")
            { 
                _positionInViewSetCameras = setPositionSetCamerasNumeric.Value;
                if (_cameraSetCamerasFQIDCollection.Count != 0 && _monitorSetCamerasFQID != null)
                    sendSetCamerasButton.Enabled = true;
            }
        }

        private void showTextTextBox_TextChanged(object sender, EventArgs e)
        {
            _textShowText = showTextTextBox.Text;
            if (_monitorShowTextFQID != null && _positionInViewShowText != default(decimal) && !string.IsNullOrEmpty(_textShowText))
                sendShowTextButton.Enabled = true;
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch(btn.Name)
            {
                case "selectPresetActivatedButton":
                    _presetActivateFQID = SelectItem(Kind.VideoWallPreset, selectPresetActivatedButton);
                    sendPresetActivatedButton.Enabled = true;
                    break;
                case "sendPresetActivatedButton":
                    msg = new VideoOS.Platform.Messaging.Message(MessageId.Control.VideoWallPresetActivateCommand);
                    EnvironmentManager.Instance.SendMessage(msg, _presetActivateFQID);
                    sendPresetActivatedButton.Enabled = false;
                    selectPresetActivatedButton.Text = "Select preset";
                    break;
                case "selectCameraRemoveCamerasButton":
                    SelectCameraToListBox(selectCameraRemoveCamerasButton, cameraToRemoveRemoveCamerasListBox, _cameraRemoveCamerasFQIDCollection);
                    if (_monitorRemoveCamerasFQID != null)
                        sendRemoveCamerasButton.Enabled = true;
                    break;
                case "selectMonitorRemoveCamerasButton":
                    _monitorRemoveCamerasFQID= SelectItem(Kind.VideoWallMonitor, selectMonitorRemoveCamerasButton);
                    if(_cameraRemoveCamerasFQIDCollection.Count != 0)
                        sendRemoveCamerasButton.Enabled = true;
                    break;
                case "sendRemoveCamerasButton":
                    VideoWallRemoveCamerasCommandData removeCamerasData = new VideoWallRemoveCamerasCommandData
                    {
                        CameraFQIDList = _cameraRemoveCamerasFQIDCollection,
                    };
                    msg = new VideoOS.Platform.Messaging.Message(MessageId.Control.VideoWallRemoveCamerasCommand, removeCamerasData);
                    EnvironmentManager.Instance.SendMessage(msg, _monitorRemoveCamerasFQID);
                    sendRemoveCamerasButton.Enabled = false;
                    selectCameraRemoveCamerasButton.Text = "Select camera";
                    cameraToRemoveRemoveCamerasListBox.Items.Clear();
                    _cameraRemoveCamerasFQIDCollection.Clear();
                    break;
                case "selectMonitorSetLayoutAndCamerasButton":
                    _monitorSetLayoutAndCamerasFQID = SelectItem(Kind.VideoWallMonitor, selectMonitorSetLayoutAndCamerasButton);
                    if (_cameraSetLayoutAndCamerasFQIDCollection.Count != 0 && _layoutSetLayoutAndCamerasFQID != null && _positionInViewSetLayoutAndCameras != default(decimal))
                        sendSetLayoutAndCamerasButton.Enabled = true;
                    break;
                case "selectLayoutSetLayoutAndCamerasButton":
                    _layoutSetLayoutAndCamerasFQID = SelectItem(Kind.Layout, selectLayoutSetLayoutAndCamerasButton);
                    if (_monitorSetLayoutAndCamerasFQID != null && _cameraSetLayoutAndCamerasFQIDCollection.Count != 0 && _positionInViewSetLayoutAndCameras != default(decimal))
                        sendSetLayoutAndCamerasButton.Enabled = true;
                    break;
                case "selectCameraSetLayoutAndCamerasButton":
                    SelectCameraToListBox(selectCameraSetLayoutAndCamerasButton, camerasToShowSetLayoutAndCamerasListBox, _cameraSetLayoutAndCamerasFQIDCollection);
                    if (_monitorSetLayoutAndCamerasFQID != null && _layoutSetLayoutAndCamerasFQID != null && _positionInViewSetLayoutAndCameras != default(decimal))
                        sendSetLayoutAndCamerasButton.Enabled = true;
                    break;
                case "sendSetLayoutAndCamerasButton":
                    VideoWallSetLayoutAndCamerasCommandData layoutCamerasData = new VideoWallSetLayoutAndCamerasCommandData
                    {
                        CameraFQIDList = _cameraSetLayoutAndCamerasFQIDCollection,
                        LayoutFQID = _layoutSetLayoutAndCamerasFQID,
                        Position = (int)_positionInViewSetLayoutAndCameras
                    };
                    msg = new VideoOS.Platform.Messaging.Message(MessageId.Control.VideoWallSetLayoutAndCamerasCommand, layoutCamerasData);
                    EnvironmentManager.Instance.SendMessage(msg, _monitorSetLayoutAndCamerasFQID);
                    _cameraSetLayoutAndCamerasFQIDCollection.Clear();
                    selectCameraSetLayoutAndCamerasButton.Text = "Select camera";
                    camerasToShowSetLayoutAndCamerasListBox.Items.Clear();
                    sendSetLayoutAndCamerasButton.Enabled = false;
                    break;
                case "selectMonitorShowTextButton":
                    _monitorShowTextFQID = SelectItem(Kind.VideoWallMonitor, selectMonitorShowTextButton);
                    if (_textShowText != null && _positionInViewShowText != default(decimal))
                        sendShowTextButton.Enabled = true;
                    break;
                case "sendShowTextButton":
                    VideoWallShowTextCommandData textData = new VideoWallShowTextCommandData
                    {
                        Position = (int)_positionInViewShowText,
                        Text = _textShowText,
                    };
                    msg = new VideoOS.Platform.Messaging.Message(MessageId.Control.VideoWallShowTextCommand, textData);
                    EnvironmentManager.Instance.SendMessage(msg, _monitorShowTextFQID);
                    sendShowTextButton.Enabled = false;
                    showTextTextBox.Clear();
                    break;
                case "selectLayoutSetLayoutButton":
                    _layoutSetLayoutFQID = SelectItem(Kind.Layout, selectLayoutSetLayoutButton);
                    if (_monitorSetLayoutFQID != null)
                        sendSetLayoutButton.Enabled = true;
                    break;
                case "selectMonitorSetLayoutButton":
                    _monitorSetLayoutFQID = SelectItem(Kind.VideoWallMonitor, selectMonitorSetLayoutButton);
                    if (_layoutSetLayoutFQID != null)
                        sendSetLayoutButton.Enabled = true;
                    break;
                case "sendSetLayoutButton":
                    VideoWallSetLayoutCommandData layoutData = new VideoWallSetLayoutCommandData
                    {
                        LayoutFQID = _layoutSetLayoutFQID,
                    };
                    msg = new VideoOS.Platform.Messaging.Message(MessageId.Control.VideoWallSetLayoutCommand, layoutData);
                    EnvironmentManager.Instance.SendMessage(msg, _monitorSetLayoutFQID);
                    break;
                case "selectCameraSetCamerasButton":
                    SelectCameraToListBox(selectCameraSetCamerasButton, cameraToShowSetCamerasListBox, _cameraSetCamerasFQIDCollection);
                    if (_positionInViewSetCameras != default(decimal) && _monitorSetCamerasFQID != null)
                        sendSetCamerasButton.Enabled = true;
                    break;
                case "selectMonitorSetCamerasButton":
                    _monitorSetCamerasFQID = SelectItem(Kind.VideoWallMonitor, selectMonitorSetCamerasButton);
                    if (_cameraSetCamerasFQIDCollection.Count != 0 && _positionInViewSetCameras != default(decimal))
                        sendSetCamerasButton.Enabled = true;
                    break;
                case "sendSetCamerasButton":
                    VideoWallSetCamerasCommandData settings = new VideoWallSetCamerasCommandData
                    {
                        CameraFQIDList = _cameraSetCamerasFQIDCollection,
                        Position = (int)_positionInViewSetCameras,
                    };
                    msg = new VideoOS.Platform.Messaging.Message(MessageId.Control.VideoWallSetCamerasCommand, settings);
                    EnvironmentManager.Instance.SendMessage(msg, _monitorSetCamerasFQID);
                    _cameraSetCamerasFQIDCollection.Clear();
                    sendSetCamerasButton.Enabled = false;
                    cameraToShowSetCamerasListBox.Items.Clear();
                    selectCameraSetCamerasButton.Text = "Select camera";
                    break;
                case "selectMonitorApplyXmlButton":
                    _monitorApplyXmlFQID = SelectItem(Kind.VideoWallMonitor, selectMonitorApplyXmlButton);
                    if (_monitorApplyXmlFQID != null && numericUpDownApplyXml.Value != default(decimal))
                        buttonSendXml.Enabled = true;
                    break;
                case "buttonSendXml":
                    VideoWallApplyXmlCommandData xmlData = new VideoWallApplyXmlCommandData()
                    {
                        Position = (int)numericUpDownApplyXml.Value,
                        Xml = textBoxApplyXml.Text
                    };
                    msg = new VideoOS.Platform.Messaging.Message(MessageId.Control.VideoWallApplyXmlCommand, xmlData);
                    EnvironmentManager.Instance.SendMessage(msg, _monitorApplyXmlFQID);
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }
        }

        private void selectMonitorChangeModeButton_Click(object sender, EventArgs e)
        {
            _changeModeFQID = SelectItem(Kind.VideoWallMonitor, selectMonitorChangeModeButton);
            if (_changeModeFQID != null )
                changeModeButton.Enabled = true;
        }

        private void playbackRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            playbackDateTimePicker.Enabled = playbackRadioButton.Checked;
            playbackModeComboBox.Enabled = playbackRadioButton.Checked;
            changeModeSpeedComboBox.Enabled = playbackRadioButton.Checked;
        }

        private void changeModeButton_Click(object sender, EventArgs e)
        {
            string xmlCommand;
            if (liveRadioButton.Checked) 
            {
                xmlCommand = $"<MonitorLiveState><SourceId>{_clientID}</SourceId></MonitorLiveState>";
            }
            else
            {
                xmlCommand = $"<MonitorPlaybackState><SourceId>{_clientID}</SourceId><PlaybackMode>{playbackModeComboBox.Text}</PlaybackMode><SeekTime>{playbackDateTimePicker.Value.ToString("o")}</SeekTime><PlaySpeed>{changeModeSpeedComboBox.Text}</PlaySpeed></MonitorPlaybackState>";
            }
            var monitor = new VideoOS.Platform.ConfigurationItems.Monitor(_changeModeFQID);
            monitor.ApplyMonitorState(xmlCommand);
        }
    }

    internal class TagItem
    {
        internal VideoWallIndicationData Data;
        internal string Text;

        internal TagItem(String text, VideoWallIndicationData data)
        {
            Text = text;
            Data = data;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
