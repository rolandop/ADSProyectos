using ADSRegistroCivil.DAL;
using ADSRegistroCivil.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSRegistroCivil.Servicios
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRegistroCivilServicio
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identificacion"></param>
        /// <param name="consulta"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        Task<ResponsePersonaModel> ConsultaRegistroCivilAsync(string identificacion, string consulta);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identification"></param>
        /// <param name="consulta"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        Task<ResponsePersonaModel> ConsultaCiudadanoAsync(string identification, string consulta);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> GuardarLogAsync(ResponsePersonaModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identificación"></param>
        /// <returns></returns>
        ResponsePersonaModel BuscarPorIdentificacion(string identificación);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tiempo"></param>
        /// <returns></returns>
        bool ValidarActualizacion(ResponsePersonaModel model, int tiempo);

    }
}
