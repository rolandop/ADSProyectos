
using ADSAudits.DAL.Models;
using ADSConfiguracion.Utilities.Services;
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
        private readonly IConfigurationService _configurationService;

        public LogContext(IConfigurationService configurationService)
        {
            this._configurationService = configurationService;
            var s = this._configurationService.GetValue("ServiceConfigurationUrl");

            var client = new MongoClient(this._configurationService.GetValue("Global:MongoConnection:ConnectionString"));
            if (client != null)
                _database = client.GetDatabase(this._configurationService.GetValue("adsaudits:Database"));
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
