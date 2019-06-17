using ADSConsultaPla.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConsultaPla.Services
{
    /// <summary>
    /// Interfaz de servicio de consulta Pla
    /// </summary>
    public interface IConsultaPlaService
    {
        /// <summary>
        /// Función que se va a ejecutar
        /// </summary>
        /// <param name="persona"></param>
        /// <returns></returns>
        DatosPersonaPlaModel ConsultaSisprevServicePost(DatosClienteModel persona);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="identificacion"></param>
      /// <param name="nombre"></param>
      /// <param name="aplicacion"></param>
      /// <returns></returns>
        string ConsultaSisprevServiceGet(string identificacion, string nombre, string aplicacion);

    }
}
