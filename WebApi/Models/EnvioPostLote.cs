using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class EnvioPostLote
    {
        public string canal { get; set; }
        public string codConvenio { get; set; }
        public string codOperacion { get; set; }
        public string empresaOrigen { get; set; }

        public string idOperacion { get; set; }

        public string[] listadoCodigoCobranza { get; set; }

    }
}