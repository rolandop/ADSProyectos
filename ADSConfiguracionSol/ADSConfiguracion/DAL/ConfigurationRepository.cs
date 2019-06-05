using ADSConfiguracion.DAL.Entities;
using ADSConfiguracion.DAL.Models;
using ADSConfiguracion.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConfiguracion.DAL
{
    public class ConfigurationRepository: IConfigurationRepository
    {
        private readonly ILogger<ConfigurationRepository> _logger;

        private readonly ADSConfigurationContext _context = null;

        public ConfigurationRepository(ILogger<ConfigurationRepository> logger,
                                IOptions<DatabaseConfigurationModel> settings)
        {
            _logger = logger;
            _context = new ADSConfigurationContext(settings);
        }

        public async Task<IEnumerable<Configuration>> GetConfigurationsAsync()
        {
            try
            {
                return await _context.Configurations
                        .Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw ex;
            }
        }

        // query after Id or InternalId (BSonId value)
        //
        public async Task<Configuration> GetConfigurationAsync(string id)
        {
            try
            {
                ObjectId idInterno = GetInternalId(id);
                return await _context.Configurations
                                .Find(config => config.Id == idInterno
                                        || config.ServiceId == id)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw ex;
            }
        }

        public async Task<Configuration> 
                    GetConfigurationAsync(string serviceId, 
                                                string environment, 
                                                string version,
                                                string section,
                                                string key)
        {
            try
            {
                ObjectId idInterno = GetInternalId(serviceId);
                return await _context.Configurations
                                .Find(config =>
                                           config.ServiceId == serviceId
                                        && config.Environment == environment
                                        && config.Section == section
                                        && config.ServiceVersion == version
                                        && config.Key == key)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw ex;
            }
        }

        public async Task<ICollection<Configuration>> 
                    GetConfigurationsAsync(string serviceId, string environment, string version)
        {
            try
            {
                ObjectId idInterno = GetInternalId(serviceId);
                return await _context.Configurations
                                .Find(config => 
                                           config.ServiceId == serviceId
                                        && config.Environment == environment
                                        && config.ServiceVersion == version)
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw ex;
            }
        }

        public async Task<ICollection<Configuration>>
                    GetGlobalConfigurationsAsync(string environment)
        {
            try
            {   
                return await _context.Configurations
                                .Find(config =>
                                        config.Environment == environment
                                        && config.ServiceId == "Global"
                                       )
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw ex;
            }
        }

        private ObjectId GetInternalId(string id)
        {
            ObjectId internalId;
            if (!ObjectId.TryParse(id, out internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }

        public async Task AddConfigurationAsync(Configuration configuration)
        {
            try
            {
                configuration.Id = ObjectId.Empty;

                await _context.Configurations.InsertOneAsync(configuration);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw ex;
            }
        }

        public async Task<bool> RemoveConfiguration(string id)
        {
            try
            {
                DeleteResult actionResult
                    = await _context.Configurations.DeleteOneAsync(
                        Builders<Configuration>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw ex;
            }
        }

        public async Task<bool> UpdateConfiguration(string id, string section, string clave, 
                                                string value, string description)
        {
            var objectId = GetInternalId(id);

            var filter = Builders<Configuration>.Filter
                                                    .Eq(s=> s.Id, objectId);

            var update = Builders<Configuration>.Update
                            .Set(s => s.Section, section)
                            .Set(s => s.Value, value)
                            .Set(s => s.Description, description)
                            .CurrentDate(s => s.UpdatedDate);

            try
            {
                UpdateResult actionResult
                    = await _context.Configurations.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw ex;
            }
        }

        public async Task<bool> UpdateConfiguration(string id, Configuration element)
        {
            try
            {
                ReplaceOneResult actionResult
                    = await _context.Configurations
                                    .ReplaceOneAsync(n => n.Id.Equals(id)
                                            , element
                                            , new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw ex;
            }
        }

      
        public async Task<bool> RemoveAllConfigurationsAsync()
        {
            try
            {
                DeleteResult actionResult
                    = await _context.Configurations.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw ex;
            }
        }

    }
}
