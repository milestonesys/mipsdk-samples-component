using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using VideoOS.ConfigurationAPI;

namespace ConfigAPIBatch
{
	class Program
	{
		static ConfigAPIClient client = new ConfigAPIClient();

		static void Main(string[] args)
		{
			client.ServerAddress = "10.10.48.236";
			client.Serverport = 80;
			client.BasicUser = true;
			client.Username = "a";
			client.Password = "a";
			client.SecureOnly = false;

			try
			{
				client.Initialize();

				ConfigurationItem main = client.GetItem("/");

				// We just dump the top level items
				ConfigurationItem[] items = client.GetChildItems("/");
				foreach (ConfigurationItem item in items)
				{
					Console.WriteLine("---- ConfigurationItem -- "+item.DisplayName+" ("+item.ItemType+")");
					Console.WriteLine("Path      :" + item.Path);
				}

				// lets find all cameras
				List<ConfigurationItem> cameraHierarchy = client.GetTopForItemType(ItemTypes.Camera);
				List<ConfigurationItem> result = new List<ConfigurationItem>();

				foreach (ConfigurationItem item in cameraHierarchy)
				{
					CollectHierarchy(item, ItemTypes.Camera, ref result);
				}

				foreach (ConfigurationItem item in result)
				{
					String id = "";
					id = item.Properties.SingleOrDefault(p => (p.Key == "Id")).Value;
					Console.WriteLine("Camera: "+item.DisplayName + ", Id="+id);
					FillChildren(item);

					foreach (ConfigurationItem folder in item.Children)
					{
						if (folder.ItemType == ItemTypes.DeviceDriverSettingsFolder)
						{
							FillChildren(folder);
							foreach (var childSetting in folder.Children)
							{
								DumpItemAndProperties(childSetting, "    ");
							}
						}
					}

				}

				// now lets dump all know hardware:
				result.Clear();
				foreach (ConfigurationItem item in cameraHierarchy)
				{
					CollectHierarchy(item, ItemTypes.Hardware, ref result);
				}

				foreach (ConfigurationItem item in result)
				{
					Console.WriteLine("Hardware: " + item.DisplayName);
				}

				client.Close();
			}
			catch (Exception ex)
			{
				Trace.WriteLine("Exception: "+ex.Message);
			}
		}

		private static void FillChildren(ConfigurationItem item)
		{
			if (!item.ChildrenFilled)
			{
				item.Children = client.GetChildItems(item.Path);
				item.ChildrenFilled = true;
			}
			if (item.Children == null)
				item.Children = new ConfigurationItem[0];			
		}

		private static void CollectHierarchy(ConfigurationItem item, String itemType, ref List<ConfigurationItem> result)
		{
			if (item.ItemType == itemType && item.ItemCategory == ItemCategories.Item)
			{
				result.Add(item);
				return;
			}

			FillChildren(item);

			foreach (ConfigurationItem child in item.Children)
			{
				if (child.ItemType == ItemTypes.HardwareDriver)		// We exclude all the drivers in this search
					continue;

				CollectHierarchy(child, itemType, ref result);
			}
		}

		private static Guid FPSTranslationId = new Guid("d661e46c-dcb0-4402-b9b3-0e9e5d07ffd9");
		private static void DumpItemAndProperties(ConfigurationItem item, string indent)
		{
			Console.WriteLine(indent+"----- "+item.DisplayName+"  ("+item.ItemType+")");
			foreach (Property property in item.Properties)
			{
				if (property.IsSettable || property.UIImportance != UIImportance.Hidden)
				{
					Console.WriteLine(indent + "Property:" + property.DisplayName + " = " + property.Value);
					// In this sample, we use the TranslationId, but could also check the ending on the Key
					// For Stream configurations, the key on it's properties is formatted like:
					// Stream:0.0.1/FPS/6527c12a-06f1-4f58-a2fe-6640c61707e0
					if (property.TranslationId == FPSTranslationId.ToString())
					{
						Console.WriteLine(indent+"   FPS Value found ="+ property.Value);
					}
				}
			}
			FillChildren(item);
			foreach (var child in item.Children)
			{
				DumpItemAndProperties(child, indent+"    ");
			}
		}
	}
}
