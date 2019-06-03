using ADSConfiguracion.DAL.Entities;
using ADSConfiguracion.DAL.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConfiguracion.DAL
{
    public class ADSConfigurationContext
    {
        private readonly IMongoDatabase _database = null;

        public ADSConfigurationContext(IOptions<DatabaseConfigurationModel> settings)
        {
            var connectionString = "";

            if (string.IsNullOrWhiteSpace(settings.Value.User)
                || string.IsNullOrWhiteSpace(settings.Value.Password))
            {
                connectionString = $"mongodb://{settings.Value.Host}";
            }
            else
            {
                connectionString = $"mongodb://{settings.Value.User}:{settings.Value.Password}@{settings.Value.Host}";
            }

            var client = new MongoClient(connectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Configuration> Configurations
        {
            get
            {
                return _database.GetCollection<Configuration>("Configuractons");
            }
        }
        public IMongoCollection<Service> Services
        {
            get
            {
                return _database.GetCollection<Service>("Services");
            }
        }
    }
}
