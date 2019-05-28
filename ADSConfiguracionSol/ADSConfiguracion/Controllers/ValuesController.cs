using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ADSConfiguracion.DAL.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;

namespace ADSConfiguracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        IOptions<BaseDatosConfiguracionModelo> _settings;
        private readonly ILogger<ConfiguracionController> _logger;

        public ValuesController(ILogger<ConfiguracionController> logger,
                            IOptions<BaseDatosConfiguracionModelo> settings)
        {
            _logger = logger;
            _settings = settings;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var json = new
            {
                Hola = "yo",
                Jaja = "El mmismo"
            };

            _logger.LogInformation("Prueba de json en informacion {Json}", json);

            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("mongoSettings")]
        public ActionResult MongoSettings()
        {
            return Ok(_settings);
        }

        [HttpGet]
        [Route("restclient")]
        public ActionResult RestClient([FromQuery]string url, string path)
        {

            try
            {
                var clienteRest = new RestClient(url);
                var solicitud = new RestRequest(path);
                
                var respuesta = clienteRest.Execute(solicitud);

                _logger.LogError("Respuesta {@Respuesta}", respuesta);

                return Ok(respuesta.Content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No se pudo enviar la configuración al servicio ");
                return Ok(ex);
            }
            
        }

        [HttpGet]
        [Route("string")]
        public async Task<ActionResult>  String([FromQuery]string url, string path)
        {
            try
            {
                HttpClient client = new HttpClient();
                string json = await client.GetStringAsync($"{url}/{path}");
                _logger.LogError("Respuesta {@Respuesta}", json);

                return Ok(json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No se pudo enviar la configuración al servicio ");
                return Ok(ex);
            }
            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
