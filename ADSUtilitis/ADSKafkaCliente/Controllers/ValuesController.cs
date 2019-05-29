using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADSLog;
using Microsoft.AspNetCore.Mvc;

namespace ADSKafkaCliente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
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

                var log = new Log();

                for (var i = 0; i < numlogs; i++)
                {
                    log.Warning("error", p, i.ToString());
                    Console.WriteLine("Send" + i.ToString());
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

                var log = new Log();
                log.Error(error, parametro);
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
