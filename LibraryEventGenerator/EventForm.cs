using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

using System.ServiceModel;
using VideoOS.Platform;
using VideoOS.Platform.Data;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.UI;

namespace LibraryEventGenerator
{
    public partial class EventForm : Form
    {
        Item _item = null;

        public EventForm()
        {
            InitializeComponent();

            groupBox1.Enabled = true;
        }
        /// <summary>
        /// This method will open a Form to select an Item.
        /// All Kind of items are allowed here - for real applications, you should set Kind filter to the Kind that makes sense.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelect(object sender, EventArgs e)
        {
            ItemPickerForm form = new ItemPickerForm();
            form.AutoAccept = true;
            form.Init(Configuration.Instance.GetItems());
            if (form.ShowDialog() == DialogResult.OK)
            {
                _item = form.SelectedItem;
                _buttonSelect.Text = _item.Name;
            }
        }

        private List<Image> _imageList = new List<Image>();
        private void OnImageSelect(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                String[] files = dialog.FileNames;
                _imageList = new List<Image>();
                foreach (String name in files)
                {
                    try
                    {
                        Image image = new Bitmap(name);
                        _imageList.Add(image);
                    }
                    catch
                    {
                    }
                }
                if (_imageList.Count > 0)
                {
                    pictureBoxSample.Image = new Bitmap(_imageList[0], pictureBoxSample.Size);
                }
            }
        }

        private void FireEventToRule_Click(object sender, EventArgs e)
        {
            if (_item == null)
            {
                MessageBox.Show("Please select an Item first...");
                return;
            }

            EventSource eventSource = new EventSource()
            {
                FQID = _item.FQID,
                Name = _item.Name
            };
            EventHeader eventHeader = new EventHeader()
            {
                ID = Guid.NewGuid(),
                Class = "NewEventToRule",
                Type = _textBoxEventType.Text,
                Timestamp = DateTime.Now,
                Message = _textBoxMessage.Text,
                Name = _textBoxEventName.Text,
                Source = eventSource
            };
            // In this sample the snap shots are being saved. This will make the 
            // the build-in AlarmPreview in Smart Client display them in the case where the event result in an alarm.
            SnapshotList snapshots = new SnapshotList();
            for (int ix = 0; ix < _imageList.Count; ix++)
            {
                snapshots.Add(new Snapshot()
                {
                    Image = convertImage(_imageList[ix])
                });
            }

            AnalyticsEvent analyticsEvent = new AnalyticsEvent()
            {
                EventHeader = eventHeader,
                SnapshotList = snapshots
            };

            EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(MessageId.Server.NewEventCommand) { Data = analyticsEvent });
        }

        private void FireSendAlarmClick(object sender, EventArgs e)
        {
            if (_item == null)
            {
                MessageBox.Show("Please select an Item first...");
                return;
            }

            EventSource eventSource = new EventSource()
            {
                FQID = _item.FQID,
                Name = _item.Name
            };
            EventHeader eventHeader = new EventHeader()
            {
                ID = Guid.NewGuid(),
                Class = "NewEventToRule",
                Type = _textBoxEventType.Text,
                Timestamp = DateTime.Now,
                Message = _textBoxMessage.Text,
                Name = _textBoxEventName.Text,
                Source = eventSource,
                Priority = 2,
                PriorityName = "Medium"
            };
            Alarm alarm = new Alarm()
            {
                EventHeader = eventHeader,
                StateName = "In progress",
                State = 4,
                AssignedTo = "test (\\test)"
                // Basic user with the name of test in this example
                // the string to use can be seen in the Smart Client dropdown

                // Other fields could be filled out, e.g. snapshotlist, objectList
            };

            // Send the Alarm directly to the EventServer, to store in the Alarm database. No event which could trigger a rule is being activated.
            EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(MessageId.Server.NewAlarmCommand) { Data = alarm });
        }

        private void OnClose(object sender, EventArgs e)
        {
            this.Close();
        }

        private static byte[] convertImage(Image image)
        {
            ImageConverter _imageConverter = new ImageConverter();
            byte[] byteArray = (byte[])_imageConverter.ConvertTo(image, typeof(byte[]));
            return byteArray;
        }
    }
}
