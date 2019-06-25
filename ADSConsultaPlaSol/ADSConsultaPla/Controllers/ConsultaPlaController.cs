using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADSConsultaPla.Models;
using ADSConsultaPla.Services;
using ADSUtilities;
using ADSUtilities.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ADSConsultaPla.Controllers
{
    /// <summary>
    /// Controlador Consulta Pla Api v1
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/v1")]
    [ApiController]
    public class ConsultaPlaController : ControllerBase
    {
        private readonly ILogger<ConsultaPlaService> _logger;
        private readonly IConsultaPlaService _consultaPlaService;

        /// <summary>
        /// Constructor del controlador
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="consultaPlaService"></param>
        public ConsultaPlaController(ILogger<ConsultaPlaService> logger, IConsultaPlaService consultaPlaService)
        {
            this._logger = logger;
            this._consultaPlaService = consultaPlaService;
        }


        /// <summary>
        /// Consulta Sisprev para devolver solo si está en lista negra o no
        /// </summary>
        /// <param name="identificacion">Identificación</param>
        /// <param name="nombre">Nombre</param>
        /// <param name="app">Aplicación que consulta</param>
        /// <returns></returns>
        [HttpGet("sisprev/{identificacion}/{nombre}/{app}")]
        public IActionResult GetConsultaSisprev(string identificacion, string nombre, string app)
        {
            try
            {
                _logger.LogInformation("Inicio Consulta Pla {@identificacion} {@nombre} {@app}", identificacion, nombre, app);
                if (string.IsNullOrEmpty(identificacion) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(app))
                {
                    _logger.LogError("Error al llamar GetConsultaSisprev: Url incorrecta");
                    return this.ADSBadRequest();
                }
                else
                {
                    var result = _consultaPlaService.ConsultaSisprevServiceGet(identificacion, nombre, app);
                    if (!string.IsNullOrEmpty(result))
                    {
                        _logger.LogInformation("Respuesta Consulta Pla: {@identificacion} {@result}", identificacion, result);
                        return this.ADSOk(result);
                    }
                    else
                    {
                        _logger.LogInformation("Respuesta consulta GetConsultaSisprev: {@identificacion} No encontrado", identificacion);
                        return this.ADSNotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al llamar GetConsultaSisprev");
                return this.ADSBadRequest();
            }
        }

        /// <summary>
        /// Consulta Sisprev para obtener datos completos de la persona
        /// </summary>
        /// <param name="persona"></param>
        /// <returns></returns>
        [HttpPost("sisprev")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        [ProducesResponseType(typeof(IActionResult), 404)]
        [ProducesResponseType(typeof(IActionResult), 500)]
        public IActionResult GetConsultaSisprev([FromBody] DatosClienteModel persona)
        {
            try
            {
                this.TraceId("GetConsultaSisprev_" + DateTime.Now.Ticks);
                _logger.LogInformation("Inicio Consulta Sisprev {@persona}", persona);
                if (ModelState.IsValid)
                {
                    if (persona != null && string.IsNullOrEmpty(persona.Identification))
                    {
                        _logger.LogError("Error al llamar GetConsultaSisprev: No tiene identificación");
                        return this.ADSBadRequest();
                    }
                    else
                    {
                        _logger.LogInformation("Inicio llamada servicio ConsultaSisprevServicePost: {@persona}", persona);
                        var result = _consultaPlaService.ConsultaSisprevServicePost(persona);
                        _logger.LogInformation("Fin llamada servicio ConsultaSisprevServicePost");
                        if (result != null)
                        {
                            result.Observacion = persona.App;
                            _logger.LogInformation("Respuesta Consulta Pla: {@persona} {@result}", persona, result);
                            _logger.LogInformation("Fin Consulta Sisprev {@persona}", persona);
                            return this.ADSOk(result);
                        }
                        else
                        {
                            _logger.LogInformation("Respuesta consulta GetConsultaSisprev: {@persona} No encontrado", persona);
                            _logger.LogInformation("Fin Consulta Sisprev {@persona}", persona);
                            return this.ADSNotFound();
                        }
                    }
                }
                else
                {
                    _logger.LogError("Error en los datos enviados GetConsultaSisprev");
                    _logger.LogInformation("Fin Consulta Sisprev {@persona}", persona);
                    return this.ADSBadRequest();
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Consulta Sisprev");
                _logger.LogInformation("Fin Consulta Sisprev {@persona}", persona);
                return this.ADSInternalError();
            }
        }
    }
}
