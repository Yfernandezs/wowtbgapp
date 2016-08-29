using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WoWTBGapp.DataAccess.Abstractions
{
    public class WebResponse
    {
        //public int EstadoRespuesta;

        public HttpStatusCode StatusCode;

        public string StateDescription;

        public string Data;

        //public Exception ExcepcionError;

        public bool IsSuccessful;

        public string ErrorMessage;

        public static async Task<WebResponse> GetHttpResponse(HttpResponseMessage respuestaHttp)
        {
            var response = new WebResponse();

            try
            {
                response.StatusCode = respuestaHttp.StatusCode;

                response.StateDescription = respuestaHttp.ReasonPhrase;

                response.IsSuccessful = respuestaHttp.IsSuccessStatusCode;

                if (response.IsSuccessful)
                {
                    response.Data = await respuestaHttp.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
                else
                {
                    response.ErrorMessage = await respuestaHttp.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
            }
            catch
            {
                response = null;
            }

            return response;
        }
    }
}
