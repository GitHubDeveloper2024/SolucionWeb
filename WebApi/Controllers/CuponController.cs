using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApi.Models;
using static System.Net.WebRequestMethods;

namespace WebApi.Controllers
{
    public class CuponController : ApiController
    {
        string sEsquema = "";

        [System.Web.Http.HttpPost]
        public HttpResponseMessage fn_ObtenerEstadoCuponCanal(CuponEstadoInput pCuponEstadoInput)
        {
            RespuestaPostEnvioMultipleFiltro oRespuestaPostEnvioMultipleFiltro = null;
            try
            {


                string Api = "https://us-central1-api-fasty-produccion.cloudfunctions.net/app/api/users/login";
                string Api_envio = "https://us-central1-api-cliente-cobranzas-dev.cloudfunctions.net/app/api/cobranzas";
                string JSON = "";
                string Token = "";
                string sResultado = "";
                Console.WriteLine("****Conectando API******");
                LoginPost oLogin = new LoginPost();
                oLogin.email = "fasty.entel@insolutions.pe";
                oLogin.password = "3Qv6M8#@w$N97Kqr";
                string ResToken = "";
                JSON = JsonConvert.SerializeObject(oLogin);
                Console.WriteLine("****JSON******" + JSON);

                //JSON = "{'email':'fasty.entel@insolutions.pe','password':'3Qv6M8#@w$N97Kqr'}";
                //JSON = "{'email':'admindev@entel.pe','password':'vBE8!r36DT@sYhFt'}";

                using (var stringContent = new
              StringContent(JSON,
          System.Text.Encoding.UTF8, "application/json"))
                using (var client = new
                    HttpClient())
                {

                    Console.WriteLine("****API REST******");
                    var response = client.PostAsync(Api, stringContent);
                    var result = response.Result;
                    sResultado = result.ReasonPhrase;
                    var contex = response.Result.Content.ReadAsStringAsync();
                    ResultadoPost oResultado = JsonConvert.DeserializeObject<ResultadoPost>(contex.Result.ToString());
                    ResToken = oResultado.token;

                    Console.WriteLine("****TOKEN******" + ResToken);

                }
                oRespuestaPostEnvioMultipleFiltro = fn_obtenerCobranzasLote_OracleCanal(ResToken, pCuponEstadoInput.IDOperacion, pCuponEstadoInput.NumeroCupon,
                    pCuponEstadoInput.Canal, pCuponEstadoInput.CodigoConvenio
                    );


                return Request.CreateResponse(System.Net.HttpStatusCode.OK, oRespuestaPostEnvioMultipleFiltro);
            }

            catch (Exception ex)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [System.Web.Http.HttpPost]
        public HttpResponseMessage fn_ObtenerEstadoCupon(CuponEstadoInput pCuponEstadoInput)
        {
            RespuestaPostEnvioMultipleFiltro oRespuestaPostEnvioMultipleFiltro = null;
            try
            {


                string Api = "https://us-central1-api-fasty-produccion.cloudfunctions.net/app/api/users/login";
                string Api_envio = "https://us-central1-api-cliente-cobranzas-dev.cloudfunctions.net/app/api/cobranzas";
                string JSON = "";
                string Token = "";
                string sResultado = "";
                Console.WriteLine("****Conectando API******");
                LoginPost oLogin = new LoginPost();
                oLogin.email = "fasty.entel@insolutions.pe";
                oLogin.password = "3Qv6M8#@w$N97Kqr";
                string ResToken = "";
                JSON = JsonConvert.SerializeObject(oLogin);
                Console.WriteLine("****JSON******" + JSON);

                //JSON = "{'email':'fasty.entel@insolutions.pe','password':'3Qv6M8#@w$N97Kqr'}";
                //JSON = "{'email':'admindev@entel.pe','password':'vBE8!r36DT@sYhFt'}";

                using (var stringContent = new
              StringContent(JSON,
          System.Text.Encoding.UTF8, "application/json"))
                using (var client = new
                    HttpClient())
                {

                    Console.WriteLine("****API REST******");
                    var response = client.PostAsync(Api, stringContent);
                    var result = response.Result;
                    sResultado = result.ReasonPhrase;
                    var contex = response.Result.Content.ReadAsStringAsync();
                    ResultadoPost oResultado = JsonConvert.DeserializeObject<ResultadoPost>(contex.Result.ToString());
                    ResToken = oResultado.token;

                    Console.WriteLine("****TOKEN******" + ResToken);

                }
                oRespuestaPostEnvioMultipleFiltro = fn_obtenerCobranzasLote_Oracle(ResToken, pCuponEstadoInput.IDOperacion, pCuponEstadoInput.NumeroCupon);


                return Request.CreateResponse(System.Net.HttpStatusCode.OK, oRespuestaPostEnvioMultipleFiltro);
            }

            catch (Exception ex)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
        }

        public RespuestaPostEnvioMultipleFiltro fn_obtenerCobranzasLote_OracleCanal(string ResToken,
            string sIDOperacion_2, string pNumeroCupon, string pCanal, string pCodigoConvenio)
        {
            //string Api_envio = "https://us-central1-api-cliente-cobranzas-dev.cloudfunctions.net/app/api/cobranzas/lote/obtenerCobranzas";
            string Api_envio = "https://us-central1-api-fasty-produccion.cloudfunctions.net/app/api/cobranzas/lote/obtenerCobranzas";

            RespuestaPostEnvioMultipleFiltro oResultado = new RespuestaPostEnvioMultipleFiltro();
            string JSON = "";
            //JSON = "{""canal"":""OP"",""codConvenio"":13003,""codOperacion"":2010,""empresaOrigen"":""Entel"",""fechaVencimiento"":""2021-05-30"",""idCliente"":""TESTDESARROLLO"",""idOperacion"":2022,""importe"":1,""moneda"":""PEN"",""nombreCliente"":""Contoso""}";
            string sResultado = "";


            //Console.WriteLine("sQueryOracle: " + sQueryOracle);

            //Console.WriteLine("cupones pendiente: " + oObj_EECC.Rows.Count);


            string NRO_CUPON = "";
            string[] arrCupones = new string[1];

            int xCCupo = 0;
            string ID_SOCIO = "";
            string NroFactura_2 = "";

            string sQuery = "";
            //foreach (DataRow oRows in oObj_EECC.Rows)
            //{
            xCCupo = 0;
            EnvioPostLote oEnvioPost = new EnvioPostLote();

            //oEnvioPost.canal = "OP";//pcanal; CANAL DE OPERACIONES
            //oEnvioPost.codConvenio = "13003"; //convenio ISOLUTIONS CON ENTEL 13004 PROD 13002 PRODUCCION
            //oEnvioPost.codOperacion = "1020"; // AMBOS
            //oEnvioPost.empresaOrigen = "ENTEL";//AMBOS

            oEnvioPost.canal = pCanal; // "OP";//pcanal; CANAL DE OPERACIONES
            oEnvioPost.codConvenio = pCodigoConvenio; //"13002"; //convenio ISOLUTIONS CON ENTEL 13004 PROD 13002 PRODUCCION
            oEnvioPost.codOperacion = "1020"; // AMBOS
            oEnvioPost.empresaOrigen = "ENTEL";//AMBOS 
                                               //sIDOperacion_2 = oRows["IDOPERACION"].ToString();
            oEnvioPost.idOperacion = sIDOperacion_2; //oRows["IDOPERACION"].ToString();//AMBOS                 
                                                     //NRO_CUPON = oRows["NUMEROCUPON"].ToString();
            arrCupones[xCCupo] = pNumeroCupon;

            oEnvioPost.listadoCodigoCobranza = arrCupones;

            JSON = JsonConvert.SerializeObject(oEnvioPost);

            Console.WriteLine("JSON CUPON: " + JSON);

            Console.WriteLine("****JSON ENVIO ******" + JSON);
            Console.WriteLine("****API REST ENVIO ******" + Api_envio);
            Console.WriteLine("****Enviando Serializacion con TOKEN....******" + Api_envio);

            Console.WriteLine("****NRO_CUPON ******" + NRO_CUPON);
            //Console.ReadLine();

            using (var stringContent = new
             StringContent(JSON,
         System.Text.Encoding.UTF8, "application/json"))
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue("Bearer", ResToken);

                Console.WriteLine("****Obteniendo respuesta POST Async....******" + Api_envio);

                var response = client.PostAsync(Api_envio, stringContent);

                //var result = await response.Content.ReadAsStringAsync();
                var result = response.Result;
                sResultado = result.ReasonPhrase;
                var contex = response.Result.Content.ReadAsStringAsync();

                oResultado = JsonConvert.DeserializeObject<RespuestaPostEnvioMultipleFiltro>(contex.Result.ToString());

                if (oResultado.data != null)
                {
                    foreach (var pRes in oResultado.data)
                    {

                        string sEstado = pRes.estado.ToUpper();
                        string sCodigoEstado = "";


                        if (sEstado == "V")
                        {
                            sEstado = "PENDIENTE";
                            sCodigoEstado = "PEND";
                        }
                        else if (sEstado == "P")
                        {
                            sEstado = "CANCELADO";
                            sCodigoEstado = "CANC";

                        }
                        else if (sEstado == "E")
                        {
                            sEstado = "ANULADO";
                            sCodigoEstado = "ANU";
                        }

                        Console.WriteLine("ESTADO: " + sEstado);
                        Console.WriteLine("idTransaccionRegistro: " + pRes.idTransaccionRegistro);
                        Console.WriteLine("descripcionCobranza: " + pRes.descripcionCobranza);
                        Console.WriteLine("NRO_CUPON: " + NRO_CUPON);
                        Console.WriteLine("sIDOperacion_2: " + sIDOperacion_2);


                        Console.WriteLine("****sQuery Update******" + sQuery);
                    }
                }
                Console.WriteLine("****Transformando Respuesta a DeserializeObject RespuestaPostEnvio ******");

                /*
                Console.WriteLine("****transaccionId******" + oResultado.transaccionId);
                Console.WriteLine("****id******" + oResultado.id);
                Console.WriteLine("****fechaRegistro******" + oResultado.fechaRegistro);*/
                Console.WriteLine("****message******" + oResultado.message);
                xCCupo++;
            }
            //Console.ReadLine();
            // }
            return oResultado;
        }

