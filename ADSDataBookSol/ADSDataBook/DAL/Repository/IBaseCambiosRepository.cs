using ADSDataBook.DAL.Entidades.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSDataBook.DAL.Repository
{
    public interface IBaseCambiosRepository
    {
        bool GuardarBaseIntermedia(BaseCambios model);
    }
}
