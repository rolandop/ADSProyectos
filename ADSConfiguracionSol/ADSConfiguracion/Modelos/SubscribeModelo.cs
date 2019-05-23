using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConfiguracion.Modelos
{
    public class SubscribeModelo
    {
        public string ServicioId { get; set; }

        public string ServicioVersion { get; set; }

        public string Ambiente { get; set; }

        public string UrlActualizacion { get; set; }
        public string UrlVerificacion { get; set; }
    }
}
