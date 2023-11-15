using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Data;
using VideoOS.Platform.UI;


namespace AudioExportSample
{
	public partial class MainForm : Form
	{
        private Item _item = null;
        private List<Item> _audioList = new List<Item>();
		private string _path = null;
		private Timer _timer = new Timer() {Interval = 100};
        WAVExporter _wavExporter;
        
		public MainForm()
		{
			InitializeComponent();
            comboBoxAudioSampleRates.Items.AddRange(new string[]{ "8000", "16000", "44100"});
            comboBoxAudioSampleRates.SelectedIndex = 0;
            BuildCodecList();
		}
         
		private void OnClose(object sender, EventArgs e)
		{
            if (_wavExporter != null)
            {
                _wavExporter.Cancel();
                _wavExporter.EndExport();
                _wavExporter.Close();
            }
		    VideoOS.Platform.SDK.Environment.RemoveAllServers();
			Close();
		}

		/// <summary>
		/// Now start the export
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExport_Click(object sender, EventArgs e)
		{
            List<Item> audioSources = new List<Item>();
            String destPath = _path;

            if (dateTimePickerStart.Value > dateTimePickerEnd.Value)
            {
                MessageBox.Show("Start time need to be lower than end time");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxAudioFileName.Text))
            {
                MessageBox.Show("Please enter a filename for the WAV file.", "Enter Filename");
                return;
            }

            destPath = Path.Combine(_path, "Exported Audios\\");
            _wavExporter = new WAVExporter()
            {
                FileName = textBoxAudioFileName.Text,
                Codec = (String)comboBoxCodec.SelectedItem,
                AudioSampleRate = Int32.Parse(comboBoxAudioSampleRates.SelectedItem.ToString()),
                Path = destPath,
                AudioList = _audioList,
        };
            
            _wavExporter.Init();
            bool isStarted = _wavExporter.StartExport(dateTimePickerStart.Value.ToUniversalTime(), dateTimePickerEnd.Value.ToUniversalTime());

            try
			{

				if (isStarted)
				{
					_timer.Tick += ShowProgress;
					_timer.Start();

					buttonExport.Enabled = false;
                    buttonCancel.Enabled = true;
                }
                else
				{
					int lastError = _wavExporter.LastError;
					string lastErrorString = _wavExporter.LastErrorString;
					labelError.Text = lastErrorString + "  ( " + lastError + " )";
					_wavExporter.EndExport();
				}
			} catch ( Exception ex)
			{
				EnvironmentManager.Instance.ExceptionDialog("Start Export", ex);
			}
		}


        /// <summary>
        /// Update progress 10 times a second
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowProgress(object sender, EventArgs e)
		{
			if (_wavExporter != null)
			{
				int progress = _wavExporter.Progress;
				int lastError = _wavExporter.LastError;
				string lastErrorString = _wavExporter.LastErrorString;
				if (progress >= 0)
				{
					progressBar.Value = progress;
					if (progress == 100)
					{
						_timer.Stop();
						labelError.Text = "Completed";
						_wavExporter.EndExport();
						_wavExporter = null;
                        buttonCancel.Enabled = false;
                    }
				}
				if (lastError > 0)
				{
					progressBar.Value = 0;
					labelError.Text = lastErrorString + "  ( " + lastError + " )";
					if (_wavExporter != null)
					{
						_wavExporter.EndExport();
						_wavExporter = null;
                        buttonCancel.Enabled = false;
                    }
				}
			}
		}

		/// <summary>
		/// Open the standard folder select dialog
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonDestination_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dialog = new FolderBrowserDialog();
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				_path = dialog.SelectedPath;
				buttonDestination.Text = _path;

				if (_audioList.Any() && _path != null)
					buttonExport.Enabled = true;
			}
		}

        private void BuildCodecList()
        {
            comboBoxCodec.Items.Clear();
            WAVExporter tempExporter = new WAVExporter();
            tempExporter.Init();
            string[] codecList = tempExporter.CodecList;
            tempExporter.Close();
            foreach (string name in codecList)
            {
                comboBoxCodec.Items.Add(name);
            }
            comboBoxCodec.SelectedIndex = 0;
        }

        private void OnCancel(object sender, EventArgs e)
        {
            if (_wavExporter != null)
                _wavExporter.Cancel();
        }


        private void buttonMicrophoneSelect_Click(object sender, EventArgs e)
        {
            AddItemByKind(Kind.Microphone);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddItemByKind(Kind.Speaker);
        }

        private void AddItemByKind(Guid kind)
        {
            ItemPickerWpfWindow itemPicker = new ItemPickerWpfWindow();
            itemPicker.KindsFilter = new List<Guid> { kind };
            itemPicker.Items = Configuration.Instance.GetItems(ItemHierarchy.UserDefined);

            if (itemPicker.ShowDialog().Value)
            {
                _item = itemPicker.SelectedItems.First();
                if (!_audioList.Contains(_item))
                {
                    _audioList.Add(_item);
                    if (_path != null)
                    {
                        buttonExport.Enabled = true;
                    }
                }

                string audioDevicesName = _item.Name;
                if (!listBoxAudioDevices.Items.Contains(audioDevicesName))
                {
                    listBoxAudioDevices.Items.Add(audioDevicesName);
                }
            }
        }
    }
}
