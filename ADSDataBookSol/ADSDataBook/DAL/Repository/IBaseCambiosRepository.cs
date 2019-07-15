//using ADSDataBook.DAL.Entidades.MySql;
using ADSDataBook.DAL.Entidades.Ruia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSDataBook.DAL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBaseCambiosRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool GuardarBaseIntermedia(BaseCambios model);
    }
}
