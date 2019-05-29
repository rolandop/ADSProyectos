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
        [ProducesResponseType(typeof(IActionResult), 404)]
        public async Task<IActionResult> GetAsync(int page)
        {
            try {
                _logger.LogInformation("Inicio");
                var resul = await _logRepository.GetAllLogAsync(page);
                return Ok(ADSUtilities.Response.Code200(resul,false));
                
            } catch(Exception e)
            {
                _logger.LogError(e.StackTrace);
                return BadRequest(ADSUtilities.Response.Code400());
            }
        }
        // GET api/log/v1/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var resul = await _logRepository.GetLogAsync(id) ;
                if(resul !=null)
                    return Ok(ADSUtilities.Response.Code200(resul, false));
                return NotFound(ADSUtilities.Response.Code404("Busqueda sin coincidencias"));

            }
            catch (Exception e)
            {
                return BadRequest(ADSUtilities.Response.Code400());
            }
        }

      
        // POST api/log/v1
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LogParam newLog)
        {
            try {
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
                return BadRequest(ADSUtilities.Response.Code400());
            }

        }

        // DELETE api/notes/23243423
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _logRepository.RemoveLog(id);
        }

    }
}
