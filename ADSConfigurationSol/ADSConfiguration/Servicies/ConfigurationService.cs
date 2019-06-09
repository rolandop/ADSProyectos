using ADSConfiguration.DAL;
using ADSConfiguration.DAL.Entities;
using ADSConfiguration.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ADSConfiguration.Servicios
{
    public class ConfigurationService: IConfigurationService
    {
        private readonly ILogger<ConfigurationService> _logger;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IServiceRepository _serviceRepository;

        private Dictionary<string, object> ConfigModel;

        public ConfigurationService(ILogger<ConfigurationService> logger,
                                        IConfigurationRepository configurationRepository,
                                        IServiceRepository serviceRepository)
        {
            _logger = logger;
            _configurationRepository = configurationRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<object> GetConfigurationService(string serviceId, string environment, string version)
        {
            var configs = await _configurationRepository
                                    .GetConfigurationsAsync(serviceId, environment, version);
          
            if (!configs.Any())
            {
                return null;
            }

            var globales = await _configurationRepository
                                  .GetGlobalConfigurationsAsync(environment);                       

            ConfigModel = new Dictionary<string, object>();

            ConfigModel.Add("ServiceId", serviceId);
            ConfigModel.Add("Environment", environment);
            ConfigModel.Add("Version", version);
            ConfigModel.Add("RequestDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            AddSection(globales, "Global");
            AddSection(configs, serviceId);

            _logger.LogInformation("ObtenerConfiguracionServicio {@Configuracion}", ConfigModel);

            return ConfigModel;
        }

        private void AddSection(ICollection<Configuration> configurations,
                                       string mainSection)
        {
            Dictionary<string, object> serviceSection = null;

            if (!ConfigModel.ContainsKey(mainSection))
            {
                ConfigModel.Add(mainSection, new Dictionary<string, object>());
            }
            serviceSection = ConfigModel[mainSection] as Dictionary<string, object>;

            if (configurations.Any())
            {
                foreach (var seccionGrupo in configurations
                                       .GroupBy(config => config.Section))
                {
                    Dictionary<string, object> currentSection = serviceSection;

                    if (!string.IsNullOrWhiteSpace(seccionGrupo.Key))
                    {
                        var seccion = string.IsNullOrWhiteSpace(seccionGrupo.Key)
                                        ? mainSection : seccionGrupo.Key;

                        var subSections = seccion.Split('/');

                        foreach (var subSection in subSections)
                        {
                            if (!currentSection.ContainsKey(subSection))
                            {
                                currentSection.Add(subSection, new Dictionary<string, object>());
                            }
                            currentSection = currentSection[subSection] as Dictionary<string, object>;
                        }
                    }                    

                    foreach (var config in seccionGrupo
                                        .OrderBy(config => config.Key)
                                        .ToList())
                    {
                        if (!currentSection.ContainsKey(config.Key))
                        {
                            currentSection.Add(config.Key, config.Value);
                        }
                    }                                        
                }
            }           
        }
        
        public async Task Subscribe(SubscribeModel subscribe)
        {
            var servicio = await _serviceRepository.GetServiceAsync(subscribe.ServiceId,
                                                            subscribe.Environment,
                                                            subscribe.ServiceVersion);
            if (servicio == null)
            {
                servicio = new Service
                {
                    ServiceId = subscribe.ServiceId,
                    Environment = subscribe.Environment,
                    ServiceVersion = subscribe.ServiceVersion,
                    UpdateUrl = subscribe.UpdateUrl,
                    VerifyUrl = subscribe.VerifyUrl
                };

                await _serviceRepository.AddServiceAsync(servicio);
            }
            else
            {
                await _serviceRepository
                            .UpdateService(servicio.Id,
                                                subscribe.UpdateUrl,
                                                subscribe.VerifyUrl);
            }
        }

        public async Task Clonar(CloneModel clonar)
        {
            var configs = await _configurationRepository
                                        .GetConfigurationsAsync(clonar.ServiceId, 
                                                        clonar.Environment, 
                                                        clonar.ServiceVersion);

            foreach (var config in configs)
            {
                //Busca si ya existe la version nueva del servicio
                var configAux = await _configurationRepository
                                            .GetConfigurationAsync(clonar.NewServiceId, 
                                                                        clonar.NewEnvironment, 
                                                                        clonar.NewVersion,
                                                                        config.Section,
                                                                        config.Key);
                if (configAux == null)
                {
                    config.ServiceId = clonar.NewServiceId;
                    config.ServiceVersion = clonar.NewVersion;
                    config.Environment = clonar.NewEnvironment;

                    await _configurationRepository
                                .AddConfigurationAsync(config);
                }
            }

        }

        public async Task Notify(string serviceId, string ambiente, string version)
        {
            var servicio = await _serviceRepository
                                            .GetServiceAsync(serviceId, ambiente, version);

            if (servicio == null)
            {
                _logger.LogWarning("Notificar NotFound");
            }

            var updateUrl = servicio.UpdateUrl;

            if (string.IsNullOrWhiteSpace(updateUrl))
            {
                updateUrl = $"http://{serviceId}/configuration/notify";
                _logger.LogWarning("Url para notificaciones construida por defecto");
            }

            _logger.LogInformation("Notificando Servicio {Servicio} {Ambiente} {Version} a {UrlActualizacion}",
                                    serviceId, ambiente, version, servicio.UpdateUrl);

            var uri = new Uri(servicio.UpdateUrl);
            var origin = uri.GetLeftPart(UriPartial.Authority);
            var clienteRest = new RestClient(origin);
            var solicitud = new RestRequest(uri.PathAndQuery, Method.POST);

            var configs = await
                                GetConfigurationService(serviceId, ambiente, version);

            solicitud.AddJsonBody(new
            {
                configurationJson = JsonConvert.SerializeObject(configs)
            });

            try
            {
                clienteRest.ExecuteAsync(solicitud, respuesta =>
                {
                    if (respuesta.StatusCode == HttpStatusCode.OK)
                    {
                        _logger.LogInformation("Notificar OK Servicio {Servicio} {Ambiente} {Version} ", 
                                    serviceId, ambiente, version);
                    }
                    else
                    {
                        _logger.LogError("No se pudo notificar al Servicio {Servicio} {Ambiente} {Version}, Mensaje {Mensaje}",
                                    serviceId, ambiente, version, respuesta.Content);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No se pudo enviar la configuración al servicio ", serviceId);
            }
        }

        public Task<bool> RemoveAllConfigutarionsAsync()
        {
            return _configurationRepository.RemoveAllConfigurationsAsync();
        }

        public Task AddConfigurationAsync(Configuration elemento)
        {
            return _configurationRepository.AddConfigurationAsync(elemento);
        }

        public async Task<bool> Save(ICollection<ConfigurationModel> configurations)
        {
            
            foreach (var configuration in configurations)
            {
                var configAux = await _configurationRepository.GetConfigurationAsync(
                                                    configuration.ServiceId,
                                                    configuration.Environment,
                                                    configuration.ServiceVersion,
                                                    configuration.Section,
                                                    configuration.Key
                                                );
                if (configAux == null)
                {
                    await _configurationRepository
                                                .AddConfigurationAsync(new Configuration {
                                                    ServiceId = configuration.ServiceId,
                                                    Environment = configuration.Environment,
                                                    ServiceVersion = configuration.ServiceVersion,
                                                    Section = configuration.Section,
                                                    Key = configuration.Key,
                                                    Value = configuration.Value,
                                                    Description = configuration.Descripction
                                                });
                }
                else
                {
                    await _configurationRepository
                                                .UpdateConfiguration(
                                                    configAux.Id.ToString(),
                                                    configuration.Section, 
                                                    configuration.Key,
                                                    configuration.Value, 
                                                    configuration.Descripction
                                                );
                }
            }

            var servciosNotificar = configurations
                                        .GroupBy(conf => new
                                        {
                                            conf.ServiceId,
                                            conf.Environment,
                                            conf.ServiceVersion
                                        });

            foreach (var servicio in servciosNotificar)
            {
                if (servicio.Key.ServiceVersion == "Global")
                {
                    var servicios = await 
                            _serviceRepository.GetServicesAsync(true);
                    foreach (var servicio2 in servicios)
                    {
                        _ = Notify(servicio2.ServiceId, 
                                        servicio2.Environment, 
                                            servicio2.ServiceVersion);
                    }
                    break;
                }
                else
                {
                    _ = Notify(servicio.Key.ServiceId, 
                                    servicio.Key.Environment, 
                                        servicio.Key.ServiceVersion);
                }
            }

            return true;
        }
    }

    public class KeyValueModel {

        public KeyValueModel()
        {
            
        }
        public KeyValueModel(string key)
        {
            Key = key;
        }
        public KeyValueModel(string key, object value)
        {
            Key = key;
            Value = value;
        }
        public string Key { get; set; }
        public object Value { get; set; }
    }
    
}
