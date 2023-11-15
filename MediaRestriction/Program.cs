using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using VideoOS.Platform;
using VideoOS.Platform.Data;
using VideoOS.Platform.UI;

namespace MediaRestriction
{
    internal class Program
    {
        private static readonly Guid IntegrationId = new Guid("ED606DAB-0548-4D91-8901-F0E4F07D7E4E");
        private const string IntegrationName = "Media restriction";
        private const string Version = "1.0";
        private const string ManufacturerName = "Sample Manufacturer";

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Console.WriteLine("Milestone SDK restricted media demo (XProtect Corporate only)");
            Console.WriteLine("");
            Console.WriteLine(" This sample will:");
            Console.WriteLine(" - Enter live mode for two cameras (create two live restrictions)");
            Console.WriteLine(" - Retrieve the list of live restrictions");
            Console.WriteLine(" - End the two live restrictions and turn them into a single playback restriction");
            Console.WriteLine(" - Retrieve the list of playback restrictions");
            Console.WriteLine(" - Update the created playback restriction");
            Console.WriteLine(" - Delete the created playback restriction");
            Console.WriteLine();

            // Initialize the SDK - must be done in stand alone
            VideoOS.Platform.SDK.Environment.Initialize();
            VideoOS.Platform.SDK.UI.Environment.Initialize();       // Initialize UI

            #region Connect to the server
            bool connected = false;
            var loginForm = new VideoOS.Platform.SDK.UI.LoginDialog.DialogLoginForm((c) => connected = c, IntegrationId, IntegrationName, Version, ManufacturerName);
            Application.Run(loginForm);                             // Show and complete the form and login to server
            if (!connected)
            {
                Console.WriteLine("Failed to connect or login");
                Console.ReadKey();
                return;
            }

            if (EnvironmentManager.Instance.MasterSite.ServerId.ServerType != ServerId.CorporateManagementServerType)
            {
                Console.WriteLine("Restricted media is not supported on this product.");
                Console.ReadKey();
                return;
            }
            #endregion

            #region Select two cameras
            Item[] selectedDeviceItems = new Item[2];
            for (int i = 0; i < 2; i++)
            {
                var selectResult = SelectCamera();
                if (!selectResult.Success)
                    return;

                selectedDeviceItems[i] = selectResult.SelectedItem;
            }

            if (selectedDeviceItems[0].FQID == selectedDeviceItems[1].FQID)
            {
                Console.WriteLine("The same camera was selected twice. Two different cameras must be selected.");
                Console.ReadKey();
                return;
            }

            if (selectedDeviceItems[0].FQID.ServerId != selectedDeviceItems[1].FQID.ServerId)
            {
                Console.WriteLine("The selected cameras must come from the same server.");
                Console.ReadKey();
                return;
            }
            #endregion

            var timeNow = DateTime.UtcNow;
            var timeBeginLive = timeNow.AddMinutes(-5);
            RestrictedMedia createdPlaybackRestriction;

            #region Enter live mode for two cameras (create two live restrictions)
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Enter live mode for two cameras (create two live restrictions)");