        public RespuestaPostEnvioMultipleFiltro fn_obtenerCobranzasLote_Oracle(string ResToken, string sIDOperacion_2, string pNumeroCupon)
        {
            //string Api_envio = "https://us-central1-api-cliente-cobranzas-dev.cloudfunctions.net/app/api/cobranzas/lote/obtenerCobranzas";
            string Api_envio = "https://us-central1-api-fasty-produccion.cloudfunctions.net/app/api/cobranzas/lote/obtenerCobranzas";

            RespuestaPostEnvioMultipleFiltro oResultado = new RespuestaPostEnvioMultipleFiltro();
            string JSON = "";
            //JSON = "{""canal"":""OP"",""codConvenio"":13003,""codOperacion"":2010,""empresaOrigen"":""Entel"",""fechaVencimiento"":""2021-05-30"",""idCliente"":""TESTDESARROLLO"",""idOperacion"":2022,""importe"":1,""moneda"":""PEN"",""nombreCliente"":""Contoso""}";
            string sResultado = "";


            //Console.WriteLine("sQueryOracle: " + sQueryOracle);

            //Console.WriteLine("cupones pendiente: " + oObj_EECC.Rows.Count);


            string NRO_CUPON = "";
            string[] arrCupones = new string[1];

            int xCCupo = 0;
            string ID_SOCIO = "";
            string NroFactura_2 = "";

            string sQuery = "";
            //foreach (DataRow oRows in oObj_EECC.Rows)
            //{
            xCCupo = 0;
            EnvioPostLote oEnvioPost = new EnvioPostLote();

            //oEnvioPost.canal = "OP";//pcanal; CANAL DE OPERACIONES
            //oEnvioPost.codConvenio = "13003"; //convenio ISOLUTIONS CON ENTEL 13004 PROD 13002 PRODUCCION
            //oEnvioPost.codOperacion = "1020"; // AMBOS
            //oEnvioPost.empresaOrigen = "ENTEL";//AMBOS

            oEnvioPost.canal = "OP";//pcanal; CANAL DE OPERACIONES
            oEnvioPost.codConvenio = "13002"; //convenio ISOLUTIONS CON ENTEL 13004 PROD 13002 PRODUCCION
            oEnvioPost.codOperacion = "1020"; // AMBOS
            oEnvioPost.empresaOrigen = "ENTEL";//AMBOS 
                                               //sIDOperacion_2 = oRows["IDOPERACION"].ToString();
            oEnvioPost.idOperacion = sIDOperacion_2; //oRows["IDOPERACION"].ToString();//AMBOS                 
                                                     //NRO_CUPON = oRows["NUMEROCUPON"].ToString();
            arrCupones[xCCupo] = pNumeroCupon;

            oEnvioPost.listadoCodigoCobranza = arrCupones;

            JSON = JsonConvert.SerializeObject(oEnvioPost);

            Console.WriteLine("JSON CUPON: " + JSON);

            Console.WriteLine("****JSON ENVIO ******" + JSON);
            Console.WriteLine("****API REST ENVIO ******" + Api_envio);
            Console.WriteLine("****Enviando Serializacion con TOKEN....******" + Api_envio);

            Console.WriteLine("****NRO_CUPON ******" + NRO_CUPON);
            //Console.ReadLine();

            using (var stringContent = new
             StringContent(JSON,
         System.Text.Encoding.UTF8, "application/json"))
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue("Bearer", ResToken);

                Console.WriteLine("****Obteniendo respuesta POST Async....******" + Api_envio);

                var response = client.PostAsync(Api_envio, stringContent);

                //var result = await response.Content.ReadAsStringAsync();
                var result = response.Result;
                sResultado = result.ReasonPhrase;
                var contex = response.Result.Content.ReadAsStringAsync();

                oResultado = JsonConvert.DeserializeObject<RespuestaPostEnvioMultipleFiltro>(contex.Result.ToString());

                if (oResultado.data != null)
                {
                    foreach (var pRes in oResultado.data)
                    {

                        string sEstado = pRes.estado.ToUpper();
                        string sCodigoEstado = "";


                        if (sEstado == "V")
                        {
                            sEstado = "PENDIENTE";
                            sCodigoEstado = "PEND";
                        }
                        else if (sEstado == "P")
                        {
                            sEstado = "CANCELADO";
                            sCodigoEstado = "CANC";

                        }
                        else if (sEstado == "E")
                        {
                            sEstado = "ANULADO";
                            sCodigoEstado = "ANU";
                        }

                        Console.WriteLine("ESTADO: " + sEstado);
                        Console.WriteLine("idTransaccionRegistro: " + pRes.idTransaccionRegistro);
                        Console.WriteLine("descripcionCobranza: " + pRes.descripcionCobranza);
                        Console.WriteLine("NRO_CUPON: " + NRO_CUPON);
                        Console.WriteLine("sIDOperacion_2: " + sIDOperacion_2);


                        Console.WriteLine("****sQuery Update******" + sQuery);
                    }
                }
                Console.WriteLine("****Transformando Respuesta a DeserializeObject RespuestaPostEnvio ******");

                /*
                Console.WriteLine("****transaccionId******" + oResultado.transaccionId);
                Console.WriteLine("****id******" + oResultado.id);
                Console.WriteLine("****fechaRegistro******" + oResultado.fechaRegistro);*/
                Console.WriteLine("****message******" + oResultado.message);
                xCCupo++;
            }
            //Console.ReadLine();
            // }
            return oResultado;
        }



