using System;
using System.Collections.Generic;
using VideoOS.Platform;
using VideoOS.Platform.SDK;
using VideoOS.Platform.SDK.StatusClient;
using VideoOS.Platform.SDK.StatusClient.StatusEventArgs;
using Environment = System.Environment;

namespace SystemStatusClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // supply three parameters to make the sample work: the hostname of the XPCO Management Server and the username and password
            // of the account that will login to the system.

            if (args.Length != 3)
            {
                Console.WriteLine("You must supply exactly three parameters: Hostname of XPCO Management Server, username and password");
                Environment.Exit(1);
            }

            var uriBuilder = new UriBuilder(args[0]);
            var serverUri = uriBuilder.Uri;
            var userName = args[1];
            var password = args[2];

            MultiEnvironment.InitializeUsingUserContext();

            // Remember to set the boolean parameter to true or false depending in whether the user is in AD.
            var userContext = MultiEnvironment.CreateSingleServerUserContext(userName, password, true, serverUri);

            bool loginSuceeded = MultiEnvironment.LoginUserContext(userContext);
            if (loginSuceeded == false)
            {
                Console.WriteLine("Failed to login to: " + serverUri);
                Console.WriteLine("");
                Console.WriteLine("Press any key");
                Console.ReadKey();
                Environment.Exit(1);
            }

            // Start new status sessions to an entire system with (possibly) multiple recording servers.
            var multiSession = new SystemStatusClient(userContext.Configuration.ServerFQID);

            // Register eventhandlers for the events we are interested in
            multiSession.EventFired += EventFiredHandler;
            multiSession.ConnectionStateChanged += MultiSessionOnConnectionStateChanged;
            multiSession.ConfigurationChanged += MultiSessionOnConfigurationChanged;
            multiSession.CameraStateChanged += MultiSessionOnCameraStateChanged;
            //multiSession.SpeakerStateChanged += ...

            // Start the sessions
            multiSession.CreateAndStartSessions();

			var result = multiSession.GetAllStatusEventMessages();


            // Register for particular events. Feel free to add identifiers for other events.
            // IMPORTANT: The sessions must be started using the CreateAndStartSessions() method before you can subsribe to any events.
            multiSession.SetSubscribedEventsOnCreatedSessions(new HashSet<Guid>
                {
                    KnownStatusEvents.MotionStarted,
                    KnownStatusEvents.MotionStopped
                });

            // Subscribe to recieve notification when the configuration has changed.
            multiSession.SubscribeToConfigurationChanges(true);

            // Subscribe to state changes on all devices of type camera.
            multiSession.AddSubscriptionsToDevicesOfKindOnCreatedSessions(Kind.Camera);

            Console.ReadKey();

            // Shut down all sessions nicely.
            multiSession.StopAndRemoveSessions();
        }

        private static void MultiSessionOnCameraStateChanged(object sender, CameraStateChangedEventArgs e)
        {
            Console.WriteLine(@"{0} - Camera status: ID({1}), Enabled({2}), Motion ({3}), Recording({4})", e.Time, e.DeviceId, e.Enabled, e.Motion, e.Recording);
        }

        private static void MultiSessionOnConfigurationChanged(object sender, StatusApiEventArgs e)
        {
            Console.WriteLine(@"{0} - Configuration changed", e.Time);
        }

        private static void EventFiredHandler(object sender, EventFiredEventArgs e)
        {
            Console.WriteLine(@"{0} - Event {1} fired from source {2}", e.Time.ToLocalTime(), KnownStatusEvents.GetEventName(e.EventId), e.SourceId);
        }

        private static void MultiSessionOnConnectionStateChanged(object sender, StatusSessionChangesArgs e)
        {
            Console.WriteLine(@"{0} - Connection state change to {1} for session to server {2}", DateTime.Now, e.ConnectionState, e.RecorderServer.Name);
        }
    }
}
