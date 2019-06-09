using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConfiguration.Cliente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {        
        private readonly ILogger<LogsController> _logger;

        public LogsController(
           ILogger<LogsController> logger)
        {
            _logger = logger;            

        }

        [HttpGet("execute/{numlogs}")]
        public ActionResult<string> Execute(int numlogs)
        {
            try
            {
                var p = new
                {
                    Edad = 11,
                    Nombre = "Pepe"
                };

                var traceId = DateTime.Now.Ticks.ToString();

                for (var i = 1; i <= numlogs; i++)
                {
                    _logger.LogWarning(new EventId(i, traceId), "Log {i} de {total}, model = {@model}{}",
                            i, numlogs, p, new
                            {
                                valor2 = "mi mama me pega"
                            });

                    //Console.WriteLine("Log enviado..");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

            return Ok();
        }
    }
}
