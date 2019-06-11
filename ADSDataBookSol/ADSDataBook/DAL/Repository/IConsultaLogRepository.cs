using ADSDataBook.DAL.Entidades.MySql;
using ADSDataBook.DAL.Entidades.Oracle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSDataBook.DAL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConsultaLogRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        bool CrearLog(BaseCambios model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        bool AddLog(ConsultaPersonaLog log, string con);

    }
}
