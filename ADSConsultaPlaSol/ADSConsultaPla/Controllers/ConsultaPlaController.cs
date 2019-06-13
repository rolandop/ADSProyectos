using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        /// <param name="identificacion"></param>
        /// <param name="nombre"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        [HttpGet("sisprev/{identificacion}/{nombre}/{app}")]
        public IActionResult GetConsultaSisprev(string identificacion, string nombre, string app)
        {
            try
            {
                _logger.LogInformation("Inicio Consulta Pla {@identificacion} {@nombre} {@app}", identificacion, nombre, app);
                var result = _consultaPlaService.ConsultaSisprevService(identificacion, nombre, app);
                if (!string.IsNullOrEmpty(result))
                {
                    _logger.LogInformation("Respuesta Consulta Pla: {@identificacion} {@result}", identificacion, result);
                    return this.ADSOk(result);
                }
                else
                {
                    _logger.LogInformation("Respuesta Consulta Pla: {@identificacion} {@result}", identificacion, result);
                    return this.ADSNotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Respuesta Consulta Pla: {@identificacion} {@Badrequest}", identificacion, ex.StackTrace);
                return this.ADSBadRequest();
            }
        }
    }
}
