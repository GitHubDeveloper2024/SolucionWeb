using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class RespuestaPostEnvioMultipleFiltro
    {

        public string message { get; set; }
        public string fechaConsulta { get; set; }
        public string transaccionId { get; set; }

        public List<data> data { get; set; }


    }
}