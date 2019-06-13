using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADSDataBook.DAL.Entidades.Oracle;
using ADSDataBook.DAL.Enums;
using ADSDataBook.DAL.Modelos;
using ADSDataBook.Servicios;
using ADSUtilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ADSDataBook.Controllers
{
    /// <summary>
    /// Controlador API para manejo de Integración con DataBook
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/v1")]
    [ApiController]
    public class DataBookController : ControllerBase
    {
        private readonly ILogger<DataBookService> _logger;
        //private readonly IConfiguration configuration;
        private readonly IDataBookService _configuracionServicio;

        /// <summary>
        ///Constructor sobre cargado
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="configuracionServicio"></param>
        public DataBookController(ILogger<DataBookService> logger,
                    IDataBookService configuracionServicio
            )
        {
            _logger = logger;
            _configuracionServicio = configuracionServicio;
        }

        /// <summary>
        /// Servicio Rest Consulta a Servicio DataBook.
        /// </summary>
        /// <param name="identification"> Numero de Identificación a Consultar</param>
        /// 
        /// <response code="200">Retorna cuando es exitóso</response>
        /// <response code="400">Retorna cuando es excepción</response>
        /// <response code="404">Retorna cuando es null la búsqueda</response>    
        /// <response code="500">Retorna cuando se produce un error interno del servicio</response>   
        [HttpGet]
        [Route("{identification}")]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        [ProducesResponseType(typeof(ResponseModel), 400)]
        [ProducesResponseType(typeof(ResponseModel), 404)]
        [ProducesResponseType(typeof(ResponseModel), 500)]
        public IActionResult Get (string identification)
        {
            try
            {
                _logger.LogInformation("Consulta Servicio DataBook cliente {@identification}", identification);
                var result = _configuracionServicio.ConsultarDataBook(identification);
                _logger.LogInformation("Termina Consulta Servicio DataBook cliente {@result}", result);
                if (result == null)
                {
                    _logger.LogInformation("Termina Proceso con respuesta HttpCode 404");
                    return this.ADSNotFound();
                }
                _logger.LogInformation("Guarda Informacion en PersonaLog {@Objeto}", result);
                var saveLog = _configuracionServicio.GuardarLog(result);
                if (saveLog != true)
                {
                    _logger.LogInformation("Termina Proceso con respuesta HttpCode 400, registro no grabado en Base Log");
                    return this.ADSBadRequest();
                }
                _logger.LogInformation("Termina Guardado Informacion en PersonaLog {@Objeto}", saveLog);
                _logger.LogInformation("Guarda Informacion en Base Intermedia {@Objeto}", result);
                var save = _configuracionServicio.GuardarBaseIntermedia(result);
                _logger.LogInformation("Termina Guardado Informacion en Base Intermedia {@Objeto}", save);
                if (save != true)
                {
                    _logger.LogInformation("Termina Proceso con respuesta HttpCode 400, registro no grabado en Base de Cambios");
                    return this.ADSBadRequest();

                }
                _logger.LogInformation("Termina Proceso con respuesta HttpCode 200, Respuesta de Servicio exitosa");
                return this.ADSOk(result);
                
            }
            catch (Exception e)
            { 
                _logger.LogError(e, "Error al Invocar Servicio DataBook");
                return this.ADSInternalError(e.Message);
            }
        }

    }
}