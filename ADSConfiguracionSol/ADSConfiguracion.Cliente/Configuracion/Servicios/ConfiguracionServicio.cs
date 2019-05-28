using ADSConfiguracion.Cliente.Configuracion.Modelos;
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

namespace ADSConfiguracion.Cliente.Configuracion.Servicios
{
    public class ConfiguracionServicio : IConfiguracionServicio
    {
        private readonly ConfiguracionParamModelo _configuracion;
        private readonly ILogger<ConfiguracionServicio> _logger;
        private string _configuracionJson;
        private readonly IConfiguration _config;

        public ConfiguracionServicio(ILogger<ConfiguracionServicio> logger,
                            IOptions<ConfiguracionParamModelo> settings,
                            IConfiguration config)
        {            
            _logger = logger;
            _config = config;
            _configuracion = settings.Value;

            ObtenerConfiguracion();
        }

        public void ObtenerConfiguracion()
        {
            _logger.LogInformation("ObtenerConfiguracion de  {Configuracion} de {Url}"
                                        , _configuracion, _configuracion.ServiceConfiguracionUrl);

            var clienteRest = new RestClient(_configuracion.ServiceConfiguracionUrl);
            var solicitud =
                    new RestRequest($"api/v1/configuracion/{_configuracion.ServiceId}/{_configuracion.Environment}/{_configuracion.ServiceVersion}"
                                    , Method.GET);

            solicitud.RequestFormat = DataFormat.Json;

            try
            {
                clienteRest.ExecuteAsync(solicitud, respuesta =>
                {
                    if (respuesta.StatusCode == HttpStatusCode.OK)
                    {
                        _configuracionJson = respuesta.Content;
                        _logger.LogInformation("Obtener configuración del servicio {@Servicio}"
                                    , _configuracion);
                    }
                    else
                    {
                        _logger.LogWarning("No se pudo obtener configuración del servicio {@Servicio}"
                                    , _configuracion);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado al obtener configuración del servicio {@Servicio}", _configuracion);
            }
        }

        public void ActualizarConfiguracion(string configuracionJson)
        {
            var anterior = _configuracionJson;

            _configuracionJson = configuracionJson;

            _logger.LogInformation("Configuración Actualizada de: {ConfiguracionAnterior} a {ConfiguracionActual} "
                                    , anterior, _configuracionJson);
        }

        public void SubscribirServicio()
        {
            _logger.LogInformation("Subscribir servicio a {Url}"
                                        , _configuracion.ServiceConfiguracionUrl);

            var clienteRest = new RestClient(_configuracion.ServiceConfiguracionUrl);
            var solicitud = new RestRequest($"api/v1/configuracion/subscribe", Method.POST);
            solicitud.RequestFormat = DataFormat.Json;
            var parametros = new
            {
                ServicioId = _configuracion.ServiceId,
                ServicioVersion = _configuracion.ServiceVersion,
                Ambiente = _configuracion.Environment,
                UrlActualizacion = $"{_configuracion.ServiceUrl}/api/configuracion/",
                UrlVerificacion = $"{_configuracion.ServiceUrl}/api/configuracion/ping"
            };

            solicitud.AddJsonBody(parametros);

            try
            {
                clienteRest.ExecuteAsync(solicitud, respuesta =>
                {
                    if (respuesta.StatusCode == HttpStatusCode.OK)
                    {
                        _configuracionJson = respuesta.Content;                        
                        _logger.LogInformation("Obtener configuración del servicio {@Servicio}"
                                    , _configuracion);
                    }
                    else
                    {
                        _logger.LogWarning("No se pudo obtener configuración del servicio {@Servicio}"
                                    , _configuracion);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado al obtener configuración del servicio {@Servicio}", _configuracion);
            }
        }

        public string ObtenerConfiguracionJson()
        {
            return _configuracionJson;
        }

        public string ObtenerValor(string clave)
        {
            try
            {
                _logger.LogInformation("ObtenerValor");
                dynamic jsonObj = JsonConvert.DeserializeObject(_configuracionJson);

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
                return "";
            }
        }
    }
}
