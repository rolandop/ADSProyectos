using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConfiguracion.Modelos
{
    public class ConfiguracionModelo
    {
        public string ServicioId { get; set; }
        public string ServicioVersion { get; set; }
        public string Ambiente { get; set; }
        public string Seccion { get; set; }
        public string Clave { get; set; }
        public string Valor { get; set; }
        public string Descripcion { get; set; }
        
    }
}
