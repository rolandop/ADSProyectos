using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ADSConfiguracion.DAL;
using ADSConfiguracion.DAL.Entities;
using ADSConfiguracion.Models;
using ADSConfiguracion.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace ADSConfiguracion.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly ILogger<ConfigurationController> _logger;        
        private readonly IConfigurationService _configurationService;
        private readonly IServiceService _serviceService;

        public ConfigurationController(ILogger<ConfigurationController> logger,           
                                        IConfigurationService configurationService,
                                        IServiceService serviceService)
        {
            _logger = logger;
            _serviceService = serviceService;
            _configurationService = configurationService;
        }

        /// <summary>
        /// Obtiene la configuración actual de un servicio
        /// </summary>
        /// <param name="id">Id del servicio</param>   
        /// <param name="environment">Ambiente de despliegue PRD = Producción, TST= Pruebas, DEV= Desallollo</param>   
        /// <param name="version">Versión del servicio</param>   
        [HttpGet("{id}/{environment}/{version}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        
        public async Task<IActionResult> Get(string id, string environment, string version)
        {
            try
            {
                _logger.LogInformation("Get id={id} ambiente={ambiente} version={version}", id, environment, version);

                var configs = await _configurationService.GetConfigurationService(id, environment, version);

                if (configs == null)
                {
                    _logger.LogInformation("Get NotFound");

                    return NotFound();
                }

                return Ok(configs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
           
        }
        
        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] SubscribeModel subscribe)
        {
            _logger.LogInformation("Subscribe {@Modelo}", subscribe);

            if (string.IsNullOrWhiteSpace(subscribe.ServiceId) ||
                string.IsNullOrWhiteSpace(subscribe.ServiceVersion) ||
                string.IsNullOrWhiteSpace(subscribe.Environment))
            {
                _logger.LogInformation("Información del servicio no puede ser nula");
                return BadRequest();
            }

            await _configurationService.Subscribe(subscribe);

            var configs = await _configurationService
                                        .GetConfigurationService(subscribe.ServiceId, 
                                                subscribe.Environment, 
                                                subscribe.ServiceVersion);

            if (configs == null)
            {
                _logger.LogInformation("Subscribe NotFound");
                return NotFound();                
            }

            _logger.LogInformation("Subscribe {@subscribe}", subscribe);

            return Ok(configs);
        }

        [HttpPut("clone")]
        public async Task<IActionResult> Clone([FromBody] CloneModel clonar)
        {
            _logger.LogInformation("Clone {@Modelo}", clonar);

            await _configurationService.Clonar(clonar);

            var configs = await _configurationService.GetConfigurationService(clonar.ServiceId, 
                                                                clonar.NewEnvironment, 
                                                                clonar.NewVersion);

            if (configs == null)
            {
                _logger.LogInformation("Clonar NotFound");

                return NotFound();
            }

            _logger.LogInformation("Clonar {@ClonarModelo}.", clonar);

            return Ok(configs);

        }

        [HttpPost("notify/{id}/{environment}/{version}")]
        public async Task<IActionResult> Notify(string id, string environment, string version)
        {
            _logger.LogInformation("Notify id={id} environment={environment} version={version}", id, environment, version);

            try
            {
                await _configurationService.Notify(id, environment, version);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al notificar servicio {Servicio} {Ambiente} {Version} ",
                                    id, environment, version);

                return StatusCode(500);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] ICollection<ConfigurationModel> configurations)
        {
            _logger.LogInformation("( {@Modelo}", configurations);

            var result = await
                                _configurationService.Save(configurations);

            if (result)
            {
                _logger.LogInformation("Configuración agregada");
                return Ok();
            }
            else
            {
                _logger.LogError("No se crearon las configuraciones {@Configuraciones}", configurations);

                return NoContent();
            }
        }
    }
}