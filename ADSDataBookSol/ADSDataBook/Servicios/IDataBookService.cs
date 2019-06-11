using ADSDataBook.DAL.Entidades.MySql;
using ADSDataBook.DAL.Entidades.Oracle;
using ADSDataBook.DAL.Enums;
using ADSDataBook.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSDataBook.Servicios
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataBookService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identification"></param>
        /// <returns></returns>
        BaseCambios ConsultarDataBook(string identification);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool GuardarBaseIntermedia(BaseCambios model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool GuardarLog(BaseCambios model);
    }
}
