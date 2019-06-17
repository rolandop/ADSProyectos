using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADSConsultaPla.Models;
using ADSConsultaPla.Services;
using ADSUtilities;
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
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="consultaPlaService"></param>
        public ConsultaPlaController(ILogger<ConsultaPlaService> logger, IConsultaPlaService consultaPlaService)
        {
            this._logger = logger;
            this._consultaPlaService = consultaPlaService;
        }


        /// <summary>
        /// Consulta desde Sisprev
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
        /// 
        /// </summary>
        /// <param name="persona"></param>
        /// <returns></returns>
        [HttpPost("sisprev")]
        public IActionResult GetConsultaSisprev(DatosClienteModel persona)
        {
            try
            {
                _logger.LogInformation("Inicio Consulta Pla {@persona}", persona);
                var result = _consultaPlaService.ConsultaSisprevServicePost(persona);
                if (result != null)
                {
                    _logger.LogInformation("Respuesta Consulta Pla: {@persona} {@result}", persona, result);
                    return this.ADSOk(result);
                }
                else
                {
                    _logger.LogInformation("Respuesta consulta GetConsultaSisprev: {@persona} No encontrado", persona);
                    return this.ADSNotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al llamar GetConsultaSisprev");
                return this.ADSBadRequest();
            }
        }
    }
}
