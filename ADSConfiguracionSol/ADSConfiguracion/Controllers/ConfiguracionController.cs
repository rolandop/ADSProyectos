using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ADSConfiguracion.DAL;
using ADSConfiguracion.DAL.Entidades;
using ADSConfiguracion.Modelos;
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
    public class ConfiguracionController : ControllerBase
    {
        private readonly ILogger<ConfiguracionController> _logger;        
        private readonly IConfiguracionServicio _configuracionServicio;
        private readonly IServicioServicio _servicioServicio;

        public ConfiguracionController(ILogger<ConfiguracionController> logger,           
                                        IConfiguracionServicio configuracionServicio,
                                        IServicioServicio servicioServicio)
        {
            _logger = logger;
            _servicioServicio = servicioServicio;
            _configuracionServicio = configuracionServicio;
        }

        [HttpGet("{id}/{ambiente}/{version}")]
        public async Task<IActionResult> Get(string id, string ambiente, string version)
        {

            var configs = await _configuracionServicio.ObtenerConfiguracionServicio(id, ambiente, version);

            if (configs == null)
            {
                _logger.LogInformation("Notificar NotFound");

                return NotFound();
            }
            _logger.LogInformation("Get id={id} ambiente={ambiente} version={version}", id, ambiente, version);

            return Ok (configs);
        }


        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] SubscribeModelo subscribe)
        {
            await _configuracionServicio.Subscribe(subscribe);

            var configs = await _configuracionServicio.ObtenerConfiguracionServicio(subscribe.ServicioId, subscribe.Ambiente, subscribe.ServicioVersion);

            if (configs == null)
            {
                _logger.LogInformation("Subscribe NotFound");
                return NotFound();                
            }

            _logger.LogInformation("Subscribe {@subscribe}", subscribe);

            return Ok(configs);
        }

        [HttpPut("clonar")]
        public async Task<IActionResult> Clonar([FromBody] ClonarModelo clonar)
        {
            await _configuracionServicio.Clonar(clonar);

            var configs = await _configuracionServicio.ObtenerConfiguracionServicio(clonar.ServicioId, 
                                                                clonar.NuevoAmbiente, 
                                                                clonar.NuevaVersion);

            if (configs == null)
            {
                _logger.LogInformation("Clonar NotFound");

                return NotFound();
            }

            _logger.LogInformation("Clonar {@ClonarModelo}.", clonar);

            return Ok(configs);

        }

        [HttpPost("notificar/{id}/{ambiente}/{version}")]
        public async Task<IActionResult> Notificar(string id, string ambiente, string version)
        {
            var servicio = await
                                _servicioServicio.ObtenerServicioAsync(id, ambiente, version);

            if (servicio == null)
            {
                _logger.LogInformation("Notificar NotFound");

                return NotFound();
            }

            var clienteRest = new RestClient();
            var solicitud = new RestRequest(servicio.UrlActualizacion, Method.POST);

            var configs = await 
                                _configuracionServicio.ObtenerConfiguracionServicio(id, ambiente, version);
                
            solicitud.AddJsonBody(new {
                configuracionJson = JsonConvert.SerializeObject(configs)
            });            

            try
            {
                clienteRest.ExecuteAsync(solicitud, respuesta =>
                {
                    if (respuesta.StatusCode == HttpStatusCode.OK)
                    {
                        // OK
                    }
                    else
                    {
                        // NOK
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No se pudo enviar la configuración al servicio ", id);
            }

            return Ok(configs);

        }
    }
}