using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADSRegistroCivil.DAL.Context;
using ADSRegistroCivil.Domain.Model;
using ADSRegistroCivil.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ADSRegistroCivil.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroCivilController : ControllerBase
    {

        private readonly ApplicationContext _siaerp;
        private readonly IConfiguration configuration;

        public RegistroCivilController(IConfiguration iConfig, ApplicationContext siaerp)
        {
            configuration = iConfig;
            this._siaerp = siaerp;
        }

        /// <response code="200">Retorna cuando es exitóso</response>
        /// <response code="400">Retorna cuando es excepción</response>
        /// <response code="404">Retorna cuando es null la búsqueda</response>    

        [HttpGet("{identification}/{consulta}/{op}")]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        [ProducesResponseType(typeof(ResponseModel), 400)]
        [ProducesResponseType(typeof(ResponseModel), 404)]

        public async Task<IActionResult> GetAsync(string identification, string consulta, string op)
        {
            try
            {
                var service = new RegistroCivilService(configuration, _siaerp);
                var log = new PersonaLogService();
                if(op  == "0")
                {
                    var response = await service.ConsultaRegistroCivilAsync(identification, consulta, log);
                    if (response != null)
                    {

                        return Ok(new ResponseModel
                        {
                            Error = 0,
                            Msg = "Ok",
                            Data = new SucessModel
                            {
                                Data = response
                            }
                        });
                    }
                    else
                    {
                        return NotFound(new ResponseModel
                        {
                            Error = 404,
                            Msg = "Datos no encontrados",
                            Data = null
                        });
                    }
                }
                else
                {
                    var response = await service.ConsultaCiudadanoAsync(identification, consulta, log);
                    if(response != null)
                    {
                        return Ok(new ResponseModel
                        {
                            Error = 0,
                            Msg = "Ok",
                            Data = new SucessModel
                            {
                                Data = response
                            }
                        });
                    }
                    else
                    {
                        return NotFound(new ResponseModel
                        {
                            Error = 404,
                            Msg = "Datos no encontrados",
                            Data = null
                        });
                    }
                }
                
                

            }
            catch (Exception)
            {
                return BadRequest(new ResponseModel
                {
                    Error = 400,
                    Msg = "Error en el Proceso",
                    Data = null
                });
            }
        }


    }
}
