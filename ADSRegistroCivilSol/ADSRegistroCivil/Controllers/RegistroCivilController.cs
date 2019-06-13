using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADSRegistroCivil.DAL;
using ADSRegistroCivil.DAL.Modelos;
using ADSRegistroCivil.Servicios;
using ADSUtilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ADSRegistroCivil.Controllers
{
    /// <summary>
    /// Controlador Principal MS Registro Civil
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/v1")]
    [ApiController]
    public class RegistroCivilController : ControllerBase
    {
        private readonly ILogger<RegistroCivilServicio> _logger;
        private readonly IRegistroCivilServicio _configuracionServicio;
        private readonly string TraceId;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="configuracionServicio"></param>
        public RegistroCivilController(ILogger<RegistroCivilServicio> logger,
                    IRegistroCivilServicio configuracionServicio
            )
        {
            _logger = logger;
            _configuracionServicio = configuracionServicio;
            TraceId = DateTime.Now.Ticks.ToString();
        }



        /// <summary>
        /// Servicio Rest Consulta a Servicio Registro Civil.
        /// </summary>
        /// <param name="identification"> Numero de Identificación a Consultar</param>
        /// /// <param name="consulta"> Opcion para indicar si se debe Forzar la Consulta a Registro Civil</param>
        /// /// <param name="op"> Opción para consultar el servicio Antiguo = 0 o Nuevo = 1 de Registro Civil</param>
        /// 
        /// <response code="200">Retorna cuando es exitóso</response>
        /// <response code="400">Retorna cuando es excepción</response>
        /// <response code="404">Retorna cuando es null la búsqueda</response>    
        [HttpGet]
        [Route("{identification}/{consulta}/{op}")]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        [ProducesResponseType(typeof(ResponseModel), 400)]
        [ProducesResponseType(typeof(ResponseModel), 404)]
        public async Task<IActionResult> GetAsync(string identification, string consulta, string op)
        {
            try
            {
                if(string.IsNullOrEmpty(identification) || string.IsNullOrEmpty(consulta) || string.IsNullOrEmpty(op))
                {
                    _logger.LogInformation("Invocación incorrecta faltan parametros de entrada");
                    return this.BadRequest();
                }

                _logger.LogInformation("Consulta Servicio Antiguo Registro Civil {@identification}", identification);

                if (op == "0")
                {
                    _logger.LogInformation("Consulta Servicio Antiguo Registro Civil {@identification}", identification);
                    var result = await _configuracionServicio.ConsultaRegistroCivilAsync(identification, consulta);
                    
                    if(result != null)
                    {
                        _logger.LogInformation("Graba log en Personal Log{@result}", result);
                        var save = await _configuracionServicio.GuardarLogAsync(result);

                        if(!save)
                        {
                            return this.ADSBadRequest();
                        }
                        return this.ADSOk(result);
                    }
                    else
                    {
                        _logger.LogInformation("Datos no encontrados Antiguo Registro Civil {@identification}", identification);
                        return this.NotFound();
                    }
                }
                else
                {
                    _logger.LogInformation("Consulta Servicio Nuevo Registro Civil {@identification}", identification);
                    var result = await _configuracionServicio.ConsultaCiudadanoAsync(identification, consulta);
                    if(result != null)
                    {
                        _logger.LogInformation("Graba log en Personal Log{@result}", result);
                        var save =  await _configuracionServicio.GuardarLogAsync(result);

                        if(!save)
                        {
                            _logger.LogInformation("Datos no encontrados");
                            return this.NotFound();
                        }

                        _logger.LogInformation("Datos correctos");
                        return this.ADSOk(result);
                    }
                    else
                    {
                        _logger.LogInformation("Datos no encontrados");
                        return this.NotFound();
                    }
                }
                
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error al Invocar Servicio Registro Civil");
                return this.ADSInternalError(e.Message);
            }
        }
    }
}