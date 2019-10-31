using System;
using ADSConsultaCliente.DAL;
using ADSConsultaCliente.DAL.Modelos;
using ADSConsultaCliente.Models;
using ADSUtilities.Logger;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace ADSConsultaCliente.Services
{
    /// <summary>
    /// Clase de servicios 
    /// </summary>
    public class ConsultaClienteService : IConsultaClienteService
    {
        private readonly ILogger<ConsultaClienteService> _logger;
        private readonly IMapper _mapper;
        private readonly IConsultaClienteRepositorio _consultaClienteRepositorio;
        private readonly ISiaerpRepositorio _siaerpRepositorio;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="consultaClienteRepositorio"></param>
        /// <param name="siaerpRepositorio"></param>
        /// <param name="configuration"></param>
        public ConsultaClienteService(ILogger<ConsultaClienteService> logger,
                                        IMapper mapper,
                                        IConsultaClienteRepositorio consultaClienteRepositorio,
                                        ISiaerpRepositorio siaerpRepositorio,
                                        IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _consultaClienteRepositorio = consultaClienteRepositorio;
            _siaerpRepositorio = siaerpRepositorio;
            _configuration = configuration;
        }

        /// <summary>
        /// Servicio consulta de persona 
        /// </summary>
        /// <param name="identificacion"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public PersonaModel ConsultaMasterDataAsync(string identificacion, bool rc)
        {
            try
            {
                _logger.LogInformation("Inicio llamada servicio ObtenerPersonaServicioAsync {@identificacion}", identificacion);
                var persona = ObtenerPersonaServicioAsync(identificacion);
                _logger.LogInformation("Fin llamada servicio ObtenerPersonaServicioAsync {@persona}", persona);
                if (persona == null)
                {
                    _logger.LogInformation("Inicio llamada servicio ObtenerPersonaUniversoServicioAsync {@identificacion}", identificacion);
                    persona = ObtenerPersonaUniversoServicioAsync(identificacion, persona ?? new PersonaModel());
                    _logger.LogInformation("Fin llamada servicio ObtenerPersonaUniversoServicioAsync {@persona}", persona);

                    var paramConsulta = rc ? "S" : "N";
                    persona = ConsultaRegistroCivilAsync(identificacion, paramConsulta, "1", new PersonaModel());
                    if (persona != null)
                    {
                        //registro en siaerp
                        //var usuario = _siaerpRepositorio.MapPersonaModelToUsuario(persona);
                        //var siaerp = _siaerpRepositorio.SaveSiaerpNuevoUsuario(usuario);
                        //fin de registro
                        _logger.LogInformation("Inicio llamada servicio ConsultaPlaAsync {@identificacion} {@nombre} {@app}", identificacion, persona.MTR_NOMBRE_COMPLETO, "MD");
                        var listaNegra = ConsultaPlaAsync(identificacion, persona.MTR_NOMBRE_COMPLETO, "MD");
                        _logger.LogInformation("Fin llamada servicio ConsultaPlaAsync {@listaNegra}", listaNegra);
                        persona.MTR_PLA_LISTA = listaNegra;
                    }
                    else
                    {
                        //_logger.LogInformation("Fin de llamada ConsultaMasterData");
                        return new PersonaModel();
                    }


                    /*

                    _logger.LogInformation("Inicio llamada servicio ConsultaDatabookAsync {@identificacion} {@persona}", identificacion, persona);
                    persona = ConsultaDatabookAsync(identificacion, persona ?? new PersonaModel());
                    _logger.LogInformation("Fin llamada servicio ConsultaDatabookAsync {@persona}", persona);
                    if (persona != null)
                    {
                        if (rc)
                        {
                            _logger.LogInformation("Inicio llamada servicio ConsultaRegistroCivilAsync {@identificacion} {@rc} {@opcion} {@persona}", identificacion, "S", 1, persona);
                            persona = ConsultaRegistroCivilAsync(identificacion, "S", "1", persona);
                            _logger.LogInformation("Fin llamada servicio ConsultaRegistroCivilAsync {@persona}", persona);
                        }

                        //actualiza modelo
                        _logger.LogInformation("Inicio llamada servicio ConsultaPlaAsync {@identificacion} {@nombre} {@app}", identificacion, persona.MTR_NOMBRE_COMPLETO,"MD");
                        var listaNegra = ConsultaPlaAsync(identificacion, persona.MTR_NOMBRE_COMPLETO, "MD");
                        _logger.LogInformation("Fin llamada servicio ConsultaPlaAsync {@listaNegra}", listaNegra);
                        persona.MTR_PLA_LISTA = listaNegra;
                    }
                    else
                    {
                        var paramConsulta = rc ? "S" : "N";
                        persona = ConsultaRegistroCivilAsync(identificacion, paramConsulta, "1", new PersonaModel());
                        if (persona != null)
                        {
                            //registro en siaerp
                            //var usuario = _siaerpRepositorio.MapPersonaModelToUsuario(persona);
                            //var siaerp = _siaerpRepositorio.SaveSiaerpNuevoUsuario(usuario);
                            //fin de registro
                            _logger.LogInformation("Inicio llamada servicio ConsultaPlaAsync {@identificacion} {@nombre} {@app}", identificacion, persona.MTR_NOMBRE_COMPLETO, "MD");
                            var listaNegra = ConsultaPlaAsync(identificacion, persona.MTR_NOMBRE_COMPLETO, "MD");
                            _logger.LogInformation("Fin llamada servicio ConsultaPlaAsync {@listaNegra}", listaNegra);
                            persona.MTR_PLA_LISTA = listaNegra;
                        }
                        else
                        {
                            //_logger.LogInformation("Fin de llamada ConsultaMasterData");
                            return new PersonaModel();
                        }
                    }*/


                }
                else
                {
                    if (rc)
                    {
                        persona = ConsultaRegistroCivilAsync(identificacion, "S", "1", persona);
                    }
                    var listaNegra = ConsultaPlaAsync(identificacion, persona.MTR_NOMBRE_COMPLETO, "Consulta Persona");
                    persona.MTR_PLA_LISTA = listaNegra;
                    _logger.LogInformation("Fin de llamada ConsultaMasterData");
                }
                return persona;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al llamar a ConsultaMasterDataAsync");                
                return null;
            }
        }

        /// <summary>
        /// Servicio para consultar la BD del MasterData Persona
        /// </summary>
        /// <param name="identificacion"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public PersonaModel ObtenerPersonaServicioAsync(string identificacion)
        {
            _logger.LogInformation("Inicio de llamada ObtenerPersonaServicioAsync");
            var result = _consultaClienteRepositorio.ObtenerPersonaServicioAsync(identificacion);
            return result;
        }

        /// <summary>
        /// Servicio para consultar la BD del MasterData Universo
        /// </summary>
        /// <param name="identificacion"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public PersonaModel ObtenerPersonaUniversoServicioAsync(string identificacion, PersonaModel model)
        {
            _logger.LogInformation("Inicio de llamada ObtenerPersonaServicioAsync");
            var result = _consultaClienteRepositorio.ObtenerPersonaUniversoServicioAsync(identificacion, model);
            return result;
        }

        /// <summary>
        ///  Llamada al servicio de consulta del Databook
        /// </summary>
        /// <param name="identificacion"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public PersonaModel ConsultaDatabookAsync(string identificacion, PersonaModel model)
        {
            return model;

            var url = _configuration
                                    .GetSection("Global:Services:Databook:Service").Value;

            _logger.LogInformation("Consultando databoock a {url}", url);
            
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest($"api/DataBook/v1/{identificacion}", 
                                        Method.GET);

            request.AddHeader("content-type", "application/json");
            request.AddHeader("TraceId", ADSUtilitiesLoggerEnvironment.TraceId);
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);

            if (response.StatusDescription == "OK")
            {
                string aux = response.Content;
                //aux = aux.Replace("\\", "").TrimStart('"').TrimEnd('"');
                var resultado = JsonConvert.DeserializeObject<DatabookResponseModel>(aux);
                if (resultado.Msg == "Ok")
                {
                    var data = resultado.Data;
                    model = _consultaClienteRepositorio.MapDatabookToModel(data, model);
                }
                return model;
            }
            return null;
        }

        /// <summary>
        /// Llamada al servicio de consulta Pla
        /// </summary>
        /// <param name="identificacion"></param>
        /// <param name="nombre"></param>
        /// <param name="app"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public string ConsultaPlaAsync(string identificacion, string nombre, string app)
        {
            string result = "";
            string url =_configuration.GetSection("Global:Services:Pla:Service").Value;

            var requestModel = new DatosClienteModel()
            {
                Identification = identificacion,
                Name = nombre,
                App = app
            };            
            var client = new RestClient(url);
            var request = new RestRequest($"api/ConsultaPla/v1/sisprev", Method.POST);
            request.RequestFormat = DataFormat.Json;
            var data = JsonConvert.SerializeObject(requestModel);
            request.AddParameter("application/json", data, ParameterType.RequestBody);
            var response = client.Execute(request);

            if (response.StatusDescription == "OK")
            {
                string aux = response.Content;
                var resultado = JsonConvert.DeserializeObject<PlaResponseModel>(aux);
                if (resultado.Msg == "Ok")
                {
                    result = resultado.Data.ListaNegra;
                    return result;
                }
            }
            return result;
        }

        /// <summary>
        /// Llamada al servicio de consulta del Registro Civil
        /// </summary>
        /// <param name="identificacion"></param>
        /// <param name="consulta"></param>
        /// <param name="op"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public PersonaModel ConsultaRegistroCivilAsync(string identificacion, string consulta, string op, PersonaModel model)
        {
            string url =
                _configuration.GetSection("Global:Services:RegistroCivil:Service").Value;

            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest($"api/RegistroCivil/v1/{identificacion}/{consulta}/{op}",                 
                Method.GET);

            request.AddHeader("content-type", "application/json");
            request.AddHeader("TraceId", ADSUtilitiesLoggerEnvironment.TraceId);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute(request);

            if (response.StatusDescription == "OK")
            {
                string aux = response.Content;
                aux = aux.Replace("\\", "").TrimStart('"').TrimEnd('"');
                var resultado = JsonConvert.DeserializeObject<RegistroCivilResponseModel>(aux);
                if (resultado.Msg == "Ok")
                {
                    var data = resultado.Data;
                    model = _consultaClienteRepositorio.MapRegitroCivilToModel(data, model);
                }
            }
            return model;
        }
    }
}
