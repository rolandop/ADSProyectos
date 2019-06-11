using ADSRegistroCivil.DAL.Entidades.Ods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSRegistroCivil.DAL.Repositorio
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDatosPersonaRcRepositorio
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identificacion"></param>
        /// <returns></returns>
        DataClientesRc BuscarPorId(string identificacion);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> InsertarRegistroAsync(DataClientesRc model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> ActualizarRegistroAsync(DataClientesRc model);
    }
}
