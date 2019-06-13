using ADSAuditsConsumer.DALL.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ADSAuditsConsumer.Interfaces
{
    public interface ILogRepository
    {
        Task AddLogAsync(LogModel item);
    }
}
