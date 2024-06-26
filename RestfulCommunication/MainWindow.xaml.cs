using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using VideoOS.Platform;
using VideoOS.Platform.Login;
using VideoOS.Platform.OAuth;

namespace RestfulCommunication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string _serverHostname;
		private readonly int _serverPort;
		private readonly Uri _serverUri;
		private readonly string _baseUri;

		private static readonly HttpClient client = new HttpClient();

		private static readonly List<RequestDefinition> _requests = new List<RequestDefinition>()
		{
			{	new RequestDefinition("---Select a request---", "", null,"") },
			{	new RequestDefinition("[POST] Create a user-defined event", "/userDefinedEvents", HttpMethod.Post,"{ \"name\" : \"my .NET event\" }") },
			{	new RequestDefinition("[GET] Get all user-defined events", "/userDefinedEvents", HttpMethod.Get, "") },
			{	new RequestDefinition("[GET] Get a user-defined event information", "/userDefinedEvents/{userDefinedEventId}", HttpMethod.Get, "") },
			{	new RequestDefinition("[GET] Get a user-defined event information with resources", "/userDefinedEvents/{userDefinedEventId}?resources", HttpMethod.Get, "") },
			{	new RequestDefinition("[GET] Get a user-defined event information with definitions", "/userDefinedEvents/{userDefinedEventId}?definitions", HttpMethod.Get, "") },
			{	new RequestDefinition("[PUT] Update all writable fields on a user-defined event", "/userDefinedEvents/{userDefinedEventId}", HttpMethod.Put, "{ \"name\" : \"my updated .NET event\" }") },
			{	new RequestDefinition("[DELETE] Delete a user defined-event", "/userDefinedEvents/{userDefinedEventId}", HttpMethod.Delete, "") },
			{	new RequestDefinition("[GET] Get a list of cameras", "/cameras", HttpMethod.Get, "") },
			{	new RequestDefinition("[GET] Get a list of available ptzPresets tasks", "/cameras/{ptzCameraId}/ptzPresets?tasks&nodata", HttpMethod.Get, "") },
			{	new RequestDefinition("[POST] Invoke a ptzPresets task", "/cameras/{ptzCameraId}/ptzPresets?task={task}", HttpMethod.Post, "{ \"sessionDataId\" : 0 }") }, //sessionDataId should be set to 0 when starting a new task. The purpose is to be able to approve special two steps tasks
			{	new RequestDefinition("[GET] Get the status of the invoked task", "/tasks/{taskId}", HttpMethod.Get, "") },
			{ new RequestDefinition("[POST] Clean up invoked task", "/tasks/{taskId}?task=TaskCleanup", HttpMethod.Post, "" ) },
			{ new RequestDefinition("[PATCH] Rename event", "/userDefinedEvents/{userDefinedEventId}", new HttpMethod("Patch"), "{ \"name\" : \"my updated .NET event\" }") },
      { new RequestDefinition("[PATCH] Disable rule", "/rules/{ruleId}", new HttpMethod("Patch"), "{ \"enabled\" : \"false\" }") }
    };

		public MainWindow()
		{
			InitializeComponent();
			_serverHostname = EnvironmentManager.Instance.MasterSite.ServerId.ServerHostname;
			_serverPort = EnvironmentManager.Instance.MasterSite.ServerId.Serverport;
			_serverUri = LoginSettingsCache.GetLoginSettings(_serverHostname, _serverPort)?.Uri;
			_baseUri = $"{_serverUri}api/rest/v1";  //Assume API Gateway is installed on same host as the Management Server that user has logged in to
			cboRequests.ItemsSource = _requests;
			cboRequests.DisplayMemberPath = "DisplayName";
			cboRequests.SelectedIndex = 0;
		}

		private void cboRequests_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			txtInput.Text = string.Empty;
			if (cboRequests.SelectedIndex == 0)
			{
				txtUrl.Text = string.Empty;
			}
			else
			{
				var selectedRequest = (RequestDefinition)cboRequests.SelectedValue;
				txtUrl.Text = string.Format("{0}{1}", _baseUri, selectedRequest.Uri);
				if (!string.IsNullOrEmpty(selectedRequest.RequestBody))
				{
					txtInput.Text = JToken.Parse(selectedRequest.RequestBody).ToString(Newtonsoft.Json.Formatting.Indented);
				}
			}
		}

		private async void btnSendRequest_ClickAsync(object sender, EventArgs e)
		{
			await CallApi();
		}

		private async Task CallApi()
		{
			var loginSettings = LoginSettingsCache.GetLoginSettings(_serverHostname, _serverPort);
			//Checks if the IdentityTokenCache is set in LoginSettings.
			//The IdentityTokenCache holds the current token that gets refreshed internally.
			if (loginSettings.IdentityTokenCache != null) 
			{
				await Call(txtUrl.Text, loginSettings.IdentityTokenCache);
			}
			else
			{
				txtOutput.Foreground = Brushes.Red;
				txtOutput.Text = "Could not retrieve the token!";
			}
		}

		/// <summary>
		/// REST API call using HttpClient
		/// </summary>
		private async Task Call(string requestUri, IMipTokenCache tokenCache)
		{
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			//Adding identity token from token cache as a bearer token in the authorization header
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenCache.Token);

			var result = string.Empty;
			try
			{
				var content = new StringContent(txtInput.Text, Encoding.UTF8, "application/json");
				var selectedRequest = (RequestDefinition)cboRequests.SelectedValue;
				var request = new HttpRequestMessage(selectedRequest.HttpMethod, requestUri);
				if (selectedRequest.HttpMethod != HttpMethod.Get)
				{
					request.Content = content;
				}
				HttpResponseMessage response = await client.SendAsync(request);
				result = await response.Content.ReadAsStringAsync();
				JToken jToken = JToken.Parse(result);
				txtOutput.Text = string.Empty;	
				txtOutput.Foreground = Brushes.Black;
				txtOutput.Text = jToken.ToString(Newtonsoft.Json.Formatting.Indented);

				ValidateResultForFurtherInformation(requestUri, response, selectedRequest.HttpMethod);
			}
			catch (JsonReaderException)
            {
				// Display response text without parsing if we received the response but it cannot be parsed to JSON
				txtOutput.Text = result;
			}
			catch (Exception ex)
			{
				txtOutput.Foreground = Brushes.Red;
				txtOutput.Text = "Error: " + ex.Message + "\nStackTrace: " + ex.StackTrace;
			}
		}

		private void ValidateResultForFurtherInformation(string requestUri, HttpResponseMessage response, HttpMethod httpMethod)
        {
			if (response.StatusCode == System.Net.HttpStatusCode.OK &&
					httpMethod == HttpMethod.Post &&
					requestUri.Contains("?task=") &&
                    !requestUri.EndsWith("?task=TaskCleanup")) 
			{
				lblWarning.Foreground = Brushes.Red;
				lblWarning.Text = "Remember to cleanup the task - use '[POST] Clean up invoked task' from the combobox.";
			}
			else
			{
				lblWarning.Text = "";
			}


		}

        private async void OnKeyDownHandler(object sender, KeyEventArgs e)
		{
			//Call API if the enter button is pressed 
			if (e.Key == Key.Return)
			{
				await CallApi();
			}
		}

		private void Clear()
		{
			VideoOS.Platform.SDK.Environment.RemoveAllServers();
		}

		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
            Clear();
            base.OnClosing(e);
		}

		private void OnClose(object sender, EventArgs e)
		{
			Close();
		}
	}
}