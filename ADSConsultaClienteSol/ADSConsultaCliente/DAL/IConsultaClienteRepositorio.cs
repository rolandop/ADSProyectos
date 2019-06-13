using ADSConsultaCliente.DAL.Entidades;
using ADSConsultaCliente.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConsultaCliente.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConsultaClienteRepositorio
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
        /// <param name="persona"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        PersonaModel MapPersonaToModel(Persona persona, PersonaModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="universo"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        PersonaModel MapUniversoToModel(PersonaUniverso universo, PersonaModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registroCivil"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        PersonaModel MapRegitroCivilToModel(ResponseRegistroCivilModel registroCivil, PersonaModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databook"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        PersonaModel MapDatabookToModel(ResponseDatabookModel databook, PersonaModel model);

        
    }
}
