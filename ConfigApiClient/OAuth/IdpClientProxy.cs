using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConfigAPIClient.OAuth
{
	/// <summary>
	/// Proxy to communicate with IDP server.
	/// </summary>
	public static class IdpClientProxy
	{
		/// <summary>
		/// Check if IDP server is available in VMS.
		/// </summary>
		/// <param name="address"></param>
		/// <param name="port"></param>
		/// <returns></returns>
		public static async Task<bool> IsOAuthServer(string address, int port)
		{
			Uri idpUri = GetIdpUri(address, port);
			var endpoint = new UriBuilder(idpUri)
			{
				Path = Path.Combine(idpUri.AbsolutePath, ".well-known/openid-configuration"),
			}.Uri.AbsoluteUri;
			var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
			try
			{
				var client = new HttpClient();
				var response = await client.SendAsync(request).ConfigureAwait(false);
				if (!response.IsSuccessStatusCode)
				{
					return false;
				}
				return HasOAuthServer(await response.Content.ReadAsStringAsync());
			}
			catch (Exception)
			{
				return false;
			}
		}

		private static Uri GetIdpUri(string address, int port)
		{
			var uri = new UriBuilder(port == 443 ? Uri.UriSchemeHttps : Uri.UriSchemeHttp, address).Uri;
			return new Uri(uri, @"/idp\");
		}

		/// <summary>
		/// In the sample we are using regular expressions for simplicity,
		/// but as the data are in json format you can easily use a standard json library for parsing the data.
		/// </summary>
		/// <param name="httpContent"></param>
		/// <returns></returns>
		private static bool HasOAuthServer(string httpContent)
		{
			string pattern = "\"server_version\"" + ":" + "\"[0-9]{1,2}.{0,1}[0-9]\"";
			Regex rgx = new Regex(pattern);
			var result = rgx.Match(httpContent);
			if (result.Success)
			{
				string[] serverVersion = result.Value.Replace("\"", "").Split(':')[1].Split('.');
				int serverMajorVersion = Convert.ToInt32(serverVersion[0]);
				return serverMajorVersion >= 21;
			}
			else
			{
				return false;
			}
		}
	}

}

