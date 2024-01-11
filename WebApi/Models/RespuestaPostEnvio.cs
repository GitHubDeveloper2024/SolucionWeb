using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class RespuestaPostEnvio
    {
        public string message { get; set; }
        public string id { get; set; }
        public string fechaRegistro { get; set; }
        public string transaccionId { get; set; }

    }
}