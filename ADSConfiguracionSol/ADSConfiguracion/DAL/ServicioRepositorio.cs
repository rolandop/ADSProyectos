using ADSConfiguracion.DAL.Entidades;
using ADSConfiguracion.DAL.Modelos;
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
    public class ServicioRepositorio : IServicioRepositorio
    {
        private readonly ILogger<ServicioRepositorio> _logger;
        private readonly ADSConfiguracionesContexto _contexto = null;

        public ServicioRepositorio(ILogger<ServicioRepositorio> logger, 
            IOptions<BaseDatosConfiguracionModelo> settings)
        {
            _logger = logger;
            _contexto = new ADSConfiguracionesContexto(settings);
        }

        public async Task<Servicio> ObtenerServicioAsync(string id, string ambiente, string version)
        {
            try
            {                
                return await _contexto.Servicios
                                .Find(srv =>
                                    (srv.ServicioId == id)
                                        && srv.Ambiente == ambiente
                                        && srv.ServicioVersion == version)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado.");
                throw ex;
            }
        }

        public async Task AgregarServicioAsync(Servicio servicio)
        {
            try
            {
                await _contexto.Servicios.InsertOneAsync(servicio);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado.");
                throw ex;
            }
        }

        public async Task<bool> ActualizarServicio(ObjectId id, string urlActualizacion, string urlVerificacion)
        {
            var filter = Builders<Servicio>
                            .Filter.Eq(s => s.Id, id);
                            
            var update = Builders<Servicio>.Update
                            .Set(s => s.UrlActualizacion, urlActualizacion)
                            .Set(s => s.UrlVerificacion, urlVerificacion)
                            .Set(s => s.Activo, true)
                            .CurrentDate(s => s.FechaActualizacion);

            try
            {
                UpdateResult actionResult
                    = await _contexto.Servicios.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado.");
                throw ex;
            }
        }

        public async Task<ICollection<Servicio>> ObtenerServiciosAsync(bool activo)
        {
            try
            {
                return await _contexto.Servicios
                                .Find(srv => srv.Activo == activo)
                                .ToListAsync();
                                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado.");
                throw ex;
            }
        }

        public async Task<bool> DesactivarServicioAsync(Servicio servicio)
        {
            var filter = Builders<Servicio>
                           .Filter.Eq(s => s.Id, servicio.Id);

            var update = Builders<Servicio>.Update
                            .Set(s => s.Activo, false)
                            .CurrentDate(s => s.FechaBaja)
                            .CurrentDate(s => s.FechaActualizacion);

            try
            {

                UpdateResult actionResult
                    = await _contexto.Servicios.UpdateOneAsync(filter, update);

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

        public async Task<bool> ActualizarIntentosAsync(Servicio servicio, int intentos)
        {
            var filter = Builders<Servicio>
                           .Filter.Eq(s => s.Id, servicio.Id);

            var update = Builders<Servicio>.Update                            
                            .Set(s => s.Intentos, intentos)
                            .CurrentDate(s => s.FechaActualizacion);

            try
            {

                UpdateResult actionResult
                    = await _contexto.Servicios.UpdateOneAsync(filter, update);

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
