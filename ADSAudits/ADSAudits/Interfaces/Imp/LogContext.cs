
using ADSAudits.DAL.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSAudits.Interfaces.Imp
{
    public class LogContext
    {
        private readonly IMongoDatabase _database = null;

        public LogContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<LogModel> Log
        {
            get
            {
                return _database.GetCollection<LogModel>("Logs");
            }
        }
    }
}
