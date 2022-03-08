using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Data;
using VideoOS.Platform.UI;

namespace ExportSample
{
    public partial class MainForm : Form
	{
		private string _path;
        private string _overlayImageFileName;
        private readonly List<Item> _cameraItems = new List<Item>();
        private IExporter _exporter;
        private readonly Timer _timer = new Timer { Interval = 100 };

        public MainForm()
		{
			InitializeComponent();
            comboBoxSampleRate.SelectedIndex = 0;
		}
         
		private void OnClose(object sender, EventArgs e)
		{
            if (_exporter != null)
            {
                _exporter.Cancel();
                _exporter.EndExport();
                _exporter.Close();
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
            string destPath = _path;

            // Get the related audio devices
            var audioSources = new List<Item>();
            var metadataSources = new List<Item>();
            if (checkBoxRelated.Checked)
            {
                foreach (var camera in _cameraItems)
                {
                    audioSources.AddRange(camera.GetRelated()
                        .Where(x => x.FQID.Kind == Kind.Microphone || x.FQID.Kind == Kind.Speaker));
                    metadataSources.AddRange(camera.GetRelated().Where(x => x.FQID.Kind == Kind.Metadata));
                }
            }
            
            

            if (dateTimePickerStart.Value > dateTimePickerEnd.Value)
            {
                MessageBox.Show("Start time need to be lower than end time");
                return;
            }

            if (radioButtonAVI.Checked)
            {
                if (textBoxVideoFilename.Text == "")
                {
                    MessageBox.Show("Please enter a filename for the AVI file.", "Enter Filename");
                    return;
                }
                AVIExporter aviExporter = new AVIExporter
                {
                    Filename = textBoxVideoFilename.Text,
                    Codec = (string)comboBoxCodec.SelectedItem,
                    AudioSampleRate = int.Parse(comboBoxSampleRate.SelectedItem.ToString())
                };

                if (checkBoxIncludeOverlayImage.Checked)
                {
                    if (_overlayImageFileName == null)
                    {
                        MessageBox.Show("Please select an image file for the overlay image.", "Select image file");
                        return;
                    }

                    Bitmap overlayImage = (Bitmap)Image.FromFile(_overlayImageFileName);
                    if (aviExporter.SetOverlayImage(overlayImage,
                            AVIExporter.VerticalOverlayPositionTop,
                            AVIExporter.HorizontalOverlayPositionLeft,
                            0.1,
                            false) == false)
                    {
                        MessageBox.Show("Failed to set overlay image, error: " + aviExporter.LastErrorString, "Overlay image");
                    }
                }
                _exporter = aviExporter;

                destPath = Path.Combine(_path, "Exported Images\\" + MakeStringPathValid(_cameraItems.FirstOrDefault().Name));
            }
            else if (radioButtonMKV.Checked)
            {
                if (textBoxVideoFilename.Text == "")
                {
                    MessageBox.Show("Please enter a filename for the MKV file.", "Enter Filename");
                    return;
                }

                if (_cameraItems.Count > 1)
                    MessageBox.Show("Warning, the MKV Exporter will only export the data from the first camera in the list");

                _exporter = new MKVExporter { Filename = textBoxVideoFilename.Text };
                destPath = Path.Combine(_path, "Exported Images\\" + MakeStringPathValid(_cameraItems.FirstOrDefault().Name));
            }
            else
            {
                if (checkBoxEncrypt.Checked && textBoxEncryptPassword.Text == "")
                {
                    MessageBox.Show("Please enter password to encrypt with.", "Enter Password");
                    return;
                }
                var dbExporter = new DBExporter(true)
                {
                    Encryption = checkBoxEncrypt.Checked,
                    EncryptionStrength = EncryptionStrength.AES128,
                    Password = textBoxEncryptPassword.Text,
                    SignExport = checkBoxSign.Checked,
                    PreventReExport = checkBoxReExport.Checked,
                    IncludeBookmarks = checkBoxIncludeBookmark.Checked
                };
                dbExporter.MetadataList.AddRange(metadataSources);

                _exporter = dbExporter;
            }

            _exporter.Init();
            _exporter.Path = destPath;
            _exporter.CameraList.AddRange(_cameraItems);
            _exporter.AudioList.AddRange(audioSources); 

            try
            {
                if (_exporter.StartExport(dateTimePickerStart.Value.ToUniversalTime(), dateTimePickerEnd.Value.ToUniversalTime()))
                {
                    _timer.Tick += ShowProgress;
                    _timer.Start();

                    buttonExport.Enabled = false;
                    buttonCancel.Enabled = true;
                }
                else
                {
                    int lastError = _exporter.LastError;
                    string lastErrorString = _exporter.LastErrorString;
                    labelError.Text = lastErrorString + "  ( " + lastError + " )";
                    _exporter.EndExport();
                }
            }
            catch (NoVideoInTimeSpanMIPException ex)
            {
                MessageBox.Show(ex.Message, "Start Export");
            }
            catch (Exception ex)
			{
				EnvironmentManager.Instance.ExceptionDialog("Start Export", ex);
			}
		}

        private static string MakeStringPathValid(string unsafeString)
        {
            char[] invalidCharacters = Path.GetInvalidFileNameChars();
            string result = unsafeString;
            foreach (var invalidCharacter in invalidCharacters)
            {
                result = result.Replace(invalidCharacter, '_');
            }
            return result;
        }


        /// <summary>
        /// Open the ItemPicker to let user select a camera
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddCamera_Click(object sender, EventArgs e)
        {
            ItemPickerForm form = new ItemPickerForm
            {
                KindFilter = Kind.Camera,
                AutoAccept = true
            };
            form.Init(Configuration.Instance.GetItems());
            if (form.ShowDialog() == DialogResult.OK)
            {
                var cameraItem = form.SelectedItem;
                _cameraItems.Add(cameraItem);
                listBoxCameras.Items.Add(cameraItem.Name);

                EnableExport();
            }
        }

        private void buttonRemoveCamera_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBoxCameras.SelectedIndex;
            if (selectedIndex != -1)
            {
                _cameraItems.RemoveAt(selectedIndex);
                listBoxCameras.Items.RemoveAt(selectedIndex);
            }
        }

