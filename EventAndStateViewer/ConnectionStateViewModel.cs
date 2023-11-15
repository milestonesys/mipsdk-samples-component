using EventAndStateViewer.Mvvm;
using System;
using VideoOS.Platform.EventsAndState;

namespace EventAndStateViewer
{
    /// <summary>
    /// View model for the connection state shown within MainWindow.xaml
    /// </summary>
    class ConnectionStateViewModel : ViewModelBase
    {
        private string _errorStateMessage = string.Empty;
        private string _successStateMessage = "Initializing...";

        public ConnectionStateViewModel()
        {
            App.DataModel.EventReceiver.ConnectionStateChanged += OnConnectionStateChanged;
        }

        public string ErrorStateMessage
        {
            get => _errorStateMessage;
            private set => SetProperty(ref _errorStateMessage, value);
        }

        public string SuccessStateMessage
        {
            get => _successStateMessage;
            private set => SetProperty(ref _successStateMessage, value);
        }

        private void OnConnectionStateChanged(object sender, ConnectionState connectionState)
        {
            (SuccessStateMessage, ErrorStateMessage) = GetConnectionStateMessage(connectionState);
        }

        private (string, string) GetConnectionStateMessage(ConnectionState connectionState)
        {
            switch (connectionState)
            {
                case ConnectionState.Connecting:
                    return ("Connecting...", string.Empty);
                case ConnectionState.Online:
                    return ("Online", string.Empty);
                case ConnectionState.Offline:
                    return (string.Empty, "Offline");
                case ConnectionState.Delayed:
                    return (string.Empty, "Reconnecting...");
                case ConnectionState.Halted:
                    return (string.Empty, "Error");
                default:
                    throw new NotImplementedException($"Unexpected connection state: {connectionState}.");
            }
        }
    }
}
