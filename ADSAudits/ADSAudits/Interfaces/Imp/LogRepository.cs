using ADSAudits.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using ADSAudits.DAL.Models;

namespace ADSAudits.Interfaces.Imp
{
    public class LogRepository : ILogRepository
    {
        private readonly LogContext _context = null;

        public LogRepository(LogContext context)
        {
            _context = context;
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

        public Task<string> CreateIndex()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LogModel>> GetAllLogAsync(int data)
        {
            try
            {
                return await _context.Log.Find(id => true)
                   .Skip(data*100)
                    .Limit(100)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LogModel> GetLogAsync(string id)
        {
            try
            {
                ObjectId internalId = GetInternalId(id);
                return await _context.Log
                                .Find(note => note.Id == internalId)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public Task<IEnumerable<LogModel>> GetLog(string bodyText, DateTime updatedFrom)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAllLog()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveLog(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateLog(string id, string body)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateLogDocument(string id, string body)
        {
            throw new NotImplementedException();
        }
        private ObjectId GetInternalId(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }
    }
}
