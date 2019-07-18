using ADSConfiguration.Utilities.Services;
using ADSRegistroCivil.DAL.Contexto;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADSRegistroCivil.DAL.Entidades.Ods;

namespace ADSRegistroCivil.DAL.Repositorio
{
    /// <summary>
    /// 
    /// </summary>
    public class DatosPersonaRcRepositorio : IDatosPersonaRcRepositorio
    {
        private readonly IConfigurationService _configurationService;
        private readonly OdsContexto _oracleContext;
        private readonly ILogger<DatosPersonaRcRepositorio> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationService"></param>
        /// <param name="oracleContext"></param>
        /// <param name="logger"></param>
        public DatosPersonaRcRepositorio(
                        IConfigurationService configurationService,
                        OdsContexto oracleContext,
                        ILogger<DatosPersonaRcRepositorio> logger)
        {
            _oracleContext = oracleContext;
            _logger = logger;
            _configurationService = configurationService;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identificacion"></param>
        /// <returns></returns>
        public DataClientesRc BuscarPorId(string identificacion)
        {
            try
            {
                var result = _oracleContext.DataClientes.FirstOrDefault(x => x.IDENTIFICACION.Trim() == identificacion.Trim());
                return result;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> InsertarRegistroAsync(DataClientesRc model)
        {
            try
            {
                _oracleContext.DataClientes.Add(model);
                var result = await _oracleContext.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> ActualizarRegistroAsync (DataClientesRc model)
        {
            try
            {
                var result = _oracleContext.DataClientes.FirstOrDefault(x => x.IDENTIFICACION == model.IDENTIFICACION);
                if(result == null)
                {
                    var flag = await InsertarRegistroAsync(model);
                    return true;
                }
                result.IDENTIFICACION = model.IDENTIFICACION;
                result.NOMBRE_COMPLETO = model.NOMBRE_COMPLETO;
                result.ESTADO_CIVIL = model.ESTADO_CIVIL;
                result.FECHA_ACTUALIZACION = DateTime.Now.ToString("yyyy-MM-dd");
                result.FECHA_DEFUNCION = model.FECHA_DEFUNCION;
                result.PROFESION = model.PROFESION;
                await _oracleContext.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
