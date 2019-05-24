using ADSConfiguracion.DAL.Entidades;
using ADSConfiguracion.DAL.Modelos;
using ADSConfiguracion.Modelos;
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
    public class ConfiguracionRepositorio: IConfiguracionRepositorio
    {
        private readonly ILogger<ConfiguracionRepositorio> _logger;

        private readonly ADSConfiguracionesContexto _contexto = null;

        public ConfiguracionRepositorio(ILogger<ConfiguracionRepositorio> logger,
                                IOptions<BaseDatosConfiguracionModelo> settings)
        {
            _logger = logger;
            _contexto = new ADSConfiguracionesContexto(settings);
        }

        public async Task<IEnumerable<Configuracion>> ObtenerTodasConfiguraciones()
        {
            try
            {
                return await _contexto.Configuraciones
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
        public async Task<Configuracion> ObtenerConfiguracionAsync(string id)
        {
            try
            {
                ObjectId idInterno = ObtenerIdInterno(id);
                return await _contexto.Configuraciones
                                .Find(config => config.Id == idInterno
                                        || config.ServicioId == id)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw ex;
            }
        }

        public async Task<Configuracion> 
                    ObtenerConfiguracionAsync(string servivioId, 
                                                string ambiente, 
                                                string version,
                                                string seccion,
                                                string clave)
        {
            try
            {
                ObjectId idInterno = ObtenerIdInterno(servivioId);
                return await _contexto.Configuraciones
                                .Find(config =>
                                           config.ServicioId == servivioId
                                        && config.Ambiente == ambiente
                                        && config.Seccion == seccion
                                        && config.ServicioVersion == version
                                        && config.Clave == clave)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw ex;
            }
        }

        public async Task<ICollection<Configuracion>> 
                    ObtenerConfiguracionesAsync(string id, string ambiente, string version)
        {
            try
            {
                ObjectId idInterno = ObtenerIdInterno(id);
                return await _contexto.Configuraciones
                                .Find(config => 
                                           config.ServicioId == id
                                        && config.Ambiente == ambiente
                                        && config.ServicioVersion == version)
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw ex;
            }
        }

        public async Task<ICollection<Configuracion>>
                    ObtenerConfiguracionesGlobalesAsync(string ambiente)
        {
            try
            {   
                return await _contexto.Configuraciones
                                .Find(config =>
                                        config.Ambiente == ambiente
                                        && config.ServicioId == "Global"
                                       )
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw ex;
            }
        }

        private ObjectId ObtenerIdInterno(string id)
        {
            ObjectId internalId;
            if (!ObjectId.TryParse(id, out internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }

        public async Task AgregarConfiguracionAsync(Configuracion elemento)
        {
            try
            {
                elemento.Id = ObjectId.Empty;

                await _contexto.Configuraciones.InsertOneAsync(elemento);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw ex;
            }
        }

        public async Task<bool> EliminarConfiguracion(string id)
        {
            try
            {
                DeleteResult actionResult
                    = await _contexto.Configuraciones.DeleteOneAsync(
                        Builders<Configuracion>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw ex;
            }
        }

        public async Task<bool> ActualizarConfiguracion(string id, string seccion, string clave, 
                                                string valor, string descripcion)
        {
            var objectId = ObtenerIdInterno(id);

            var filter = Builders<Configuracion>.Filter
                                                    .Eq(s=> s.Id, objectId);

            var update = Builders<Configuracion>.Update
                            .Set(s => s.Seccion, seccion)
                            .Set(s => s.Valor, valor)
                            .Set(s => s.Descripcion, descripcion)
                            .CurrentDate(s => s.FechaActualizacion);

            try
            {
                UpdateResult actionResult
                    = await _contexto.Configuraciones.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw ex;
            }
        }

        public async Task<bool> ActualizarConfiguracion(string id, Configuracion elemento)
        {
            try
            {
                ReplaceOneResult actionResult
                    = await _contexto.Configuraciones
                                    .ReplaceOneAsync(n => n.Id.Equals(id)
                                            , elemento
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

      
        public async Task<bool> EliminarTodasConfiguracionesAsync()
        {
            try
            {
                DeleteResult actionResult
                    = await _contexto.Configuraciones.DeleteManyAsync(new BsonDocument());

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
