using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConfiguracion.Modelos
{
    public class ClonarModelo
    {
        public string ServicioId { get; set; }

        public string ServicioVersion { get; set; }

        public string Ambiente { get; set; }

        public string NuevoServicioId { get; set; }
        public string NuevoAmbiente { get; set; }
        public string NuevaVersion { get; set; }
    }
}
