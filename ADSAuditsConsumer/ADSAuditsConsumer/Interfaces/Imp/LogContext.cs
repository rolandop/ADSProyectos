using ADSAuditsConsumer.DALL.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADSAuditsConsumer.Interfaces.Imp
{
    public class LogContext
    {
        private readonly IMongoDatabase _database = null;
        private string Database = "Logs";
        public LogContext(string ConnectionString)
        {
            var client = new MongoClient(ConnectionString);
            if (client != null)
                _database = client.GetDatabase(Database);
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
