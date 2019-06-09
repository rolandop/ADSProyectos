using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Logging;
using ADSConfiguration.Utilities.Models;
using ADSConfiguration.Utilities.Services;
using Microsoft.Extensions.Configuration;

namespace ADSConfiguration.Utilities.Controller
{
    [Produces("application/json")]
    [Route("configuration")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {   
        private readonly ILogger<ConfigurationController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationProvider _configurationProvider;

        public ConfigurationController(
            ILogger<ConfigurationController> logger,
            IConfiguration configuration,
            IConfigurationBuilder builder)
        {
            _logger = logger;
            _configuration = configuration;
            //_configurationProvider = configurationProvider;
        }

        [HttpGet]
        [Route("ping")]
        public ActionResult Ping()
        {
            _logger.LogInformation("Ping from {@Url}", Request.PathBase);

            return Ok("Pong");
        }


        [HttpPost]
        [Route("")]
        public ActionResult Update([FromBody]ConfigurationJsonModel configuration)
        {
            _logger.LogInformation("Actualizar configuración {@Configuracion}", configuration);
            //_configurationService.UpdateConfiguration(configuration.ConfigurationJson);


            //return Ok(_configurationService.GetConfigurationJson());
            return Ok();
        }

        [HttpGet]
        [Route("")]
        public ActionResult CurrentConfiguration()
        {
            _logger.LogInformation("ConfiguracionActual");

            //return Content(_configurationService.GetConfigurationJson(), "application/json");

            return Content("");
        }

        [HttpGet]
        [Route("value/{key}")]
        public ActionResult Value(string key)
        {
            var valor = _configuration.GetSection(key)?.Value;
            _logger.LogInformation("Valor= {@Valor}", valor);

            return Ok(valor);
        }

        [HttpGet]
        [Route("subscribe")]
        public ActionResult Subscribe()
        {
            try
            {
                _logger.LogInformation("subscribe");

                //_configurationService.SubscribeService();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al suscribir");
                return StatusCode(500, ex.Message);
            }
           
        }
    }
}
