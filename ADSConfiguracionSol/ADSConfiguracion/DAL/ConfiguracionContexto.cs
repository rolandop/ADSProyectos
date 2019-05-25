using ADSConfiguracion.DAL.Entidades;
using ADSConfiguracion.DAL.Modelos;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConfiguracion.DAL
{
    public class ADSConfiguracionesContexto
    {
        private readonly IMongoDatabase _database = null;

        public ADSConfiguracionesContexto(IOptions<BaseDatosConfiguracionModelo> settings)
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

        public IMongoCollection<Configuracion> Configuraciones
        {
            get
            {
                return _database.GetCollection<Configuracion>("Configuraciones");
            }
        }
        public IMongoCollection<Servicio> Servicios
        {
            get
            {
                return _database.GetCollection<Servicio>("Servicios");
            }
        }
    }
}
