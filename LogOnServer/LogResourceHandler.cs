using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using VideoOS.Platform;
using VideoOS.Platform.Log;

namespace LogOnServer
{
	internal class LogResourceHandler
	{
		private const String _myApplication = "LogMessageTester";
		private const String _myComponent = "LogForm";

		private const String _id1 = "RecStart";
		private const String _id2 = "RecStop";
		private const String _id3 = "ACSwipe";
		
		internal static void RegisterMyMessages()
		{
			LogMessageDictionary lmd1 = new LogMessageDictionary("en-US", "3.9", _myApplication, _myComponent,
			                                                    BuildDictionary("en-US"), "AccessControl");
			LogMessageDictionary lmd2 = new LogMessageDictionary("da-DK", "3.9", _myApplication, _myComponent,
																BuildDictionary("da-DK"), "AccessControl");
			VideoOS.Platform.Log.LogClient.Instance.RegisterDictionary(lmd1);
			VideoOS.Platform.Log.LogClient.Instance.RegisterDictionary(lmd2);
		}

		private static Dictionary<String,LogMessage> BuildDictionary(String culture)
		{
			Dictionary<String, LogMessage> messages = new Dictionary<string, LogMessage>();
			Strings.Culture = new CultureInfo(culture);
			messages.Add(_id1, new LogMessage() { Id = _id1, Category = "ExternalComponents", Message = Strings.RecStart, Group=Group.System, Severity = Severity.Info, Status = Status.StatusQuo, RelatedObjectKind = Kind.Camera });
			messages.Add(_id2, new LogMessage() { Id = _id2, Category = "ExternalComponents", Message = Strings.RecStop, Group = Group.System, Severity = Severity.Info, Status = Status.StatusQuo, RelatedObjectKind = Kind.Camera });
			messages.Add(_id3, new LogMessage() { Id = _id3, Category = "ExternalComponents", Message = Strings.ACSwipe, Group = Group.Audit, Severity = Severity.Info, Status = Status.StatusQuo, RelatedObjectKind = Kind.Server });
			return messages;
		}

		internal static void LogStart(Item cameraItem)
		{
			Dictionary<String,String> parms = new Dictionary<string, string>();
			parms["c1"] = cameraItem.Name;		// Filling for the {c1} part of the message

			VideoOS.Platform.Log.LogClient.Instance.NewEntry(_myApplication, _myComponent, _id1, cameraItem, parms);
		}

		internal static void LogStop(Item cameraItem)
		{
			Dictionary<String, String> parms = new Dictionary<string, string>();
			parms["c1"] = cameraItem.Name;		// Filling for the {c1} part of the message

			VideoOS.Platform.Log.LogClient.Instance.NewEntry(_myApplication, _myComponent, _id2, cameraItem, parms);
		}

		internal static void CardSwiped(bool granted)
		{
			Dictionary<String, String> parms = new Dictionary<string, string>();
			parms["d1"] = "Door 25 ";
			parms["card"] = "ABCs card";

			Item item = new Item(EnvironmentManager.Instance.MasterSite, "Door 25-1");

			VideoOS.Platform.Log.LogClient.Instance.AuditEntry(_myApplication, _myComponent, _id3, item, parms, granted?PermissionState.Granted:PermissionState.Denied);
		}
	}
}