        [System.Web.Http.HttpPost]
        public HttpResponseMessage fn_ObtenerTicket(CuponInput pCuponInput)
        {
            RespuestaPostEnvio oRespuestaPostEnvio = new RespuestaPostEnvio();
            try
            {
                string sToken = fn_ApiConsumer(pCuponInput.email_token, pCuponInput.clave_token, pCuponInput.Api_token);
                oRespuestaPostEnvio = fn_EnviarComprobante(pCuponInput.Api_Cupon, sToken, pCuponInput.canal, pCuponInput.fechaVencimiento, pCuponInput.ImportePendiente,
                    pCuponInput.idCliente, pCuponInput.Socio, pCuponInput.idOperacion, pCuponInput.codConvenio);

                return Request.CreateResponse(System.Net.HttpStatusCode.OK, oRespuestaPostEnvio);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [System.Web.Http.HttpPost]
        public HttpResponseMessage fn_ActualizarFechaVencimientoCobranza(CuponInput pCuponInput)
        {
            RespuestaPostEnvio oRespuestaPostEnvio = new RespuestaPostEnvio();
            try
            {
                oRespuestaPostEnvio = fn_ApiPatch_Vencimiento(pCuponInput.email_token, pCuponInput.clave_token, pCuponInput.Api_token,

                    pCuponInput.Api_Patch,
                    pCuponInput.canal,pCuponInput.codOperacion,
                    pCuponInput.codConvenio, pCuponInput.idTrazabilidad, pCuponInput.codigoCobranzaAsociada,pCuponInput.fechaVencimiento 
                    
                    );

                return Request.CreateResponse(System.Net.HttpStatusCode.OK, oRespuestaPostEnvio);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
        }



        [System.Web.Http.HttpPost]
        public HttpResponseMessage fn_AnularCupon(CuponInput pCuponInput)
        {
            RespuestaPostEnvio oRespuestaPostEnvio = new RespuestaPostEnvio();
            try
            {
                oRespuestaPostEnvio = fn_ApiPatch(pCuponInput.email_token, pCuponInput.clave_token, pCuponInput.Api_token, pCuponInput.canal,
                    pCuponInput.codConvenio,
                    pCuponInput.Api_Patch, pCuponInput.codOperacion, pCuponInput.idOperacion, pCuponInput.empresaOrigen
                    );

                return Request.CreateResponse(System.Net.HttpStatusCode.OK, oRespuestaPostEnvio);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [System.Web.Http.HttpPost]
        public HttpResponseMessage fn_GenerarNotaCredito(CuponInput pCuponInput)
        {
            NotaCreditoOutput oNotaCreditoOutput = new NotaCreditoOutput();
            try
            {
                oNotaCreditoOutput = fn_GenerarNotaCredito_Back(pCuponInput.email_token, pCuponInput.clave_token, pCuponInput.Api_token,
                    pCuponInput.canal,
                    pCuponInput.codConvenio,
                    pCuponInput.Api_Patch, pCuponInput.codOperacion, pCuponInput.idOperacion, pCuponInput.numeroCupon, pCuponInput.importe,
                    pCuponInput.moneda, pCuponInput.referencia
                    );

                return Request.CreateResponse(System.Net.HttpStatusCode.OK, oNotaCreditoOutput);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
        }

        public static NotaCreditoOutput fn_GenerarNotaCredito_Back(
           string pmail, string ppassword,
                   string pAPI_LOGIN,

           string pcanal, string codConvenio,
                   string pAPI_POST, string pcodOperacion, string pidTrazabilidad,
                   string pcodigoCobranzaAsociada, string pmonto, string pmoneda, string preferencia
           )
        {
            NotaCreditoOutput oResultado = null;

            string accessToken = "";
            //string Api = "https://us-central1-api-cliente-cobranzas-dev.cloudfunctions.net/app/api/cobranzas/C403635";
            EnvioPost3 oEnvioPost = new EnvioPost3();
            //oEnvioPost.canal = "OP";//pcanal; CANAL DE OPERACIONES
            //oEnvioPost.codConvenio = "13003"; //convenio ISOLUTIONS CON ENTEL 13004 PROD
            //oEnvioPost.codConvenio = "13003";
            //oEnvioPost.codOperacion = "3010"; // AMBOS
            //oEnvioPost.empresaOrigen = "Entel";//AMBOS
            //oEnvioPost.idOperacion = "230000084" ;

            oEnvioPost.canal = pcanal;//pcanal; CANAL DE OPERACIONES
            oEnvioPost.codOperacion = pcodOperacion; // AMBOS
            oEnvioPost.codConvenio = codConvenio; //convenio ISOLUTIONS CON ENTEL 13004 PROD
            oEnvioPost.idTrazabilidad = pidTrazabilidad;
            oEnvioPost.codigoCobranzaAsociada = pcodigoCobranzaAsociada;
            oEnvioPost.monto = pmonto;
            oEnvioPost.moneda = pmoneda;
            oEnvioPost.referencia = preferencia;

            //oEnvioPost.codConvenio = pempresaOrigen;//AMBOS
            //oEnvioPost.idOperacion = pidOperacion;

            string JSON = JsonConvert.SerializeObject(oEnvioPost);

            //    oHttpClient.DefaultRequestHeaders.Authorization =new AuthenticationHeaderValue("Bearer", fn_ApiConsumer(
            //oResultado = JsonConvert.DeserializeObject<RespuestaPostEnvio>(responseJson.Result);

            string sResultado = "";

            using (var stringContent = new
       StringContent(JSON,
   System.Text.Encoding.UTF8, "application/json"))
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", fn_ApiConsumer(pmail, ppassword, pAPI_LOGIN));
                Console.WriteLine("****API REST******");
                var response = client.PostAsync(pAPI_POST, stringContent);
                var result = response.Result;
                sResultado = result.ReasonPhrase;
                var contex = response.Result.Content.ReadAsStringAsync();
                oResultado = JsonConvert.DeserializeObject<NotaCreditoOutput>(contex.Result.ToString());
                //ResToken = oResultado.token;

                //Console.WriteLine("****TOKEN******" + ResToken);
                //Cadena = Token;
                //Console.ReadLine();

            }



            return oResultado;

        }

        public static RespuestaPostEnvio fn_ApiPatch_Vencimiento(
            string pmail, string ppassword,
                    string pAPI_LOGIN,
                     
                    string pAPI_PATCH,
                    string pcanal, string pcodOperacion,
                    string pcodConvenio, string pidTrazabilidad, string pcodigoCobranzaAsociada,
                    string pfechaVencimiento

            )
        {
            RespuestaPostEnvio oResultado = null;

            string accessToken = "";
            //string Api = "https://us-central1-api-cliente-cobranzas-dev.cloudfunctions.net/app/api/cobranzas/C403635";
            EnvioPost4 oEnvioPost = new EnvioPost4();
            //oEnvioPost.canal = "OP";//pcanal; CANAL DE OPERACIONES
            //oEnvioPost.codConvenio = "13003"; //convenio ISOLUTIONS CON ENTEL 13004 PROD
            //oEnvioPost.codConvenio = "13003";
            //oEnvioPost.codOperacion = "3010"; // AMBOS
            //oEnvioPost.empresaOrigen = "Entel";//AMBOS
            //oEnvioPost.idOperacion = "230000084" ;

            oEnvioPost.canal = pcanal;//pcanal; CANAL DE OPERACIONES
            oEnvioPost.codOperacion = pcodOperacion; //convenio ISOLUTIONS CON ENTEL 13004 PROD
            oEnvioPost.codConvenio = pcodConvenio; // AMBOS
            oEnvioPost.idTrazabilidad = pidTrazabilidad;//AMBOS
            oEnvioPost.codigoCobranzaAsociada = pcodigoCobranzaAsociada;

            string[] sFecha = pfechaVencimiento.Split(@"/");
            //string[] sFecha = new string[2];
            oEnvioPost.fechaVencimiento = (sFecha[2] + "-" + sFecha[1] + "-" + sFecha[0]);

            //oEnvioPost.fechaVencimiento = pfechaVencimiento;
            
            string JSON = JsonConvert.SerializeObject(oEnvioPost);

            //throw new Exception(JSON);

            HttpClient oHttpClient = new HttpClient();
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), pAPI_PATCH)
            {
                Content = new StringContent(JSON,
                                    Encoding.UTF8,
                                    "application/json")//CONTENT-TYPE header
            };
            oHttpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", fn_ApiConsumer(
        //"admindev@entel.pe", "vBE8!r36DT@sYhFt", "https://us-central1-api-cliente-cobranzas-dev.cloudfunctions.net/app/api/users/login"));
        pmail, ppassword, pAPI_LOGIN));
            oHttpClient.DefaultRequestHeaders.ExpectContinue = false;
            var response = oHttpClient.SendAsync(request);

            //if (!response.Result.IsSuccessStatusCode)
            //{
            var responseCode = response.Result.StatusCode;
            var responseJson = response.Result.Content.ReadAsStringAsync();
            //throw new Exception($"Unexpected http response {responseCode}: {responseJson}");
            oResultado = JsonConvert.DeserializeObject<RespuestaPostEnvio>(responseJson.Result);

            //}

            /*
            Uri oUri = new Uri(pAPI_LOGIN);
 
            var oRes= StringExtension.PatchAsync(oHttpClient, oUri, new StringContent(JSON,
                                    Encoding.UTF8,
                                    "application/json"));

            // more things here
            using (var client = new HttpClient())
            {
                client.BaseAddress = oUri;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", 
                    fn_ApiConsumer(pmail, ppassword, pAPI_LOGIN));
                var method = "PATCH";
                var httpVerb = new HttpMethod(method);
                var httpRequestMessage =
                    new HttpRequestMessage(httpVerb, pAPI_PATCH)
                    {
                        Content = new StringContent(JSON,
                                    Encoding.UTF8,
                                    "application/json")
                    };
                try
                {
                    var response2 = client.SendAsync(httpRequestMessage);
                    
                    if (!response.Result.IsSuccessStatusCode)
                    {
                        var responseCode = response.Result.StatusCode;
                        var responseJson = response.Result.Content.ReadAsStringAsync();
                        //throw new Exception($"Unexpected http response {responseCode}: {responseJson}");
                        oResultado = JsonConvert.DeserializeObject<ResultadoPost>(responseJson.Result);

                        if(oResultado.message.Contains("LA COBRANZA YA ESTABA EXTORNADA"))
                        {
                            oResultado.codigoanulacion = "ANU";
                        }
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception($"Unexpected http response :"+ exception.Message);
                }
            }
            */

            //    HttpClient oHttpClient = new HttpClient();
            //    string requestAddress = "https://localhost:44318/api/odata/Employee/1";
            //string jsonBody = @"{ ""Department"": ""1""}";
            // or
            // string jsonBody = @"{ ""Department"": { ""Oid"": ""1"" }}";
            //StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            //var response = HttpClient.PatchAsyn(requestAddress, content);
            //Console.WriteLine(response);


            //oResultado.message = JSON;
            return oResultado;

        }

        public static RespuestaPostEnvio fn_ApiPatch(
            string pmail, string ppassword,
                    string pAPI_LOGIN,

            string pcanal, string codConvenio,
                    string pAPI_PATCH, string pcodOperacion, string pidOperacion,string pempresaOrigen

            )
        {
            RespuestaPostEnvio oResultado = null;

            string accessToken = "";
            //string Api = "https://us-central1-api-cliente-cobranzas-dev.cloudfunctions.net/app/api/cobranzas/C403635";
            EnvioPost2 oEnvioPost = new EnvioPost2();
            //oEnvioPost.canal = "OP";//pcanal; CANAL DE OPERACIONES
            //oEnvioPost.codConvenio = "13003"; //convenio ISOLUTIONS CON ENTEL 13004 PROD
            //oEnvioPost.codConvenio = "13003";
            //oEnvioPost.codOperacion = "3010"; // AMBOS
            //oEnvioPost.empresaOrigen = "Entel";//AMBOS
            //oEnvioPost.idOperacion = "230000084" ;

            oEnvioPost.canal = pcanal;//pcanal; CANAL DE OPERACIONES
            oEnvioPost.codConvenio = codConvenio; //convenio ISOLUTIONS CON ENTEL 13004 PROD
            oEnvioPost.codOperacion = pcodOperacion; // AMBOS
            oEnvioPost.empresaOrigen = pempresaOrigen;//AMBOS
            oEnvioPost.idOperacion = pidOperacion;

            string JSON = JsonConvert.SerializeObject(oEnvioPost);

            

            HttpClient oHttpClient = new HttpClient();
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), pAPI_PATCH)
            {
                Content = new StringContent(JSON,
                                    Encoding.UTF8,
                                    "application/json")//CONTENT-TYPE header
        };
            oHttpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", fn_ApiConsumer(
        //"admindev@entel.pe", "vBE8!r36DT@sYhFt", "https://us-central1-api-cliente-cobranzas-dev.cloudfunctions.net/app/api/users/login"));
        pmail, ppassword, pAPI_LOGIN));
            oHttpClient.DefaultRequestHeaders.ExpectContinue = false;
            var response = oHttpClient.SendAsync(request);

            //if (!response.Result.IsSuccessStatusCode)
            //{
            var responseCode = response.Result.StatusCode;
                var responseJson = response.Result.Content.ReadAsStringAsync();
                //throw new Exception($"Unexpected http response {responseCode}: {responseJson}");
                oResultado = JsonConvert.DeserializeObject<RespuestaPostEnvio>(responseJson.Result);

            //}

            /*
            Uri oUri = new Uri(pAPI_LOGIN);
 
            var oRes= StringExtension.PatchAsync(oHttpClient, oUri, new StringContent(JSON,
                                    Encoding.UTF8,
                                    "application/json"));

            // more things here
            using (var client = new HttpClient())
            {
                client.BaseAddress = oUri;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", 
                    fn_ApiConsumer(pmail, ppassword, pAPI_LOGIN));
                var method = "PATCH";
                var httpVerb = new HttpMethod(method);
                var httpRequestMessage =
                    new HttpRequestMessage(httpVerb, pAPI_PATCH)
                    {
                        Content = new StringContent(JSON,
                                    Encoding.UTF8,
                                    "application/json")
                    };
                try
                {
                    var response2 = client.SendAsync(httpRequestMessage);
                    
                    if (!response.Result.IsSuccessStatusCode)
                    {
                        var responseCode = response.Result.StatusCode;
                        var responseJson = response.Result.Content.ReadAsStringAsync();
                        //throw new Exception($"Unexpected http response {responseCode}: {responseJson}");
                        oResultado = JsonConvert.DeserializeObject<ResultadoPost>(responseJson.Result);

                        if(oResultado.message.Contains("LA COBRANZA YA ESTABA EXTORNADA"))
                        {
                            oResultado.codigoanulacion = "ANU";
                        }
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception($"Unexpected http response :"+ exception.Message);
                }
            }
            */

            //    HttpClient oHttpClient = new HttpClient();
            //    string requestAddress = "https://localhost:44318/api/odata/Employee/1";
            //string jsonBody = @"{ ""Department"": ""1""}";
            // or
            // string jsonBody = @"{ ""Department"": { ""Oid"": ""1"" }}";
            //StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            //var response = HttpClient.PatchAsyn(requestAddress, content);
            //Console.WriteLine(response);


            //oResultado.message = JSON;
            return oResultado;

             }


    public static string  fn_ApiConsumer(string email,string password,string Api)
        {
            //string Api = "https://us-central1-api-fasty-produccion.cloudfunctions.net/app/api/users/login";
            //string Api = "https://us-central1-api-cliente-cobranzas-dev.cloudfunctions.net/app/api/users/login";
            //string Api_envio = "https://us-central1-api-cliente-cobranzas-dev.cloudfunctions.net/app/api/cobranzas";
            //string urlid =     "https://us-central1-api-fasty-produccion.cloudfunctions.net/app/api/cobranzas";
            string JSON = "";
            string Token = "";
            string sResultado = "";
            Console.WriteLine("****Conectando API******");
            LoginPost oLogin = new LoginPost();
            //oLogin.email = "admindev@entel.pe";
            //oLogin.password = "vBE8!r36DT@sYhFt";
            oLogin.email = email;
            oLogin.password = password;
            string ResToken = "";
            JSON = JsonConvert.SerializeObject(oLogin);
            Console.WriteLine("****JSON******" + JSON);

            //JSON = "{'email':'fasty.entel@insolutions.pe','password':'3Qv6M8#@w$N97Kqr'}";
            //JSON = "{'email':'admindev@entel.pe','password':'vBE8!r36DT@sYhFt'}";

            using (var stringContent = new
          StringContent(JSON,
      System.Text.Encoding.UTF8, "application/json"))
            using (var client = new HttpClient())
            {

                Console.WriteLine("****API REST******");
                var response = client.PostAsync(Api, stringContent);
                var result = response.Result;
                sResultado = result.ReasonPhrase;
                var contex = response.Result.Content.ReadAsStringAsync();
                ResultadoPost oResultado = JsonConvert.DeserializeObject<ResultadoPost>(contex.Result.ToString());
                ResToken = oResultado.token;

                Console.WriteLine("****TOKEN******" + ResToken);
                //Cadena = Token;
                //Console.ReadLine();

            }

            return ResToken;
        }



            public RespuestaPostEnvio fn_EnviarComprobante(
                    string Api_envio, string ResToken, string pcanal, string pfechaVencimiento, string pImportePendiente,
                    string pidCliente, string pSocio, string pidOperacion,string codConvenio)
        {
            RespuestaPostEnvio oResultado = new RespuestaPostEnvio();
            string JSON = "";
            //JSON = "{""canal"":""OP"",""codConvenio"":13003,""codOperacion"":2010,""empresaOrigen"":""Entel"",""fechaVencimiento"":""2021-05-30"",""idCliente"":""TESTDESARROLLO"",""idOperacion"":2022,""importe"":1,""moneda"":""PEN"",""nombreCliente"":""Contoso""}";
            string sResultado = "";
            EnvioPost oEnvioPost = new EnvioPost();
            oEnvioPost.canal = pcanal;//pcanal; CANAL DE OPERACIONES
            //oEnvioPost.codConvenio = "13003"; //convenio ISOLUTIONS CON ENTEL 13004 PROD
            oEnvioPost.codConvenio = codConvenio;
            oEnvioPost.codOperacion = "2010"; // AMBOS
            oEnvioPost.empresaOrigen = "Entel";//AMBOS
            string[] sFecha = pfechaVencimiento.Split(@"/");
            //string[] sFecha = new string[2];
            oEnvioPost.fechaVencimiento = (sFecha[2] + "-" + sFecha[1] + "-" + sFecha[0]);
            oEnvioPost.idCliente = pidCliente;
            oEnvioPost.idOperacion = pidOperacion.Replace("-", "");
            oEnvioPost.importe = Convert.ToDecimal(pImportePendiente.Replace(",", "")).ToString();
            oEnvioPost.moneda = "PEN";
            oEnvioPost.nombreCliente = pSocio;

            JSON = JsonConvert.SerializeObject(oEnvioPost);

            Console.WriteLine("****JSON ENVIO ******" + JSON);
            Console.WriteLine("****API REST ENVIO ******" + Api_envio);
            Console.WriteLine("****Enviando Serializacion con TOKEN....******" + Api_envio);
              
            using (var stringContent = new
             StringContent(JSON,
         System.Text.Encoding.UTF8, "application/json"))
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue("Bearer", ResToken);

                Console.WriteLine("****Obteniendo respuesta POST Async....******" + Api_envio);

                var response = client.PostAsync(Api_envio, stringContent);

                //var result = await response.Content.ReadAsStringAsync();
                var result = response.Result;
                sResultado = result.ReasonPhrase;
                var contex = response.Result.Content.ReadAsStringAsync();

                oResultado = JsonConvert.DeserializeObject<RespuestaPostEnvio>(contex.Result.ToString());

                Console.WriteLine("****Transformando Respuesta a DeserializeObject RespuestaPostEnvio ******");
                Console.WriteLine("****transaccionId******" + oResultado.transaccionId);
                Console.WriteLine("****id******" + oResultado.id);
                Console.WriteLine("****fechaRegistro******" + oResultado.fechaRegistro);
                Console.WriteLine("****message******" + oResultado.message);
                //Console.ReadLine();
            }
             
            //oResultado.transaccionId = "C410"+ DateTime.Now.ToShortDateString(); ;
            return oResultado;
        }

    }
}

