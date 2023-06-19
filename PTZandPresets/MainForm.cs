using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.ConfigurationItems;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.UI;

namespace PTZandPresets
{
    public partial class MainForm : Form
    {
        private Item _camera;
        private VideoOS.Platform.Client.ImageViewerControl _imageViewerControl;

        public MainForm()
        {
            InitializeComponent();

            EnvironmentManager.Instance.RegisterReceiver(RecorderErrorHandler,
                new MessageIdFilter(MessageId.Control.RecorderErrorIndication));
        }

        private object RecorderErrorHandler(VideoOS.Platform.Messaging.Message message, FQID receiver, FQID sender)
        {
            RecorderErrorIndicationData data = message.Data as RecorderErrorIndicationData;
            toolStripStatusLabel1.Text = "Recorder error: (" + data.Command + ") " + data.ErrorText + ", " + data.RecorderWebInterfaceErrorCode +
                                         " - " + data.PtzErrorDetail;
            return null;
        }

        private void OnClose(object sender, EventArgs e)
        {
            VideoOS.Platform.SDK.Environment.RemoveAllServers();
            Close();
        }

        #region choose camera
        private void buttonSelectClick(object sender, EventArgs e)
        {
            ItemPickerForm form = new ItemPickerForm();
            form.KindFilter = Kind.Camera;
            form.AutoAccept = true;
            form.IsItemVisibleEvent += Form_IsItemVisibleEvent;
            form.Init(Configuration.Instance.GetItems());
            if (form.ShowDialog() == DialogResult.OK)
            {
                _camera = form.SelectedItem;
                buttonSelect.Text = _camera.Name;

                ListPresets();
                checkSetPTZ();
                ListSequences();
                ShowCamera();
                buttonViewProperties.Enabled = true;
            }
        }

        private void checkSetPTZ()
        {
            textBoxPresetName.Visible = false;
            buttonCreatePreset.Visible = false;
            buttonDeletePreset.Visible = false;
            buttonUpdatePreset.Visible = false;
            labelPresetAddName.Visible = false;
            if (isSettablePresets())
            {   
                    textBoxPresetName.Visible = true;
                    buttonCreatePreset.Visible = true;
                    buttonDeletePreset.Visible = true;
                    buttonUpdatePreset.Visible = true;
                    labelPresetAddName.Visible = true;
            }
        }

        /// <summary>
        /// We take a ptz preset and see if it is a preset imported from device, if it is we know that imported presets are used and we cannot set a preset.
        /// Imported ptz preset are set in the device.
        /// </summary>
        /// <returns></returns>
        private bool isSettablePresets()
        {
            bool result = true;
            Camera camera = new Camera(_camera.FQID);
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

        private void ShowCamera()
        {
            try
            {
                _imageViewerControl = VideoOS.Platform.ClientControl.Instance.GenerateImageViewerControl();
                _imageViewerControl.Dock = DockStyle.Fill;
                panel1.Controls.Clear();
                panel1.Controls.Add(_imageViewerControl);
                _imageViewerControl.CameraFQID = _camera.FQID;
                _imageViewerControl.EnableVisibleHeader = true; // May not be in 1.0a
                _imageViewerControl.EnableMouseControlledPtz = true;
                _imageViewerControl.EnableMousePtzEmbeddedHandler = true;
                _imageViewerControl.Initialize();
                _imageViewerControl.Connect();
                _imageViewerControl.Selected = true;
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Runtime DLLs are missing\r\nPlease refer to the MIP SDK documentation: Home > Component Integration > Samples Index > Video Viewer");
                Close();
            }
        }

        private void Form_IsItemVisibleEvent(ItemPickerForm.IsItemVisibleEventArgs e)
        {
            e.Visible = e.Item.FQID.FolderType != FolderType.No || e.Item.Properties.ContainsKey("PTZ") && string.Compare(e.Item.Properties["PTZ"], "Yes", true) == 0;
        }
        #endregion

        #region sequences

        private void ListSequences()
        {
            VideoOS.Platform.Data.SequenceDataSource ds = new VideoOS.Platform.Data.SequenceDataSource(_camera);
            List<object> dslist = ds.GetData(DateTime.UtcNow, TimeSpan.FromHours(8), 25, TimeSpan.FromHours(0), 0);
            if (dslist == null || dslist.Count == 0)
            {
                comboBoxSequences.Items.Add("No sequences in the last 8 hours");
            }
            else
            {
                comboBoxSequences.DisplayMember = "StartDateTime";
                foreach (VideoOS.Platform.Data.SequenceData sd in dslist)
                {
                    comboBoxSequences.Items.Add(sd.EventSequence);
                }
            }

            // We do not want the selection changed callback to execute at this point, only when the end user changes the selection.
            comboBoxSequences.SelectedIndexChanged -= new System.EventHandler(comboBoxSequences_SelectedIndexChanged);
            comboBoxSequences.SelectedIndex = 0;
            comboBoxSequences.SelectedIndexChanged += new System.EventHandler(comboBoxSequences_SelectedIndexChanged);
            comboBoxSequences.Enabled = true;
        }

        private void comboBoxSequences_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("No action taken.\r\nThis sample only shows how to obtain the sequences\r\nRefer to the VideoViewer sample for display of recorded video");
        }
        #endregion

