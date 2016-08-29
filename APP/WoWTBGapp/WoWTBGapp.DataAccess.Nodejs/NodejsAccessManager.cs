using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WoWTBGapp.DataAccess.Abstractions;
using Xamarin.Forms;

namespace WoWTBGapp.DataAccess.Nodejs
{
    public class NodejsAccessManager : IAccessManager
    {
        // Web URL for the Node.js service.
        public const string NodejsUrl = "https://wowtbgapp.herokuapp.com";

        public static string authHeaderValue { get; set; }

        public static HttpRequestMessage NodeRequest { get; set; }

        public static HttpClientHandler ClientHandler { get; set; }

        public static HttpClient Client { get; set; }

        #region IAccessManager implementation

        public bool IsInitialized { get; private set; }

        private IItemCardAccess itemCardAccess;
        public IItemCardAccess ItemCardAccess => itemCardAccess ?? (itemCardAccess = DependencyService.Get<IItemCardAccess>());

        object locker = new object();
        public async Task InitializeAsync()
        {
            lock (locker)
            {

                if (IsInitialized)
                    return;

                IsInitialized = true;

                // Initialize the object
                var authData = $"{"wowtbgapp"}:{"W0WTBGapp"}";
                authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

                //Client = new HttpClient();
                //Client.MaxResponseContentBufferSize = 256000;
                //Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            }

            //await GetDataAsync(null, string.Empty).ConfigureAwait(false);
        }

        //public Task<bool> SyncAllAsync(bool syncUserSpecific)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion IAccessManager implementation
            
        /// <summary>
        /// Método que consulta las funciones GET de un servicio Restful.
        /// </summary>
        /// <param name="parametros"></param>
        /// <param name="metodoAPI"></param>
        /// <param name="segundosTimeout"></param>
        /// <returns></returns>
        public async Task<WebResponse> GetDataAsync(List<object> parameters, string APIMethod, int timeoutSeconds = 30)
        {
            if (!IsInitialized)
            {
                await InitializeAsync().ConfigureAwait(false);
            }

            WebResponse webResponse = null;

            var finalURL = NodejsUrl + "/" + APIMethod;

            if (parameters != null && parameters.Count > 0)
            {
                foreach (string value in parameters)
                {
                    finalURL += "/" + value;
                }
            }

            var uri = new Uri(finalURL);

            //if (Client != null)
            //{
            //    //Client.Timeout = new TimeSpan(0, 0, timeoutSeconds * 1000);

            //    //try
            //    //{
            //    //    var response = await Client.GetAsync(uri, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);

            //    //    webResponse = await WebResponse.GetHttpResponse(response);
            //    //}
            //    //catch (Exception ex)
            //    //{
            //    //    Debug.WriteLine(@" ERROR {0}", ex.Message);
            //    //}

                
            //}

            try
            {
                ClientHandler = new HttpClientHandler();

                Client = new HttpClient(ClientHandler);
                //Client.MaxResponseContentBufferSize = 256000;
                //Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

                NodeRequest = new HttpRequestMessage(HttpMethod.Get, uri);

                //NodeRequest.Content = streamContent;
                //if (ClientHandler.SupportsTransferEncodingChunked())
                //{
                //    NodeRequest.Headers.TransferEncodingChunked = true;
                //}
                NodeRequest.Headers.TransferEncodingChunked = true;

                HttpResponseMessage response = await Client.SendAsync(NodeRequest).ConfigureAwait(false);

                webResponse = await WebResponse.GetHttpResponse(response).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@" ERROR {0}", ex.Message);
            }

            return webResponse;
        }
    }
}
