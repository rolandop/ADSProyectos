using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADSConfiguracion.Utilities.Controller;
using ADSConfiguracion.Utilities.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Logging;

namespace ADSConfiguracion.Cliente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConfigurationService _configurationService;
        private readonly ApplicationPartManager _partManager;
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(
           IConfigurationService configurationService,
           ILogger<ValuesController> logger,
           ApplicationPartManager partManager)
        {
            _logger = logger;
            _configurationService = configurationService;
            _partManager = partManager;

        }



        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            _logger.LogInformation("ConfiguracionActual");

            return Content(_configurationService.GetConfigurationJson(), "application/json");
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
