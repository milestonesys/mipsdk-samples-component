using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VideoOS.Platform.JsonHandling;
using VideoOS.Platform.Login;
using VideoOS.Platform.Proxy.RestApi;
using VideoOS.Platform.EventsAndState;

namespace EventAndStateViewer
{
    /// <summary>
    /// This class is a cached wrapper for the <see cref="RestApiClient"/> for
    /// the purpose of looking up names based on the ids in an <see cref="Event"/>.
    /// Caching is required to prevent looking up names every time an event is received.
    /// <br/>
    /// Note: This class does not implement cache invalidation. To support this, create
    /// a new <see cref="IEventsAndStateSession"/> and subscribe to configuration
    /// changed events and invalidate the items having changed.
    /// </summary>
    class CachedRestApiClient : IDisposable
    {
        private readonly RestApiClient _restApiClient = new RestApiClient();
        private readonly SemaphoreSlim _clientSemaphore = new SemaphoreSlim(10); // Allow max 10 simultanious conncetions to API Gateway
        private readonly Dictionary<string, Task<JsonObject>> _cache = new Dictionary<string, Task<JsonObject>>();
        private readonly object _cacheLock = new object();

        public CachedRestApiClient(LoginSettings loginSettings)
        {
            _restApiClient.Initialize(loginSettings, false);
        }

        public async Task<string> LookupResourceNameAsync(string resourcePath, string nameKey)
        {
            // services and mipKinds are currently not supported by the REST API, so we skip those
            if (resourcePath.StartsWith("services/"))
                return "(Unknown service)";

            if (resourcePath.StartsWith("mipKinds/"))
                return "(Unknown MIP item)";

            var jsonObj = await LookupResourceAsync(resourcePath).ConfigureAwait(false);
            return jsonObj.GetChild("data").GetString(nameKey);
        }

        public Task<JsonObject> LookupResourceAsync(string resourcePath)
        {
            lock (_cacheLock)
            {
                // Cache hit: Return cached task (may or may not be completed or faulted)
                if (_cache.TryGetValue(resourcePath, out var result))
                {
                    return result;
                }

                // Cache miss: To prevent simultanious lookups, cache the incomplete task before returning it
                result = GetResourceAsync(resourcePath);
                _cache[resourcePath] = result;
                return result;
            }
        }

        private async Task<JsonObject> GetResourceAsync(string resourcePath)
        {
            // Throttle connection to API Gateway
            await _clientSemaphore.WaitAsync().ConfigureAwait(false);
            try
            {
                return await Task.Run(() => _restApiClient.GetResources(resourcePath)).ConfigureAwait(false);
            }
            finally
            {
                _clientSemaphore.Release();
            }
        }

        public void Dispose()
        {
            _restApiClient.Dispose();
        }
    }
}
