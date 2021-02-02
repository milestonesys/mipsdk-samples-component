using System;
using System.Windows.Forms;
using VideoOS.Platform.UI;
using VideoOS.Platform;
using VideoOS.Platform.Data;
using VideoOS.Platform.Messaging;

namespace TriggerAnalyticsEventSDK
{
	public partial class MainForm : Form
	{
		Item _selectedItem = null;

		public MainForm()
		{
			InitializeComponent();
		}

		private void btnSelectEventSource_Click(object sender, EventArgs e)
		{
			ItemPickerForm form = new ItemPickerForm();
			form.AutoAccept = true;
			form.Init(Configuration.Instance.GetItemsByKind(Kind.Camera, ItemHierarchy.SystemDefined));
			if (form.ShowDialog() == DialogResult.OK)
			{
				_selectedItem = form.SelectedItem;
				btnSelectEventSource.Text = _selectedItem.Name;
			}
		}

		private void btnSendAnalyticsEvent_Click(object sender, EventArgs e)
		{
			if (_selectedItem == null)
			{
				MessageBox.Show("Please select an event source first...");
				return;
			}

			EventSource eventSource = new EventSource()
			{
                // Send empty - it is possible to send without an eventsource, but the intended design is that there should be a source
                // the FQID is primamry used to match up the ObjectId with the a camera.
                FQID = _selectedItem.FQID,
                // If FQID is null, then the Name can be an IP address, and the event server will make a lookup to find the camera
                Name = _selectedItem.Name
			};

			EventHeader eventHeader = new EventHeader()
			{
				ID = Guid.NewGuid(),
				Type = "MyType",
				Timestamp = DateTime.Now,
				Message = txtAnalyticsEventName.Text,
                Source = eventSource,
				CustomTag = "TagFromC#"
			};

			AnalyticsEvent analyticsEvent = new AnalyticsEvent();
			analyticsEvent.EventHeader = eventHeader;
			analyticsEvent.Location = "Event location 1";
			analyticsEvent.Description = "Analytics event description.";
			analyticsEvent.Vendor = new Vendor();
			analyticsEvent.Vendor.Name = "My Smart Video";
            
			if (chkIncludeOverlay.Checked)
			{
				analyticsEvent.ObjectList = new AnalyticsObjectList();
				analyticsEvent.ObjectList.Add(GetRectangle());
			}

			EnvironmentManager.Instance.SendMessage(
				new VideoOS.Platform.Messaging.Message(MessageId.Server.NewEventCommand)
					{ Data = analyticsEvent });
		}

		private static AnalyticsObject GetRectangle()
		{
			AnalyticsObject aObject;
			TPolygon tPolygon;
			TColor tColor;
			aObject = new AnalyticsObject();
			aObject.Name = "SuspectArea";
			aObject.AlarmTrigger = true;
			aObject.Value = "A suspect item";
			aObject.Confidence = 0.9;
			aObject.Description = "Object description";
			tPolygon = new TPolygon();
			aObject.Polygon = tPolygon;

			tColor = new TColor();
			tColor.A = 255;
			tColor.R = 255;
			tColor.G = 255;
			tColor.B = 0;
			tPolygon.Color = tColor;

			PointList pointList = new PointList();
			TPoint tPoint;
			tPoint = new TPoint();
			tPoint.X = 0.3D;
			tPoint.Y = 0.3D;
			pointList.Add(tPoint);
			tPoint = new TPoint();
			tPoint.X = 0.6D;
			tPoint.Y = 0.3D;
			pointList.Add(tPoint);
			tPoint = new TPoint();
			tPoint.X = 0.6D;
			tPoint.Y = 0.6D;
			pointList.Add(tPoint);
			tPoint = new TPoint();
			tPoint.X = 0.3D;
			tPoint.Y = 0.6D;
			pointList.Add(tPoint);
			tPoint = new TPoint();
			tPoint.X = 0.3D;
			tPoint.Y = 0.3D;
			pointList.Add(tPoint);

			tPolygon.PointList = pointList;

			return aObject;
		}
	}
}
