using ADSConfiguracion.DAL.Entities;
using ADSConfiguracion.DAL.Models;
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
    public class ServiceRepository : IServiceRepository
    {
        private readonly ILogger<ServiceRepository> _logger;
        private readonly ADSConfigurationContext _context = null;

        public ServiceRepository(ILogger<ServiceRepository> logger, 
            IOptions<DatabaseConfigurationModel> settings)
        {
            _logger = logger;
            _context = new ADSConfigurationContext(settings);
        }

        public async Task<Service> GetServiceAsync(string serviceId, string environment, string version)
        {
            try
            {                
                return await _context.Services
                                .Find(srv =>
                                    (srv.ServiceId == serviceId)
                                        && srv.Environment == environment
                                        && srv.ServiceVersion == version)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado.");
                throw ex;
            }
        }

        public async Task AddServiceAsync(Service service)
        {
            try
            {
                await _context.Services.InsertOneAsync(service);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado.");
                throw ex;
            }
        }

        public async Task<bool> UpdateService(ObjectId id, string urlActualizacion, string urlVerificacion)
        {
            var filter = Builders<Service>
                            .Filter.Eq(s => s.Id, id);
                            
            var update = Builders<Service>.Update
                            .Set(s => s.UpdateUrl, urlActualizacion)
                            .Set(s => s.VerifyUrl, urlVerificacion)
                            .Set(s => s.Attempts, 0)
                            .Set(s => s.Active, true)
                            .CurrentDate(s => s.UpdatedDate);

            try
            {
                UpdateResult actionResult
                    = await _context.Services.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado.");
                throw ex;
            }
        }

        public async Task<ICollection<Service>> GetServicesAsync(bool active)
        {
            try
            {
                return await _context.Services
                                .Find(srv => srv.Active == active)
                                .ToListAsync();
                                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado.");
                throw ex;
            }
        }

        public async Task<bool> UnsubscribeServiceAsync(Service service)
        {
            var filter = Builders<Service>
                           .Filter.Eq(s => s.Id, service.Id);

            var update = Builders<Service>.Update
                            .Set(s => s.Active, false)
                            .CurrentDate(s => s.UnsubscribeDate)
                            .CurrentDate(s => s.UpdatedDate);

            try
            {

                UpdateResult actionResult
                    = await _context.Services.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No se pudo actualizar el servicio");

                return false;
            }
            
        }

        public void Dispose()
        {
            _logger.LogInformation("Dispose");
        }

        public async Task<bool> UpdateAttemptsAsync(Service service, int attempts)
        {
            var filter = Builders<Service>
                           .Filter.Eq(s => s.Id, service.Id);

            var update = Builders<Service>.Update                            
                            .Set(s => s.Attempts, attempts)
                            .CurrentDate(s => s.UpdatedDate);

            try
            {

                UpdateResult actionResult
                    = await _context.Services.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No se pudo actualizar numero de intentos al el servicio");

                return false;
            }
        }
    }
}
