using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VideoOS.Platform.SDK.OAuth;

namespace MultiUserEnvironment
{
    public class IdpHelper
    {
        public static MipTokenCache GetTokenCache(Uri serverUri, string username, string password, bool isAdUser)
        {
            var idpUri = new Uri(serverUri, "idp");
            return new MipTokenCache(idpUri, new NetworkCredential(username, password), !isAdUser);
        }
    }
}