        private void EnableExport()
        {
            if (_cameraItems.Count > 0 && _path != null)
            {
                buttonExport.Enabled = true;
            }
            else
            {
                buttonExport.Enabled = false;
            }
        }

        private void ShowProgress(object sender, EventArgs e)
        {
            if (_exporter != null)
            {
                int progress = _exporter.Progress;
                int lastError = _exporter.LastError;
                string lastErrorString = _exporter.LastErrorString;
                if (progress >= 0)
                {
                    progressBar.Value = progress;
                    if (progress == 100)
                    {
                        _timer.Stop();
                        labelError.Text = "Done";
                        _exporter.EndExport();
                        _exporter = null;
                        buttonCancel.Enabled = false;
                    }
                }
                if (lastError > 0)
                {
                    progressBar.Value = 0;
                    labelError.Text = lastErrorString + "  ( " + lastError + " )";
                    if (_exporter != null)
                    {
                        _exporter.EndExport();
                        _exporter = null;
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

				EnableExport();
			}
		}

		private void radioButtonAVI_CheckedChanged(object sender, EventArgs e)
		{
            textBoxVideoFilename.Enabled = radioButtonMKV.Checked || radioButtonAVI.Checked;
			comboBoxCodec.Enabled = radioButtonAVI.Checked;
            comboBoxSampleRate.Enabled = radioButtonAVI.Checked;
            checkBoxIncludeOverlayImage.Enabled = radioButtonAVI.Checked;
            buttonOverlayImage.Enabled = radioButtonAVI.Checked;

            BuildCodecList();
		}

        private void radioButtonMKV_CheckedChanged(object sender, EventArgs e)
        {
            textBoxVideoFilename.Enabled = radioButtonMKV.Checked || radioButtonAVI.Checked;
            comboBoxCodec.Enabled = radioButtonAVI.Checked;
        }

		private void BuildCodecList()
		{
			comboBoxCodec.Items.Clear();
			if (radioButtonAVI.Checked)
			{
				AVIExporter tempExporter = new AVIExporter { Width = 320, Height = 240, Filename = textBoxVideoFilename.Text };
				tempExporter.Init();
				string[] codecList = tempExporter.CodecList;
                tempExporter.Close();
                foreach (var name in codecList)
				{
					comboBoxCodec.Items.Add(name);				
				}
				comboBoxCodec.SelectedIndex = 0;
            }
			
		}
        private void buttonOverlayImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files|*.*|BMP|*.bmp|GIF|*.gif|JPG|*.jpg;*.jpeg|PNG|*.png|TIFF|*.tif;*.tiff";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _overlayImageFileName = openFileDialog.FileName;
                buttonOverlayImage.Text = _overlayImageFileName;
            }
        }

        private void OnCancel(object sender, EventArgs e)
        {
            _exporter?.Cancel();
        }

        private void OnEncryptChanged(object sender, EventArgs e)
        {
            if (checkBoxEncrypt.Checked)
            {
                textBoxEncryptPassword.Enabled = true;
            } else
            {
                textBoxEncryptPassword.Enabled = false;
                textBoxEncryptPassword.Text = "";
            }
        }

        private void OnDatabaseChanged(object sender, EventArgs e)
        {
            groupBoxDbSettings.Enabled = radioButtonDB.Checked;
        }

        private void _liftPrivacyMask_Click(object sender, EventArgs e)
        {
            if (Configuration.Instance.ServerFQID.ServerId.UserContext.PrivacyMaskLifted)
            {
                if (Configuration.Instance.ServerFQID.ServerId.UserContext.SetPrivacyMaskLifted(false))
                    _liftPrivacyMask.Text = "Lift privacy mask";                
            }
            else
            {
                if (Configuration.Instance.ServerFQID.ServerId.UserContext.SetPrivacyMaskLifted(true))
                    _liftPrivacyMask.Text = "Set privacy mask";
            }
        }

    }
}
