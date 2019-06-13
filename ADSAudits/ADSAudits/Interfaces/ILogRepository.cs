using ADSAudits.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSAudits.Interfaces
{
    public interface ILogRepository
    {
        Task<IEnumerable<LogModel>> GetAllLogAsync(int page);

        Task<LogModel> GetLogAsync(string id);

        // query after multiple parameters
        Task<IEnumerable<LogModel>> GetLog(string bodyText, DateTime updatedFrom);

        // add new note document
        Task AddLogAsync(LogModel item);

        // remove a single document / note
        Task<bool> RemoveLog(string id);

        // update just a single document / note
        Task<bool> UpdateLog(string id, string body);

        // demo interface - full document update
        Task<bool> UpdateLogDocument(string id, string body);

        // should be used with high cautious, only in relation with demo setup
        Task<bool> RemoveAllLog();

        // creates a sample index
        Task<string> CreateIndex();
    }
}
