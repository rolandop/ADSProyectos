using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConfiguracion.Cliente.Configuracion.Servicios
{
    public interface IConfiguracionServicio
    {
        string ObtenerConfiguracionJson();
        void ObtenerConfiguracion();
        void ActualizarConfiguracion(string configuracionJson);
        void SubscribirServicio();
    }
}
