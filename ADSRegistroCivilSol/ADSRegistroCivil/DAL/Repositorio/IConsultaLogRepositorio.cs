using ADSRegistroCivil.DAL.Entidades.Siaerp;
using ADSRegistroCivil.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSRegistroCivil.DAL.Repositorio
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConsultaLogRepositorio
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        Task<bool> CrearLogAsync(ResponsePersonaModel info);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        bool AddLog(ConsultaPersonaLog log, string con);

    }
}
