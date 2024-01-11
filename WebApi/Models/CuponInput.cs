using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class CuponInput
    {
        public string email_token { get; set; }
        public string clave_token { get; set; }
        public string Api_token { get; set; }
        public string Api_Cupon { get; set; }
         
        public string canal { get; set; }

        public string fechaVencimiento { get; set; }
        public string ImportePendiente { get; set; }
        public string idCliente { get; set; }
        public string Socio { get; set; }
        public string idOperacion { get; set; }
        public string codConvenio { get; set; }

        public string Api_Patch { get; set; }
        public string codOperacion { get; set;}

        public string empresaOrigen { get; set; }

        public string numeroCupon { get; set; }
        public string importe { get; set; }

        public string referencia { get; set; }
        public string moneda { get; set; }

        public string idTrazabilidad { get; set; }

        public string codigoCobranzaAsociada { get; set; }

        //public string fechaVencimiento { get; set; }

        



    }
}