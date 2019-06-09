using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;

namespace ADSConfiguration.Utilities
{
    public class ADSConfigurationProvider: ConfigurationProvider
    {
        private readonly IConfigurationBuilder _builder;
        private readonly ADSConfigurationServiceOptions _options;
        private readonly string _environmentName;
        private string ServiceConfigurationUrl
        {
            get
            {
                var url = !_options.ServiceConfiguration.StartsWith("http")
                                            ? $"http://{_options.ServiceConfiguration}"
                                            : _options.ServiceConfiguration;

                return url;
            }
        }
        private string ServiceUrl
        {
            get
            {
                var url = !_options.Service.StartsWith("http")
                                                ? $"http://{_options.Service}"
                                                : _options.Service;

                return url;
            }
        }

        private string _configurationJson;

        public ADSConfigurationProvider(
            IConfigurationBuilder builder,
            ADSConfigurationServiceOptions options)
        {
            _environmentName = 
                    Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            _builder = builder;
            _options = options;
        }

        public override void Load()
        {
            var basePath =
                            (_builder.Properties["FileProvider"] as PhysicalFileProvider).Root;            

            var appsettings = $"appsettings.{_environmentName}.json";
            var appsettingsPath = Path.Combine(basePath, appsettings);
            if (!File.Exists(appsettingsPath))
            {
                appsettingsPath = Path.Combine(basePath, "appsettings.json");
            }

            if (!File.Exists(appsettingsPath))
            {
                Console.WriteLine($"Error al leer el archivo de configuración {appsettingsPath}");
                throw new Exception($"Error al leer el archivo de configuración {appsettingsPath}");
            }

            var appsettingsJson = File.ReadAllText(appsettingsPath);
            dynamic appsettingObj = JsonConvert.DeserializeObject(appsettingsJson);

            if (string.IsNullOrWhiteSpace(_options.ServiceId))
            {
                _options.ServiceId = appsettingObj["ServiceId"];
            }

            if (string.IsNullOrWhiteSpace(_options.Service))
            {
                _options.Service = appsettingObj["Service"];
            }
            if (string.IsNullOrWhiteSpace(_options.ServiceVersion))
            {
                _options.ServiceVersion = appsettingObj["ServiceVersion"];
            }

            if (string.IsNullOrWhiteSpace(_options.ServiceEnvironment))
            {
                _options.ServiceEnvironment = appsettingObj["ServiceEnvironment"];
            }

            if (string.IsNullOrWhiteSpace(_options.ServiceConfiguration))
            {
                _options.ServiceConfiguration =
                        GetValue(appsettingObj, "Global:Services:Configuration:Service");
            }

            SubscribeService();
        }

        private void SubscribeService()
        {
            Console.WriteLine($"Subscribir servicio a {ServiceConfigurationUrl}");

            var clienteRest = new RestClient(ServiceConfigurationUrl);
            var request = new RestRequest($"api/v1/configuration/subscribe", Method.POST);
            request.RequestFormat = DataFormat.Json;

            var parameters = new
            {
                ServiceId = _options.ServiceId,
                ServiceVersion = _options.ServiceVersion,
                Environment = _options.ServiceEnvironment,
                UpdateUrl = $"{ServiceUrl}/configuration/",
                VerifyUrl = $"{ServiceUrl}/configuration/ping"
            };

            Console.WriteLine("Parametros {0}", parameters);

            request.AddJsonBody(parameters);

            try
            {
                var response = clienteRest.Execute(request);

                Console.WriteLine("Subscribe Resultado {0} {1} {2}",
                                        _options.ServiceId,
                                        _options.ServiceEnvironment,
                                        _options.ServiceVersion);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    _configurationJson = response.Content;

                    LoadConfiguration();

                    Console.WriteLine("Servicio subscrito exitosamente!");
                }
                else
                {
                    Console.WriteLine("No se pudo subscribir el servicio");
                    Console.WriteLine("{@respuesta}", response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error no controlado al obtener configuración del servicio {0} {1} {2}",
                                        _options.ServiceId,
                                        _options.ServiceEnvironment,
                                        _options.ServiceVersion);
                Console.WriteLine(ex.Message);
            }
        }

        private void LoadConfiguration() {
            try
            {
                var data = ADSConfigurationParser.Parse(_configurationJson);

                foreach (var item in data)
                {
                    Data.Add(item);
                }

                //_builder.AddInMemoryCollection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar configuración {_configurationJson}");
                Console.WriteLine(ex.Message);
            }
        }

        public string GetValue(dynamic jsonObj, string key)
        {
            try
            {
                if (jsonObj != null)
                {
                    var partes = key.Split(":");
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
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
