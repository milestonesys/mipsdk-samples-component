using System.Net.Http;

namespace RestfulCommunication
{
	/// <summary>
	/// Class that represents a request used in the sample. 
	/// </summary>
	public class RequestDefinition
	{
		/// <summary>
		/// Name of the request used in combobox.
		/// </summary>
		public string DisplayName { get; set; }
		/// <summary>
		/// URI used to make the REST API call against.
		/// </summary>
		public string Uri { get; set; }
		/// <summary>
		/// Sample JSON data for a request like POST, PUT, etc.
		/// </summary>
		public string RequestBody { get; set; }
		/// <summary>
		/// HTTP method (GET, POST, PUT, etc ) to be used to make a REST API call.
		/// </summary>
		public HttpMethod HttpMethod { get; set; }
		public RequestDefinition(string displayName, string uri, HttpMethod httpMethod, string requestBody)
		{
			DisplayName = displayName;
			Uri = uri;
			HttpMethod = httpMethod;
			RequestBody = requestBody;
		}
	}
}
