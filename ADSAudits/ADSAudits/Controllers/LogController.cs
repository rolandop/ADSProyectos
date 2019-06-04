using System;
using System.Threading.Tasks;
using ADSAudits.DAL.Models;
using ADSAudits.Infrastructure;
using ADSAudits.Interfaces;
using ADSAudits.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace ADSAudits.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/v1")]
    public class LogController : Controller
    {
        private readonly ILogRepository _logRepository;
        private readonly ILogger<LogController> _logger;
        public LogController(ILogRepository logRepository, ILogger<LogController> logger)
        {
            _logRepository = logRepository;
            _logger = logger;
        }

        [NoCache]
        [HttpGet("page/{page}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        public async Task<IActionResult> GetAsync(int page)
        {
            try
            {
                _logger.LogInformation("Inicio");
                var resul = await _logRepository.GetAllLogAsync(page);
                return Ok(ADSUtilities.Response.Code200(resul,false));

            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                return BadRequest(ADSUtilities.Response.Code400());
            }
        }
        // GET api/log/v1/5
        [HttpGet("{id}")]

        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                _logger.LogInformation("Busqueda por id");
                var resul = await _logRepository.GetLogAsync(id) ;
                if(resul !=null)
                    return Ok(ADSUtilities.Response.Code200(resul, false));

                return NotFound(ADSUtilities.Response.Code404("Busqueda sin coincidencias"));

            }
            catch (Exception e)
            {
                _logger.LogError("Busqueda por id Error :", e.StackTrace);
                return BadRequest(ADSUtilities.Response.Code400());
            }
        }
 
        // POST api/log/v1
        [HttpPost]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        public async Task<IActionResult> Post([FromBody] LogParam newLog)
        {
            try {
                _logger.LogInformation("Insert ");
                var a = new LogModel
                {
                    Application = newLog.Application,
                    Cuerpo = newLog.Cuerpo,
                    LogLevel = newLog.LogLevel,
                    TransactionId = newLog.TransactionId
                };

            _logRepository.AddLogAsync(a);

            return Ok(ADSUtilities.Response.Code200(null));

            }
            catch (Exception e)
            {
                _logger.LogError("Insert Error :" , e.StackTrace);
                return BadRequest(ADSUtilities.Response.Code400());
            }

        }

        // DELETE api/notes/23243423
        [HttpDelete("{id}")]

        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(IActionResult), 404)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                _logger.LogInformation("Eliminar");

                _logRepository.RemoveLog(id);
                return Ok(ADSUtilities.Response.Code200(null));

            }
            catch (Exception e)
            {
                _logger.LogError("Insert Error :", e.StackTrace);
                return BadRequest(ADSUtilities.Response.Code400());
            }
            
        }

    }
}
