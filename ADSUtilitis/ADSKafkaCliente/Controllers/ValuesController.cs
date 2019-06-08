using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ADSUtilities;
using Microsoft.Extensions.Logging;

namespace ADSKafkaCliente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
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
                            i, numlogs, p, new {
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

        [HttpGet("error")]
        public ActionResult<string> Error(string error, string parametro)
        {
            try
            {
                var p = new
                {
                    Edad = 11,
                    Nombre = "Pepe"
                };

                
                _logger.LogError(error, parametro);
                Console.WriteLine("Error=" + error + "," + parametro);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

            return Ok();
        }

    }
}
