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
    public interface ISiaerpRepositorio
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Usuario MapPersonaModelToUsuario(PersonaModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        bool SaveSiaerpNuevoUsuario(Usuario usuario);
    }
}
