using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace wowtbgapp.api.Comunicacion
{
    public class RestService
    {
        // La Dirección URL va quemada y si cambia en el servicio web debe ser cambiada aqui también.
        public const string RestUrl = "https://wowtbgapp.herokuapp.com";

        private HttpClient client;
        
        public RestService(string username, string password)
        {
            var authData = string.Format("{0}:{1}", username, password);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            var Autorization = "Basic " + authHeaderValue;


            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

            // El Token va quemado y si cambia en el servicio web debe ser cambiado aqui también.
            client.DefaultRequestHeaders.Add("X-ZUMO-APPLICATION", "e6195c0257b02b31e05422559183dd96f251170b"); 
        }

        /// <summary>
        /// Método que consulta las funciones GET de un servicio Restful.
        /// </summary>
        /// <param name="parametros"></param>
        /// <param name="metodoAPI"></param>
        /// <param name="segundosTimeout"></param>
        /// <returns></returns>
        public async Task<RespuestaWeb> GetDataAsync(List<object> parametros, string metodoAPI, int segundosTimeout = 30)
        {
            RespuestaWeb respuestaWeb = null;
            
            var URLFinal = RestUrl + "/" + metodoAPI;

            if (parametros != null && parametros.Count > 0)
            {
                foreach (string valor in parametros)
                {
                    URLFinal += "/" + valor;
                }
            }
            
            var uri = new Uri(URLFinal);

            client.Timeout = new TimeSpan(0, 0, segundosTimeout * 1000);

            try
            {
                var response = await client.GetAsync(uri);

                respuestaWeb = await RespuestaWeb.LeerRespuestaHttp(response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@" ERROR {0}", ex.Message);
            }

            return respuestaWeb;
        }

    }
}
