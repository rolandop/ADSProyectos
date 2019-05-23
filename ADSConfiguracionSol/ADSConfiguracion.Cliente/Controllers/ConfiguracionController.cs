using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADSConfiguracion.Cliente.Configuracion.Modelos;
using ADSConfiguracion.Cliente.Configuracion.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ADSConfiguracion.Cliente.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracionController : ControllerBase
    {
        private readonly ILogger<ConfiguracionServicio> _logger;
        private readonly IConfiguracionServicio _configuracionServicio;

        public ConfiguracionController(ILogger<ConfiguracionServicio> logger,
                    IConfiguracionServicio configuracionServicio
            )
        {
            _logger = logger;
            _configuracionServicio = configuracionServicio;
        }

        [HttpGet]
        [Route("Ping")]
        public ActionResult Ping()
        {
            _logger.LogInformation("Ping from {@Url}", Request.PathBase);

            return Ok();
        }


        [HttpPost]
        [Route("")]
        public ActionResult Actualizar([FromBody]ConfiguracionJsonModelo configuracion)
        {
            _logger.LogInformation("Actualizar configuración {@Configuracion}", configuracion);
            _configuracionServicio.ActualizarConfiguracion(configuracion.ConfiguracionJson);
            
            return Ok(_configuracionServicio.ObtenerConfiguracionJson());
        }

        [HttpGet]
        [Route("")]
        public ActionResult ConfiguracionActual()
        {
            _logger.LogInformation("ConfiguracionActual");
            
            return Content(_configuracionServicio.ObtenerConfiguracionJson(), "application/json");
        }
    }
}