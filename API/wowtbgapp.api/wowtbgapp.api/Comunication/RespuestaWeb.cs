using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace wowtbgapp.api.Comunicacion
{
    public class RespuestaWeb
    {
        //public int EstadoRespuesta;

        public HttpStatusCode CodigoEstado;

        public string DescripcionEstado;

        public string DatosObtenidos;

        //public Exception ExcepcionError;

        public bool EsRespuestaExitosa;

        public string MensajeError;

        public static async Task<RespuestaWeb> LeerRespuestaHttp(HttpResponseMessage respuestaHttp)
        {
            var respuesta = new RespuestaWeb();

            try
            {
                respuesta.CodigoEstado = respuestaHttp.StatusCode;

                respuesta.DescripcionEstado = respuestaHttp.ReasonPhrase;

                respuesta.EsRespuestaExitosa = respuestaHttp.IsSuccessStatusCode;

                if (respuesta.EsRespuestaExitosa)
                {
                    respuesta.DatosObtenidos = await respuestaHttp.Content.ReadAsStringAsync();
                }
                else
                {
                    respuesta.MensajeError = await respuestaHttp.Content.ReadAsStringAsync();
                }
            }
            catch
            {
                respuesta = null;
            }

            return respuesta;
        }
    }
}
