using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ConfigAPIClient.UI
{

	public class Icons
	{
		public static ImageList IconList = new ImageList();
		public static ImageList IconListBlack = new ImageList();

		public static int ServerIconIx = 0;
		public static int FolderIconIx = 1;
		public static int AlertIconIx = 2;
		public static int InputIconIx = 3;
		public static int OutputIconIx = 4;
		public static int MatrixIconIx = 5;
		public static int TransactIconIx = 6;
		public static int PresetIconIx = 7;
		public static int CameraIconIx = 8;
		public static int CustomEventIconIx = 9;
		public static int SpeakerIconIx = 10;
		public static int MicrophoneIconIx = 11;
		public static int RecorderIconIx = 12;
		public static int ViewGroupIx = 13;
		public static int GroupIx = 14;
		public static int VideoWallGroupIx = 15;
		public static int VideoWallIx = 16;
		public static int ViewIx = 17;
		public static int ViewItemIx = 18;

		public static int PluginIx = 19;
		public static int SDK_VSIx = 20;
		public static int SDK_GeneralIx = 21;
		public static int SDK_ToolIx = 22;

		public static int WindowIx = 23;

		public static int RoleIx = 24;
		public static int UserIx = 25;

		public static int WorkSpaceIx = 26;
		public static int TimeProfileIx = 27;
        public static int HardwareIx = 28;
	    public static int MetadataIconIx = 29;

        public static int BasicUserIx = 30;
        public static int GisMapLocationIconIx = 31;
        public static int AlarmIconIx = 32;
        public static int LprMatchListIconIx = 33;
        public static int AccessControlSystemIconIx = 34;
        public static int AccessControlDoorIconIx = 35;
        public static int RuleIconIx = 36;
        public static int LicenseIconIx = 37;
        public static int SettingsIx = 38;
        public static int MulticastIx = 39;
        public static int FailoverGroupIx = 40;
        public static int FailoverServerIx = 41;
        public static int ClientProfileIx = 42;

        public static Dictionary<String, int> ObjectTypeToIndex = new Dictionary<string, int>();


		public static void Init()
		{
			try
			{
				IconList = new ImageList();
				IconListBlack = new ImageList();
				IconListBlack.ImageSize = new Size(16, 16);
				Assembly assembly = Assembly.GetExecutingAssembly();
				string ProjectName = assembly.GetName().Name;

				IconList.ImageSize = new Size(16, 16);
				IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.Server.png")));
				IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.folder_closed.ico")));
				IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.alert.ico")));
				IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.input.ico")));
				IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.output.ico")));
				IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.matrix.ico")));
				IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.TransactSource.ico")));
				IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.Presets.ico")));
				IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.camera.ico")));
				IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.event.ico")));
				IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.speaker.ico")));
				IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.microphone.ico")));
				IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.Recorder.ico")));
				IconList.Images.Add(
					new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.View group closed 16 n p.png")));
				IconList.Images.Add(
					new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.Folder Closed Flat 16 n p.png")));
				IconList.Images.Add(
					new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.Clear Video Wall View.png")));
				IconList.Images.Add(
					new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.Video Wall 16 n i8.ico")));
				IconList.Images.Add(
					new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.Single View 16 h p.png")));
				IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.ViewItem.ico")));

				IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.PlugIn.bmp")));
				IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.SDK_VS.ico")));
				IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.SDK_general.ico")));
				IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.SDK_tool.ico")));

				IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.Window 16 h p.png")));
				IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.Role.ico")));
				IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.User.ico")));
				IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.PlugIn.bmp")));
				IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.TimeProfile.ico")));
                IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.Hardware.ico")));
                IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.Metadata_device.ico")));

                IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.BasicUser.ico")));
                IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.GisMapLocation.png")));
                IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.alarm-14x14.png")));
                IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.LicensePlateList_16x16.png")));
                IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.AccessControlSystem.png")));
                IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.AccessControlDoor.png")));
                IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.Rule.ico")));
                IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.Create License Request 16 n i8.ico")));
                IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.advanced.ico")));

                IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.Multicast 16 h i8.ico")));

                IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.Failover Group 16 n i8.ico")));
                IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.Failover Server 16 n i8.ico")));
                IconList.Images.Add(new Bitmap(assembly.GetManifestResourceStream(ProjectName + ".Resources.User.ico")));

                for (int ix = 0; ix < IconList.Images.Count; ix++)
				{
					Bitmap temp = new Bitmap(IconList.Images[ix] as Bitmap, 16, 16);
					IconListBlack.Images.Add(MakeBlackImage(temp));
				}

				ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.System, ServerIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.SystemAddressFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.RecordingServer, RecorderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.RecordingServerFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.ArchiveStorageFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.Camera, CameraIconIx);
				ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.CameraFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.CameraGroupFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.CameraGroup, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.MicrophoneFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.MicrophoneGroupFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.MicrophoneGroup, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.Microphone, MicrophoneIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.SpeakerGroupFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.SpeakerGroup, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.SpeakerFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.Speaker, SpeakerIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.InputEventGroup, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.InputEventGroupFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.InputEventFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.InputEvent, InputIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.OutputGroup, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.OutputGroupFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.OutputFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.Output, OutputIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.MetadataGroup, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.MetadataGroupFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.MetadataFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.Metadata, MetadataIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.HardwareFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.HardwareDriverFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.HardwareDriver, HardwareIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.HardwareDriverSettings, SettingsIx);   

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.Hardware, HardwareIx);
				ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.PTZ, PresetIconIx);
				ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.Storage, RecorderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.BasicUser, BasicUserIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.BasicUserFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.Role, RoleIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.RoleFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.UserFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.StorageFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.Layout, ViewIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.LayoutFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.LayoutGroup, ViewGroupIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.LayoutGroupFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.VideoWall, VideoWallIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.VideoWallFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.Monitor, ViewIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.MonitorFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.VideoWallPreset, ViewIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.VideoWallPresetFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.MonitorPreset, ViewIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.MonitorPresetFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.UserDefinedEvent, CustomEventIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.UserDefinedEventFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.AnalyticsEvent, CustomEventIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.AnalyticsEventFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.GenericEvent, CustomEventIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.GenericEventFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.GenericEventDataSource, CustomEventIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.GenericEventDataSourceFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.GisMapLocation, GisMapLocationIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.GisMapLocationFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.TimeProfile, TimeProfileIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.TimeProfileFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.MIPKind, PluginIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.MIPKindFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.MIPItem, PluginIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.MIPItemFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.AlarmDefinition, AlarmIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.AlarmDefinitionFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.LprMatchList, LprMatchListIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.LprMatchListFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.SaveSearches, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.FindSaveSearches, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.SaveSearchesFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.AccessControlSystem, AccessControlSystemIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.AccessControlSystemFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.AccessControlUnit, AccessControlDoorIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.AccessControlUnitFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.Rule, RuleIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.RuleFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.Site, ServerIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.SiteFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.SiteAddressFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.LicenseInformationFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.LicenseInstalledProductFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.LicenseOverviewAllFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.LicenseDetailFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.LicenseInformation, LicenseIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.LicenseInstalledProduct, LicenseIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.LicenseOverviewAll, LicenseIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.LicenseDetail, LicenseIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.BasicOwnerInformation, BasicUserIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.BasicOwnerInformationFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.ToolOption, SettingsIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.ToolOptionFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.ViewGroup, ViewGroupIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.ViewGroupFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.View, ViewIx); 
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.ViewFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.RecordingServerMulticast, MulticastIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.RecordingServerMulticastFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.HardwarePtzSettingsFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.FailoverGroupFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.FailoverGroup, FailoverGroupIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.FailoverRecorder, FailoverServerIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.FailoverRecorderFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.ClientProfile, ClientProfileIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.ClientProfileFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.RecordingServerFailover, FailoverServerIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.RecordingServerFailoverFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.ClaimFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.LoginProviderFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.RegisteredClaimFolder, FolderIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.EventTypeGroupFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.EventTypeGroup, CustomEventIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.EventTypeFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.EventType, CustomEventIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.StateGroupFolder, FolderIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.StateGroup, CustomEventIconIx);

                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.Matrix, MatrixIconIx);
                ObjectTypeToIndex.Add(VideoOS.ConfigurationAPI.ItemTypes.MatrixFolder, FolderIconIx);

            }
            catch (Exception ex)
			{
				MessageBox.Show("Init:" + ex.Message);
			}
		}

		internal static int GetImageIndex(String itemType)
		{
			int index = 0;
            if (itemType == null || !ObjectTypeToIndex.TryGetValue(itemType, out index))
            {
                Debug.WriteLine("Unknown itemType for GetImageIndex: " + itemType);
            }
			return index;
		}

		private static Bitmap MakeBlackImage(Bitmap bitmapWhite)
		{
			Bitmap bitmapBlack = new Bitmap(bitmapWhite);
			for (int i = 0; i < 16; i++)
			{
				for (int y = 0; y < 16; y++)
				{
					Color c = bitmapWhite.GetPixel(i, y);
					if (c.ToArgb() == -1)
					{
						bitmapBlack.SetPixel(i, y, Color.FromArgb(0x00ffffff));
						continue;
					}
					if (c.R != 0 || c.G != 0 || c.B != 0)
					{
						if (y < 15)
						{
							Color c1 = bitmapWhite.GetPixel(i, y + 1);
							if (c1.R < c.R && c1.G < c.G && c1.B < c.B)
							{
								c1 = Color.FromArgb(0x00ffffff);
								bitmapBlack.SetPixel(i, y, c1);
							}
						}
						break;
					}
				}
				for (int y = 15; y > 0; y--)
				{
					Color c = bitmapWhite.GetPixel(i, y);
					if (c.ToArgb() == -1)
					{
						bitmapBlack.SetPixel(i, y, Color.FromArgb(0x00ffffff));
						continue;
					}
					if (c.R != 0 || c.G != 0 || c.B != 0)
					{
						if (y > 0)
						{
							Color c1 = bitmapWhite.GetPixel(i, y - 1);
							if (c1.R < c.R && c1.G < c.G && c1.B < c.B)
							{
								//c1 = c;
								//c1 = Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B);
								//c1 = Color.FromArgb((255 + c.R)/2, (255 + c.G)/2, (255 + c.B)/2);
								c1 = Color.FromArgb(0x00ffffff);
								bitmapBlack.SetPixel(i, y, c1);
							}
						}
						break;
					}
				}
			}
			return bitmapBlack;
		}

	}
}
