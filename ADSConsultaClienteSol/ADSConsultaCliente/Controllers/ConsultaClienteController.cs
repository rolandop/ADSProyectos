using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADSConsultaCliente.DAL;
using ADSConsultaCliente.DAL.Modelos;
using ADSConsultaCliente.Models;
using ADSConsultaCliente.Services;
using ADSUtilities;
using ADSUtilities.Logger;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ADSConsultaCliente.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/v1")]
    [ApiController]
    public class ConsultaClienteController : ControllerBase
    {
        private readonly ILogger<ConsultaClienteService> _logger;
        private readonly IConsultaClienteService _consultaClienteService;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="consultaClienteService"></param>
        public ConsultaClienteController(ILogger<ConsultaClienteService> logger,
            IMapper mapper,
            IConsultaClienteService consultaClienteService)
        {
            _logger = logger;
            _mapper = mapper;
            _consultaClienteService = consultaClienteService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Ping")]
        [ProducesResponseType(200)]
        public ActionResult Ping()
        {
            _logger.LogInformation("Ping from {@Url}", Request.PathBase);

            return Ok();
        }

        /// <response code="200">Retorna cuando es exitóso</response>
        //[HttpPost]
        //[Route("")]
        //[ProducesResponseType(200)]
        //public ActionResult Actualizar([FromBody]ConfiguracionJsonModelo configuracion)
        //{
        //    _logger.LogInformation("Actualizar configuración {@Configuracion}", configuracion);
        //    _configuracionRepositorio.ActualizarConfiguracion(configuracion.ConfiguracionJson);

        //    return Ok(_configuracionRepositorio.ObtenerConfiguracionJson());
        //}

        // GET persona/{identificacion}
        /// <summary>
        /// Función que consulta el MasterData de clientes.
        /// </summary>
        /// <param name="identificacion">Identificación a consultar</param>
        /// <response code="200">Retorna cuando es exitoso</response>
        /// <response code="400">Retorna cuando es excepción</response>
        /// <response code="404">Retorna cuando es null la búsqueda</response>    

        [HttpGet("persona/{identificacion}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        [ProducesResponseType(typeof(IActionResult), 404)]
        public IActionResult ConsultaCliente(string identificacion)
        {
            try
            {
                var persona = _consultaClienteService.ObtenerPersonaServicioAsync(identificacion);
                if (persona != null)
                {
                    return this.ADSOk(persona);
                }
                else
                {
                    return this.ADSNotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return this.ADSBadRequest();
            }
        }

        // GET universo/{identificacion}
        /// <summary>
        /// Función que consulta el MasterData universal.
        /// </summary>
        /// <param name="identificacion">Identificación a consultar</param>
        /// <response code="200">Retorna cuando es exitoso</response>
        /// <response code="400">Retorna cuando es excepción</response>
        /// <response code="404">Retorna cuando es null la búsqueda</response>  
        [HttpGet("universo/{identificacion}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        [ProducesResponseType(typeof(IActionResult), 404)]
        public IActionResult ConsultaClienteUniverso(string identificacion)
        {
            try
            {
                var persona = _consultaClienteService.ObtenerPersonaUniversoServicioAsync(identificacion, new PersonaModel());
                if (persona != null)
                {
                    return this.ADSOk(persona);
                }
                else
                {
                    return this.ADSNotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return this.ADSBadRequest();
            }
        }

        // GET consultaPersona/{identificacion}/{rc}
        /// <summary>
        /// Función para consultar los datos de una persona.
        /// </summary>
        /// <param name="identificacion">Identificación a consultar</param>
        /// <param name="rc">Campo para indicar si es obligatorio consultar Regitro Civil</param>
        /// <response code="200">Retorna cuando es exitoso</response>
        /// <response code="400">Retorna cuando es excepción</response>
        /// <response code="404">Retorna cuando es null la búsqueda</response>  
        [HttpGet("consultaPersona/{identificacion}/{rc}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        [ProducesResponseType(typeof(IActionResult), 404)]
        public IActionResult ConsultaMasterData(string identificacion, bool rc)
        {
            try
            {
                _logger.LogInformation("Inicio Consulta MasterData {@identificacion} {@rc}", identificacion, rc);
                this.TraceId("ConsultaMasterData_" + DateTime.Now.Ticks);
                var persona = _consultaClienteService.ConsultaMasterDataAsync(identificacion, rc);
                if (persona != null)
                {
                    _logger.LogInformation("Respuesta Consulta MasterData {@persona}", persona);
                    return this.ADSOk(persona);
                }
                else
                {
                    _logger.LogInformation("Respuesta Consulta MasterData {@persona}", persona);
                    return this.ADSNotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Respuesta Consulta MasterData {@BadRequest}", ex.StackTrace);
                _logger.LogError(ex.StackTrace);
                return this.ADSBadRequest();
            }
        }
    }
}
