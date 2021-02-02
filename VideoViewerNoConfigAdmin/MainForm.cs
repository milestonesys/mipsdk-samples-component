using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using VideoOS.Platform;

namespace VideoViewerNoConfigAdmin
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

        void OnLoad(object sender, EventArgs e)
        {
            //Loop through all cameras and generate XML.

            Directory.CreateDirectory("C:\\CameraXml\\");       //We store camera xml here!!
            List<Item> cameras = FindAllCameras();
            foreach (Item camera in cameras)
            {
                listView1.Items.Add(camera.Name);
                String xml = VideoOS.Platform.SDK.Util.ConfigUtil.GenerateCameraConfigurationXml(camera.FQID);
                System.IO.File.WriteAllText("C:\\CameraXml\\"+camera.Name+".xml", xml);    //NOTE - no funny characters in camera name!!                
            }
            Directory.CreateDirectory("C:\\MicrophoneXml\\");       //We store microphone xml here!!
            List<Item> microphones = FindAllMicrophones();
            foreach (Item mic in microphones)
            {
                listView1.Items.Add(mic.Name);
                String xml = VideoOS.Platform.SDK.Util.ConfigUtil.GenerateMicrophoneConfigurationXml(mic.FQID);
                System.IO.File.WriteAllText("C:\\MicrophoneXml\\" + mic.Name+".xml", xml);    //NOTE - no funny characters in camera name!!                
            }
        }

		private void OnClose(object sender, EventArgs e)
		{
			Close();
		}

	    private List<Item> FindAllCameras()
	    {
            List<Item> items = new List<Item>();
            foreach (Item item in Configuration.Instance.GetItems(ItemHierarchy.SystemDefined))
            {
                foreach (Item child in item.GetChildren())
                    AddCameras(child, ref items);
            }
	        return items;
	    }

	    private void AddCameras(Item item, ref List<Item> cameras)
	    {
	        if (item.FQID.Kind == Kind.Camera && item.FQID.FolderType == FolderType.No)
                cameras.Add(item);
            else if (item.HasChildren != VideoOS.Platform.HasChildren.No)
            {
                foreach (Item child in item.GetChildren())
                    AddCameras(child, ref cameras);                
            }
	    }

        private List<Item> FindAllMicrophones()
        {
            List<Item> items = new List<Item>();
            foreach (Item item in Configuration.Instance.GetItems(ItemHierarchy.SystemDefined))
            {
                foreach (Item child in item.GetChildren())
                    AddMicrophone(child, ref items);
            }
            return items;
        }

        private void AddMicrophone(Item item, ref List<Item> cameras)
        {
            if (item.FQID.Kind == Kind.Microphone && item.FQID.FolderType == FolderType.No)
                cameras.Add(item);
            else if (item.HasChildren != VideoOS.Platform.HasChildren.No)
            {
                foreach (Item child in item.GetChildren())
                    AddMicrophone(child, ref cameras);
            }
        }

	}
}
