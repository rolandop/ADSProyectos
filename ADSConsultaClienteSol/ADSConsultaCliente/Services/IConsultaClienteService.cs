using ADSConsultaCliente.DAL.Entidades;
using ADSConsultaCliente.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConsultaCliente.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConsultaClienteService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identificacion"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        PersonaModel ObtenerPersonaServicioAsync(string identificacion);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="identificacion"></param>
      /// <param name="model"></param>
      /// <returns></returns>
        PersonaModel ObtenerPersonaUniversoServicioAsync(string identificacion, PersonaModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identificacion"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        PersonaModel ConsultaMasterDataAsync(string identificacion, bool rc);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identificacion"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        PersonaModel ConsultaDatabookAsync(string identificacion, PersonaModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identificacion"></param>
        /// <param name="consulta"></param>
        /// <param name="op"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        PersonaModel ConsultaRegistroCivilAsync(string identificacion, string consulta, string op, PersonaModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identificacion"></param>
        /// <param name="nombre"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        string ConsultaPlaAsync(string identificacion, string nombre, string app);
    }
}
