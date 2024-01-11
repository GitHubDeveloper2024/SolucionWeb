using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class CuponEstadoInput
    {
        public string Canal { get; set; }

        public string CodigoConvenio { get; set; }
        public string IDOperacion { get; set; }
        public string NumeroCupon { get; set; }
    }
}