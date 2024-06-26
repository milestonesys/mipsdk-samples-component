using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Live;
using VideoOS.Platform.UI;
using VideoOS.Platform.Util;


namespace AudioRecorder
{
    public partial class MainForm : Form
    {
        #region private fields

        private Item _selectedItem;
        private PcmLiveSource _pcmLiveSource;

        private Stream _fileStream;
        private BinaryWriter _binaryWriter;

        private bool _running = false;

        #endregion

        #region construction and close

        public MainForm()
        {
            InitializeComponent();

            UpdateUiState();
        }

        private void OnClose(object sender, EventArgs e)
        {
            CloseMic();
            CloseFile();
            Close();
        }

        private void CloseMic()
        {
            try
            {
                if (_pcmLiveSource != null)
                {
                    // Close any current displayed PCM Live Source
                    _pcmLiveSource.LiveContentEvent -= PcmLiveContentEvent;
                    _pcmLiveSource.LiveStatusEvent -= PcmLiveStatusEvent;
                    _pcmLiveSource.Close();
                    _pcmLiveSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not close:" + ex.Message);
            }
        }

        private void CloseFile()
        {
            if (null != _binaryWriter)
            {
                _binaryWriter.Flush();
                _binaryWriter.Close();
                _binaryWriter = null;
            }

            if (null != _fileStream)
            {
//                _fileStream.Flush();
//                _fileStream.Close();
                _fileStream = null;
            }
        }

        #endregion

        private void OnButtonStartClick(object sender, EventArgs e)
        {
            _pcmLiveSource.LiveContentEvent += PcmLiveContentEvent;
            _pcmLiveSource.LiveStatusEvent += PcmLiveStatusEvent;
            _pcmLiveSource.LiveModeStart = _running = true;
            UpdateUiState();
        }

        private void OnButtonStopClick(object sender, EventArgs e)
        {
            _pcmLiveSource.LiveModeStart = _running = false;
            _pcmLiveSource.LiveContentEvent -= PcmLiveContentEvent;
            _pcmLiveSource.LiveStatusEvent -= PcmLiveStatusEvent;
            CloseFile();
            UpdateUiState();
        }

        private void OnButtonSelectMicClick(object sender, EventArgs e)
        {
            CloseMic();
            buttonSelectMic.Text = "...";

            ItemPickerWpfWindow itemPicker = new ItemPickerWpfWindow();
            itemPicker.KindsFilter = new List<Guid> { Kind.Microphone };
            itemPicker.SelectionMode = SelectionModeOptions.AutoCloseOnSelect;
            itemPicker.Items = Configuration.Instance.GetItems();

            if (itemPicker.ShowDialog().Value)
            {
                _selectedItem = itemPicker.SelectedItems.First();
                buttonSelectMic.Text = _selectedItem.Name;

                _pcmLiveSource = new PcmLiveSource(_selectedItem);
                try
                {
                    _pcmLiveSource.PcmSourceSettings.SamplingRate = 8000;
                    _pcmLiveSource.PcmSourceSettings.BitsPerSample = PcmSourceSettings.BitsPerSampleType.TwoBytesInt;
                    _pcmLiveSource.PcmSourceSettings.Channels = PcmSourceSettings.ChannelsType.Mono;
                    _pcmLiveSource.Init();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not Init:" + ex.Message);
                    CloseMic();
                }
            }

            UpdateUiState();
        }

        private void PcmLiveStatusEvent(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
        }

        private void PcmLiveContentEvent(object sender, EventArgs e)
        {
            var args = e as LiveContentPcmEventArgs;
            if ((null != args) &&
                (null != args.LiveContent) &&
                (null != args.LiveContent.Content))
            {
                _binaryWriter.Write(args.LiveContent.Content);
                args.LiveContent.Dispose();
            }
        }

        private void OnButtonFileClick(object sender, EventArgs e)
        {
            CloseFile();
            buttonFile.Text = "";

            if (DialogResult.OK == saveFileDialogData.ShowDialog())
            {
                _fileStream = saveFileDialogData.OpenFile();
                _binaryWriter = new BinaryWriter(_fileStream);

                buttonFile.Text = saveFileDialogData.FileName;
            }
        }

        private void UpdateUiState()
        {
            buttonStop.Enabled = _running;
            buttonStart.Enabled = (false == _running) && (null != _binaryWriter) && (null != _selectedItem);
        }

    }

}
