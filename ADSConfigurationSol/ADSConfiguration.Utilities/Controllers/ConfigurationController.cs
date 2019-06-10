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
        private readonly ADSConfigurationProvider _adsConfigurationProvider;

        public ConfigurationController(
            ILogger<ConfigurationController> logger,
            IConfiguration configuration
            //IConfigurationBuilder builder
            )
        {
            _logger = logger;
            _configuration = configuration;
            _adsConfigurationProvider  = (_configuration as ConfigurationRoot).Providers
                                        .Where(p => p is ADSConfigurationProvider)
                                        .Select(p => p as ADSConfigurationProvider)
                                        .FirstOrDefault();
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

            _adsConfigurationProvider.Update(configuration.ConfigurationJson);

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
        public ActionResult GetValue(string key)
        {
            var value = _configuration.GetSection(key)?.Value;
            _logger.LogInformation("Valor= {@value}", value);

            return Ok(value);
        }

        [HttpPost]
        [Route("{key}/{value}")]
        public ActionResult SetValue(string key, string value)
        {   
            _logger.LogInformation("Clave={@key}, Valor={@value}", key, value);

            _adsConfigurationProvider.SetValue(key, value);

            value = _configuration.GetSection(key)?.Value;
            _logger.LogInformation("Valor= {@value}", value);

            return Ok($"{key}={value}");
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