            try
            {
                var result = RestrictedMediaService.Instance.RestrictedMediaLiveModeEnter(
                    serverId: EnvironmentManager.Instance.MasterSite.ServerId,
                    devices: selectedDeviceItems.Select(d => d.FQID),
                    timeStart: timeBeginLive
                    );

                Console.WriteLine($"Server returned status '{result.Status}' when entering live mode.");
                PrintWarningAndFaultDevices(result.WarningDevices, result.FaultDevices);

                if (!IsSuccessStatus(result.Status))
                {
                    Console.WriteLine("Press any key");
                    Console.ReadKey();
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Entering live mode failed : {ex.Message}");
                Console.WriteLine("Press any key");
                Console.ReadKey();
                return;
            }
            #endregion


            #region Retrieve the list of live restrictions
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Retrieve the list of live restrictions");
            try
            {
                var liveRestrictions = RestrictedMediaService.Instance.RestrictedMediaLiveQueryAll(
                    EnvironmentManager.Instance.MasterSite.ServerId
                    );
                foreach (var liveRestriction in liveRestrictions)
                {
                    Console.WriteLine($" * Device '{Configuration.Instance.GetItem(liveRestriction.Device).Name}' is in restricted live mode, starting from {liveRestriction.StartTime.ToShortDateString()} {liveRestriction.StartTime.ToShortTimeString()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Querying the list of live restrictions failed : {ex.Message}");
                Console.WriteLine("Press any key");
                Console.ReadKey();
                return;
            }
            #endregion


            #region End the two live restrictions and turn them into a single playback restriction
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("End the two live restrictions and turn them into a single playback restriction");

            try
            {
                var result = RestrictedMediaService.Instance.RestrictedMediaLiveModeExit(
                    serverId: EnvironmentManager.Instance.MasterSite.ServerId,
                    devices: selectedDeviceItems.Select(d => d.FQID),
                    header: $"Restriction created {timeNow.ToShortDateString()} {timeNow.ToShortTimeString()}",
                    description: "",
                    timeStart: timeBeginLive,
                    timeEnd: DateTime.UtcNow
                    );

                Console.WriteLine($"Server returned '{result.Status}' when exiting live mode.");
                PrintWarningAndFaultDevices(result.WarningDevices, result.FaultDevices);

                if (!IsSuccessStatus(result.Status))
                {
                    Console.WriteLine("Press any key");
                    Console.ReadKey();
                    return;
                }

                createdPlaybackRestriction = result.RestrictedMedia;

                Console.WriteLine();
                PrintPlaybackRestriction(createdPlaybackRestriction);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exiting live mode failed : {ex.Message}");
                Console.WriteLine("Press any key");
                Console.ReadKey();
                return;
            }
            #endregion


            #region Retrieve the list of playback restrictions
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Retrieve the list of playback restrictions");

            try
            {
                var playbackRestrictions = RestrictedMediaService.Instance.RestrictedMediaQueryAll(
                    EnvironmentManager.Instance.MasterSite.ServerId
                    );
                foreach (var playbackRestriction in playbackRestrictions)
                {
                    Console.WriteLine();
                    PrintPlaybackRestriction(playbackRestriction);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Querying the list of playback restrictions failed : {ex.Message}");
                Console.WriteLine("Press any key");
                Console.ReadKey();
                return;
            }
            #endregion


            #region Update the created playback restriction
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Update the created playback restriction");

            try
            {
                   var result = RestrictedMediaService.Instance.RestrictedMediaUpdate(
                                            restrictedMediaFQID: createdPlaybackRestriction.FQID,
                                            devicesToAdd: Enumerable.Empty<FQID>(),
                                            devicesToRemove: createdPlaybackRestriction.Devices.Take(1),
                                            header: createdPlaybackRestriction.Header,
                                            description: createdPlaybackRestriction.Description,
                                            timeStart: createdPlaybackRestriction.StartTime,
                                            timeEnd: createdPlaybackRestriction.EndTime
                                            );

                Console.WriteLine($"Server returned '{result.Status}' when updating playback restriction.");
                PrintWarningAndFaultDevices(result.WarningDevices, result.FaultDevices);

                if (!IsSuccessStatus(result.Status))
                {
                    Console.WriteLine("Press any key");
                    Console.ReadKey();
                    return;
                }

                PrintPlaybackRestriction(result.RestrictedMedia);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Updating playback restriction failed : {ex.Message}");
                Console.WriteLine("Press any key");
                Console.ReadKey();
                return;
            }
            #endregion


            #region Delete the created playback restriction
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Delete the created playback restriction");

            try
            {
                var result = RestrictedMediaService.Instance.RestrictedMediaDelete(
                                        restrictedMediaFQID: createdPlaybackRestriction.FQID
                                        );

                Console.WriteLine($"Server returned '{result.Status}' when updating playback restriction.");
                PrintWarningAndFaultDevices(result.WarningDevices, result.FaultDevices);

                if (!IsSuccessStatus(result.Status))
                {
                    Console.WriteLine("Press any key");
                    Console.ReadKey();
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Deleting the playback restriction failed : {ex.Message}");
                Console.WriteLine("Press any key");
                Console.ReadKey();
                return;
            }
            #endregion


            Console.WriteLine("");
            Console.WriteLine("Press any key");
            Console.Out.Flush();
            Console.ReadKey();

            // Local functions
            (bool Success, Item SelectedItem) SelectCamera()
            {
                ItemPickerWpfWindow itemPicker = new ItemPickerWpfWindow()
                {
                    KindsFilter = new List<Guid> { Kind.Camera },
                    SelectionMode = SelectionModeOptions.AutoCloseOnSelect,
                    Items = Configuration.Instance.GetItems()
                };

                if (!itemPicker.ShowDialog().Value)
                {
                    Console.WriteLine("Failed to pick a camera");
                    Console.ReadKey();
                    return (false, null);
                }

                if (itemPicker.SelectedItems.First().FQID.ServerId.ServerType != ServerId.CorporateRecordingServerType)
                {
                    Console.WriteLine("Media restrictions are not supported on this product.");
                    Console.ReadKey();
                    return (false, null);
                }

                return (true, itemPicker.SelectedItems.First());
            }

            void PrintWarningAndFaultDevices(IEnumerable<WarningDevice> warningDevices, IEnumerable<FaultDevice> faultDevices)
            {
                if (warningDevices?.Any() ?? false)
                {
                    foreach (var warningDevice in warningDevices)
                    {
                        Console.WriteLine($"  - Got a warning for device '{Configuration.Instance.GetItem(warningDevice.FQID).Name}'. Message: {warningDevice.Message}");
                    }
                }

                if (faultDevices?.Any() ?? false)
                {
                    foreach (var faultDevice in faultDevices)
                    {
                        Console.WriteLine($"  - Failed device with id '{Configuration.Instance.GetItem(faultDevice.FQID).Name}'. Message: {faultDevice.Message}");
                    }
                }
            }

            void PrintPlaybackRestriction(RestrictedMedia playbackRestriction)
            {
                Console.WriteLine($" * Playback restriction '{playbackRestriction.Header}'");
                Console.WriteLine($"     from {playbackRestriction.StartTime.ToShortDateString()} {playbackRestriction.StartTime.ToShortTimeString()} to {playbackRestriction.EndTime.ToShortDateString()} {playbackRestriction.EndTime.ToShortTimeString()}");
                Console.WriteLine($"     Device ids:");
                foreach (var deviceFQID in playbackRestriction.Devices)
                {
                    Console.WriteLine($"      - {Configuration.Instance.GetItem(deviceFQID).Name}");
                }
            }

            bool IsSuccessStatus(RestrictedMediaResultStatus status)
            {
                return status == RestrictedMediaResultStatus.Success ||
                       status == RestrictedMediaResultStatus.PartlySuccess;
            }
        }
    }
}
