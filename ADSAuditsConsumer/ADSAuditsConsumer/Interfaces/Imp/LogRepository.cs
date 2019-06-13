using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ADSAuditsConsumer.DALL.Model;
using Microsoft.Extensions.Options;

namespace ADSAuditsConsumer.Interfaces.Imp
{
    public class LogRepository : ILogRepository
    {
        private readonly LogContext _context = null;

        public LogRepository(IOptions<Settings> settings)
        {
            _context = new LogContext(settings.ToString());
        }

        public async Task AddLogAsync(LogModel item)
        {
            try
            {
                await _context.Log.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}

