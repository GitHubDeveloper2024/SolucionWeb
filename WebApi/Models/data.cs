using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class data
    {
        public string moneda { get; set; }
        public string codOperacion { get; set; }
        public string empresaOrigen { get; set; }
        public string nombreProveedorPago { get; set; }
        public string canal { get; set; }
        public string nombreCliente { get; set; }
        public string estado { get; set; }
        public string idCliente { get; set; }
        public string importeOriginal { get; set; }
        public string descripcionCobranza { get; set; }
        public string idOperacion { get; set; }
        public string fechaVencimiento { get; set; }
        public string fechaRegistro { get; set; }
        public string idTransaccionRegistro { get; set; }
        public string importe { get; set; }
        public string fechaActualizacionEstado { get; set; }
        public string codCliente { get; set; }
        public string codConvenio { get; set; }
        public string fechaVencimientoOriginal { get; set; }
        public string id { get; set; }
        public string fechaProcesoPago { get; set; }

    }
}