        #region presets
        private void ListPresets()
        {
            comboBoxPresets.Items.Clear();
            List<VideoOS.Platform.Item> children = _camera.GetChildren();
            if (children == null || children.Count == 0)
            {
                comboBoxPresets.Items.Add("No presets found");
                comboBoxPresets.Enabled = false;
            }
            else
            {
                comboBoxPresets.DisplayMember = "Name";
                foreach (VideoOS.Platform.Item child in children)
                {
                    comboBoxPresets.Items.Add(child);
                }
            }

            // We do not want the selection changed callback to execute at this point, only when the end user changes the selection.
            comboBoxPresets.SelectedIndexChanged -= new System.EventHandler(comboBoxPresets_SelectedIndexChanged);
            comboBoxPresets.SelectedIndex = 0;
            comboBoxPresets.SelectedIndexChanged += new System.EventHandler(comboBoxPresets_SelectedIndexChanged);
            comboBoxPresets.Enabled = true;
        }

        private void comboBoxPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            // This is the Item which represents the preset we selected in the combo box.
            VideoOS.Platform.Item item = comboBoxPresets.SelectedItem as VideoOS.Platform.Item;

            if (item != null)
            {
                // This constucts a "trigger" message
                VideoOS.Platform.Messaging.Message triggerMessage =
                    new VideoOS.Platform.Messaging.Message(MessageId.Control.TriggerCommand);

                // This sends the trigger message to the preset, eventually causing the camera to actually move.
                EnvironmentManager.Instance.SendMessage(triggerMessage, item.FQID);

                buttonUpdatePreset.Enabled = true;
                buttonDeletePreset.Enabled = true;
            }
        }
        
