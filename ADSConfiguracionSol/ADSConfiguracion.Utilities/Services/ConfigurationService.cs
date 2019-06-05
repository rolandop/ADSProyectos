
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using ADSConfiguracion.Utilities.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ADSConfiguracion.Utilities.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private ConfigurationParamModel _configuracion {
            get { return ADSConfigurationServiceOptions.Parameters; }
        }
        private readonly ILogger<ConfigurationService> _logger;        
        private readonly IConfiguration _config;
        private string _configuracionJson;

        public string ServiceConfigurationUrl {
            get {
                var url = !_configuracion.ServiceConfiguration.StartsWith("http")
                                                ? $"http://{_configuracion.ServiceConfiguration}"
                                                : _configuracion.ServiceConfiguration;

                return url;
            }
        }

        public string ServiceUrl
        {
            get
            {
                var url = !_configuracion.Service.StartsWith("http")
                                                ? $"http://{_configuracion.Service}"
                                                : _configuracion.Service;

                return url;
            }
        }

        public ConfigurationService(ILogger<ConfigurationService> logger,
                            IConfiguration config)
        {            
            _logger = logger;
            _config = config;

            GetConfiguracion();
        }

        public void GetConfiguracion()
        {   

            _logger.LogInformation("ObtenerConfiguracion de  {$Configuracion} de {Url}",
                                        _configuracion,
                                        ServiceConfigurationUrl);

            _logger.LogInformation("Configuracion {ServicioId} {Environment} {Version}",
                                        _configuracion.ServiceId,
                                        _configuracion.ServiceEnvironment,
                                        _configuracion.ServiceVersion);


            var clienteRest = new RestClient(ServiceConfigurationUrl);
            var solicitud =
                    new RestRequest($"api/v1/configuration/{_configuracion.ServiceId}/{_configuracion.ServiceEnvironment}/{_configuracion.ServiceVersion}"
                                    , Method.GET);

            solicitud.RequestFormat = DataFormat.Json;

            try
            {
                clienteRest.ExecuteAsync(solicitud, respuesta =>
                {
                    if (respuesta.StatusCode == HttpStatusCode.OK)
                    {
                        _configuracionJson = respuesta.Content;
                        _logger.LogInformation("Configuración obtenida exitosamente!");
                    }
                    else
                    {
                        _logger.LogWarning("No se pudo obtener configuración del servicio");

                        _logger.LogError("{@respuesta}", respuesta);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado al obtener configuración del servicio {@Servicio}", _configuracion);
            }
        }

        public void UpdateConfiguration(string configuracionJson)
        {
            var anterior = _configuracionJson;

            _configuracionJson = configuracionJson;

            _logger.LogInformation("Configuración Actualizada de: {ConfiguracionAnterior} a {ConfiguracionActual} "
                                    , anterior, _configuracionJson);
        }

        public void SubscribeService()
        {
            _logger.LogInformation("Subscribir servicio a {Url}"
                                        , ServiceConfigurationUrl);

            var clienteRest = new RestClient(ServiceConfigurationUrl);
            var solicitud = new RestRequest($"api/v1/configuration/subscribe", Method.POST);
            solicitud.RequestFormat = DataFormat.Json;

            var parametros = new
            {
                ServiceId = _configuracion.ServiceId,
                ServiceVersion = _configuracion.ServiceVersion,
                Environment = _configuracion.ServiceEnvironment,
                UpdateUrl = $"{ServiceUrl}/configuration/",
                VerifyUrl = $"{ServiceUrl}/configuration/ping"
            };

            _logger.LogInformation("Parametros {@Parametros}", parametros);

            solicitud.AddJsonBody(parametros);

            try
            {
                clienteRest.ExecuteAsync(solicitud, respuesta =>
                {
                    _logger.LogInformation("Subscribe Resultado {ServicioId} {Environment} {Version}",
                                        _configuracion.ServiceId,
                                        _configuracion.ServiceEnvironment,
                                        _configuracion.ServiceVersion);

                    if (respuesta.StatusCode == HttpStatusCode.OK)
                    {
                        _configuracionJson = respuesta.Content;        
                        
                        _logger.LogInformation("Servicio subscrito exitosamente!");
                    }
                    else
                    {
                        _logger.LogWarning("No se pudo subscribir el servicio");
                        _logger.LogError("{@respuesta}", respuesta);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado al obtener configuración del servicio {@Servicio}", _configuracion);
            }
        }

        public string GetConfigurationJson()
        {
            return _configuracionJson;
        }

        public string GetValue(string clave)
        {
            try
            {
                _logger.LogInformation("ObtenerValor");
                dynamic jsonObj = null;

                try
                {
                    jsonObj = JsonConvert.DeserializeObject(_configuracionJson);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Al Deserializar {@_configuracionJson}", 
                            _configuracionJson);
                }                

                if (jsonObj != null)
                {
                    var partes = clave.Split(":");
                    var objetoEvaluar = jsonObj;

                    foreach (var parte in partes)
                    {
                        if (objetoEvaluar != null)
                        {
                            objetoEvaluar = objetoEvaluar[parte];
                        }
                        else
                        {                            
                            break;
                        }
                    }
                    if (objetoEvaluar != null)
                    {
                        return objetoEvaluar.ToString();
                    }                               
                }
                return _config[clave];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ObtenerValor");
                return _config[clave];
            }
        }
    }
}