        private void buttonCreatePreset_Click(object sender, EventArgs e)
        {
            //get current coordinates
            System.Collections.ObjectModel.Collection<object> objResult = EnvironmentManager.Instance.SendMessage(
            new VideoOS.Platform.Messaging.Message(MessageId.Control.PTZGetAbsoluteRequest), _camera.FQID);

            PTZGetAbsoluteRequestData datRequestData = (PTZGetAbsoluteRequestData)objResult[0];
            double pan = datRequestData.Pan;
            double tilt = datRequestData.Tilt;
            double zoom = datRequestData.Zoom;
            objResult.Clear();
            try
            {
                Camera camera = new Camera(_camera.FQID);
                PtzPresetFolder folder = camera.PtzPresetFolder;
                folder.AddPtzPreset(textBoxPresetName.Text, "", pan, tilt, zoom);
            }
            catch (Exception ex)
            {
                EnvironmentManager.Instance.Log(true, "Create Preset", ex.Message);
                MessageBox.Show(ex.Message, "Exception in Create preset", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            refreshList();
            textBoxPresetName.Text = "new preset name";
        }

        private void OnUpdatePreset(object sender, EventArgs e)
        {
            //get current coordinates
            System.Collections.ObjectModel.Collection<object> objResult = EnvironmentManager.Instance.SendMessage(
            new VideoOS.Platform.Messaging.Message(MessageId.Control.PTZGetAbsoluteRequest), _camera.FQID);

            PTZGetAbsoluteRequestData datRequestData = (PTZGetAbsoluteRequestData)objResult[0];
            double pan = datRequestData.Pan;
            double tilt = datRequestData.Tilt;
            double zoom = datRequestData.Zoom;
            objResult.Clear();

            Item currentlySelectedPresetItem = comboBoxPresets.SelectedItem as Item;
            string currentlySelectedPresetName = currentlySelectedPresetItem.Name;

            try
            {
                Camera camera = new Camera(_camera.FQID);
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
                MessageBox.Show(ex.Message, "Exception in Update preset", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void OnDeletePreset(object sender, EventArgs e)
        {
            Item currentlySelectedPresetItem = comboBoxPresets.SelectedItem as Item;
            string currentlySelectedPresetName = currentlySelectedPresetItem.Name;
            try
            {
                Camera camera = new Camera(_camera.FQID);
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
                MessageBox.Show(ex.Message, "Exception in Remove preset", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            refreshList();
        }

        /// <summary>
        /// When we have added or deleted a PTZ preset the configuration is no longer up to date.
        /// Specifically the _camera.GetChildren() will not return the right PTZ Presets.
        /// In this method we make sure the configuration is refreshed (re-read from server),
        /// reload configuration is done pertaining to the recording server to which the camera belongs
        /// and _camera item is renewed.
        /// Finally we re-write the list of preset refreshing the combobox using the ListPresets() method that is used to fill it at start-up also.
        /// </summary>
        private void refreshList()
        {
            Item rsItem = Configuration.Instance.GetItem(_camera.FQID.ServerId.Id, Kind.Server);
            VideoOS.Platform.SDK.Environment.ReloadConfiguration(rsItem.FQID);
            _camera = Configuration.Instance.GetItem(_camera.FQID);
            ListPresets();
        }
        #endregion

        #region move in steps
        private void buttonUp_Click(object sender, EventArgs e)
        {
            VideoOS.Platform.Messaging.Message msg = new VideoOS.Platform.Messaging.Message(MessageId.Control.PTZMoveCommand, VideoOS.Platform.Messaging.PTZMoveCommandData.Up);
            EnvironmentManager.Instance.PostMessage(msg, _camera.FQID);
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            VideoOS.Platform.Messaging.Message msg = new VideoOS.Platform.Messaging.Message(MessageId.Control.PTZMoveCommand, VideoOS.Platform.Messaging.PTZMoveCommandData.Down);
            EnvironmentManager.Instance.PostMessage(msg, _camera.FQID);
        }

        private void buttonleft_Click(object sender, EventArgs e)
        {
            VideoOS.Platform.Messaging.Message msg = new VideoOS.Platform.Messaging.Message(MessageId.Control.PTZMoveCommand, VideoOS.Platform.Messaging.PTZMoveCommandData.Left);
            EnvironmentManager.Instance.SendMessage(msg, _camera.FQID);
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            VideoOS.Platform.Messaging.Message msg = new VideoOS.Platform.Messaging.Message(MessageId.Control.PTZMoveCommand, VideoOS.Platform.Messaging.PTZMoveCommandData.Right);
            EnvironmentManager.Instance.SendMessage(msg, _camera.FQID);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bitmap bmp = this._imageViewerControl.GetCurrentDisplayedImageAsBitmap();
            if (bmp == null)
                return;

            PTZCenterCommandData datPTZCenterCommandData = new PTZCenterCommandData();

            datPTZCenterCommandData.CenterX = Convert.ToDouble(bmp.Width / 2 + 10);
            datPTZCenterCommandData.CenterY = Convert.ToDouble(bmp.Height / 2 + 10);
            datPTZCenterCommandData.RefWidth = Convert.ToDouble(bmp.Width);
            datPTZCenterCommandData.RefHeight = Convert.ToDouble(bmp.Height);

            System.Collections.ObjectModel.Collection<object> objResult = EnvironmentManager.Instance.SendMessage(
            new VideoOS.Platform.Messaging.Message(MessageId.Control.PTZGetAbsoluteRequest), _camera.FQID);

            PTZGetAbsoluteRequestData datRequestData = (PTZGetAbsoluteRequestData)objResult[0];
            datPTZCenterCommandData.Zoom = -1.0;// datRequestData.Zoom;
            objResult.Clear();

            EnvironmentManager.Instance.SendMessage(
            new VideoOS.Platform.Messaging.Message(MessageId.Control.PTZCenterCommand, datPTZCenterCommandData), _camera.FQID);

        }
        #endregion

        #region Continuesly move control 2
        // using PTZMoveStartCommandData2
        // It is recommended that Pan Tilt Zoom is either -1, 0, 1 and PanSpeed, TiltSpeed, ZoomSpeed is in range 0 to 1
        // Scroll bars are used to examplify this
        private void trackBarPan_Scroll(object sender, EventArgs e)
        {
            System.Globalization.NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign;
            if (t2Pan.Text == "") t2Pan.Text = "0.0";
            double value1 = ((double)trackBarPan.Value);
            double value; // = Convert.ToDouble(t2Pan.Text);
            if (double.TryParse(t2Pan.Text, style, System.Globalization.CultureInfo.InvariantCulture, out value))
            {
                if (Math.Round(value1) != Math.Round(value)) t2Pan.Text = value1.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        private void trackBarPanS_ValueChanged(object sender, EventArgs e)
        {
            System.Globalization.NumberStyles style = NumberStyles.AllowDecimalPoint;
            if (t2PanSpeed.Text == "") t2PanSpeed.Text = "0.0";
            double value1 = ((double)trackBarPanS.Value) / 10.0;
            double value; // = Convert.ToDouble(t2Pan.Text);
            if (double.TryParse(t2PanSpeed.Text, style, System.Globalization.CultureInfo.InvariantCulture, out value))
            {
                if (Math.Round(value1 * 10.0) != Math.Round(value * 10.0)) t2PanSpeed.Text = value1.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture);
            }

        }

        private void t2PanSpeed_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Globalization.NumberStyles style = NumberStyles.AllowDecimalPoint;
            if (t2PanSpeed.Text == "") t2PanSpeed.Text = "0.0";
            double value; // = Convert.ToDouble(t2Pan.Text);
            if (double.TryParse(t2PanSpeed.Text, style, System.Globalization.CultureInfo.InvariantCulture, out value))
            {
                if (value >= 0.0 && value <= 1.0)
                {
                    value = Math.Round(value * 10.0);
                    trackBarPanS.Value = (int)value;
                }
                else
                {
                    e.Cancel = true;
                    t2PanSpeed.SelectAll();
                }
            }
            else
            {
                e.Cancel = true;
                t2PanSpeed.SelectAll();
            }
        }

        private void t2Pan_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Globalization.NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign;
            if (t2Pan.Text == "") t2Pan.Text = "0.0";
            double value; // = Convert.ToDouble(t2Pan.Text);
            if (double.TryParse(t2Pan.Text, style, System.Globalization.CultureInfo.InvariantCulture, out value))
            {
                if (value >= -1.0 && value <= 1.0)
                {
                    value = Math.Round(value);
                    trackBarPan.Value = (int)value;
                }
                else
                {
                    e.Cancel = true;
                    t2Pan.SelectAll();
                }
            }
            else
            {
                e.Cancel = true;
                t2Pan.SelectAll();
            }
        }

        private void trackBarTilt_Scroll(object sender, EventArgs e)
        {
            System.Globalization.NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign;
            if (t2Tilt.Text == "") t2Tilt.Text = "0.0";
            double value1 = ((double)trackBarTilt.Value);
            double value; // = Convert.ToDouble(t2Tilt.Text);
            if (double.TryParse(t2Tilt.Text, style, System.Globalization.CultureInfo.InvariantCulture, out value))
            {
                if (Math.Round(value1) != Math.Round(value)) t2Tilt.Text = value1.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        private void trackBarTiltS_ValueChanged(object sender, EventArgs e)
        {
            System.Globalization.NumberStyles style = NumberStyles.AllowDecimalPoint;
            if (t2TiltSpeed.Text == "") t2TiltSpeed.Text = "0.0";
            double value1 = ((double)trackBarTiltS.Value) / 10.0;
            double value; // = Convert.ToDouble(t2Tilt.Text);
            if (double.TryParse(t2TiltSpeed.Text, style, System.Globalization.CultureInfo.InvariantCulture, out value))
            {
                if (Math.Round(value1 * 10.0) != Math.Round(value * 10.0)) t2TiltSpeed.Text = value1.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture);
            }

        }

        private void t2TiltSpeed_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Globalization.NumberStyles style = NumberStyles.AllowDecimalPoint;
            if (t2TiltSpeed.Text == "") t2TiltSpeed.Text = "0.0";
            double value; // = Convert.ToDouble(t2Tilt.Text);
            if (double.TryParse(t2TiltSpeed.Text, style, System.Globalization.CultureInfo.InvariantCulture, out value))
            {
                if (value >= 0.0 && value <= 1.0)
                {
                    value = Math.Round(value * 10.0);
                    trackBarTiltS.Value = (int)value;
                }
                else
                {
                    e.Cancel = true;
                    t2TiltSpeed.SelectAll();
                }
            }
            else
            {
                e.Cancel = true;
                t2TiltSpeed.SelectAll();
            }
        }

        private void t2Tilt_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Globalization.NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign;
            if (t2Tilt.Text == "") t2Tilt.Text = "0.0";
            double value; // = Convert.ToDouble(t2Tilt.Text);
            if (double.TryParse(t2Tilt.Text, style, System.Globalization.CultureInfo.InvariantCulture, out value))
            {
                if (value >= -1.0 && value <= 1.0)
                {
                    value = Math.Round(value);
                    trackBarTilt.Value = (int)value;
                }
                else
                {
                    e.Cancel = true;
                    t2Tilt.SelectAll();
                }
            }
            else
            {
                e.Cancel = true;
                t2Tilt.SelectAll();
            }
        }

        private void trackBarZoom_Scroll(object sender, EventArgs e)
        {
            System.Globalization.NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign;
            if (t2Zoom.Text == "") t2Zoom.Text = "0.0";
            double value1 = ((double)trackBarZoom.Value);
            double value; // = Convert.ToDouble(t2Zoom.Text);
            if (double.TryParse(t2Zoom.Text, style, System.Globalization.CultureInfo.InvariantCulture, out value))
            {
                if (Math.Round(value1) != Math.Round(value)) t2Zoom.Text = value1.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        private void trackBarZoomS_ValueChanged(object sender, EventArgs e)
        {
            System.Globalization.NumberStyles style = NumberStyles.AllowDecimalPoint;
            if (t2ZoomSpeed.Text == "") t2ZoomSpeed.Text = "0.0";
            double value1 = ((double)trackBarZoomS.Value) / 10.0;
            double value; // = Convert.ToDouble(t2Zoom.Text);
            if (double.TryParse(t2ZoomSpeed.Text, style, System.Globalization.CultureInfo.InvariantCulture, out value))
            {
                if (Math.Round(value1 * 10.0) != Math.Round(value * 10.0)) t2ZoomSpeed.Text = value1.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture);
            }

        }

        private void t2ZoomSpeed_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Globalization.NumberStyles style = NumberStyles.AllowDecimalPoint;
            if (t2ZoomSpeed.Text == "") t2ZoomSpeed.Text = "0.0";
            double value; // = Convert.ToDouble(t2Zoom.Text);
            if (double.TryParse(t2ZoomSpeed.Text, style, System.Globalization.CultureInfo.InvariantCulture, out value))
            {
                if (value >= 0.0 && value <= 1.0)
                {
                    value = Math.Round(value * 10.0);
                    trackBarZoomS.Value = (int)value;
                }
                else
                {
                    e.Cancel = true;
                    t2ZoomSpeed.SelectAll();
                }
            }
            else
            {
                e.Cancel = true;
                t2ZoomSpeed.SelectAll();
            }
        }

        private void t2Zoom_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Globalization.NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign;
            if (t2Zoom.Text == "") t2Zoom.Text = "0.0";
            double value; // = Convert.ToDouble(t2Zoom.Text);
            if (double.TryParse(t2Zoom.Text, style, System.Globalization.CultureInfo.InvariantCulture, out value))
            {
                if (value >= -1.0 && value <= 1.0)
                {
                    value = Math.Round(value);
                    trackBarZoom.Value = (int)value;
                }
                else
                {
                    e.Cancel = true;
                    t2Zoom.SelectAll();
                }
            }
            else
            {
                e.Cancel = true;
                t2Zoom.SelectAll();
            }
        }

        private void buttonStartMove_Click(object sender, EventArgs e)
        {
            PTZMoveStartCommandData2 data = new PTZMoveStartCommandData2();
            Double.TryParse(t2Pan.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out data.Pan);
            Double.TryParse(t2Tilt.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out data.Tilt);
            Double.TryParse(t2Zoom.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out data.Zoom);
            Double.TryParse(t2PanSpeed.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out data.PanSpeed);
            Double.TryParse(t2TiltSpeed.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out data.TiltSpeed);
            Double.TryParse(t2ZoomSpeed.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out data.ZoomSpeed);
            EnvironmentManager.Instance.SendMessage(
                new VideoOS.Platform.Messaging.Message(MessageId.Control.PTZMoveStartCommand, data), _camera.FQID);
        }

        private void buttonStopMove_Click(object sender, EventArgs e)
        {
            EnvironmentManager.Instance.SendMessage(
                new VideoOS.Platform.Messaging.Message(MessageId.Control.PTZMoveStopCommand), _camera.FQID);
        }

        #endregion

        #region Absolute PTZ
        private void buttonGetAbs_Click(object sender, EventArgs e)
        {
            System.Collections.ObjectModel.Collection<object> objResult = EnvironmentManager.Instance.SendMessage(
                        new VideoOS.Platform.Messaging.Message(MessageId.Control.PTZGetAbsoluteRequest), _camera.FQID);

            PTZGetAbsoluteRequestData datRequestData = (PTZGetAbsoluteRequestData)objResult[0];
            textBoxGetAbsPan.Text = datRequestData.Pan.ToString(System.Globalization.CultureInfo.InvariantCulture);
            textBoxGetAbsTilt.Text = datRequestData.Tilt.ToString(System.Globalization.CultureInfo.InvariantCulture);
            textBoxGetAbsZoom.Text = datRequestData.Zoom.ToString(System.Globalization.CultureInfo.InvariantCulture);
            objResult.Clear();
        }

        private void buttonSendAbs_Click(object sender, EventArgs e)
        {
            PTZMoveAbsoluteCommandData adata = new PTZMoveAbsoluteCommandData();
            Double.TryParse(textBoxGetAbsPan.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out adata.Pan);
            Double.TryParse(textBoxGetAbsTilt.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out adata.Tilt);
            Double.TryParse(textBoxGetAbsZoom.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out adata.Zoom);

            EnvironmentManager.Instance.SendMessage(
                new VideoOS.Platform.Messaging.Message(MessageId.Control.PTZMoveAbsoluteCommand, adata), _camera.FQID);
        }
        #endregion

        #region other controls
        private void buttonViewProperties_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> kvp in _camera.Properties)
            {
                sb.Append(string.Format("{0} : {1}\r\n", kvp.Key, kvp.Value));
            }

            string msg = sb.ToString();
            if (msg.Length > 0)
                System.Windows.Forms.MessageBox.Show(sb.ToString(), "Camera Properties");
            else
                System.Windows.Forms.MessageBox.Show("No properties for this camera");
        }

        private void OnTiltReverse(object sender, EventArgs e)
        {
            EnvironmentManager.Instance.EnvironmentOptions["PtzReverseTiltForContinuesMoves"] =
                checkBox1.Checked.ToString();
        }
        #endregion

    }
}
