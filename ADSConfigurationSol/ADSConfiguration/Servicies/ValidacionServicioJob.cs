using ADSConfiguration.DAL;
using ADSConfiguration.DAL.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ADSConfiguration
{
    [DisallowConcurrentExecution]
    public class ValidacionServicioJob: IJob, IDisposable
    {
        private readonly IServiceScope _scope;
        private readonly ILogger<ValidacionServicioJob> _logger;
        private readonly IApplicationLifetime _applicationLifetime;        

        private static object lockHandle = new object();
        private static bool shouldExit = false;

        public ValidacionServicioJob(ILogger<ValidacionServicioJob> logger, 
            IApplicationLifetime applicationLifetime,
            IServiceProvider services)
        {
            _logger = logger;
            _applicationLifetime = applicationLifetime;
            _scope = services.CreateScope();
        }

        public IEnumerable<Service> ObtenerServicios(bool estado)
        {
            using (var servicioRepositorio = _scope.ServiceProvider.GetRequiredService<IServiceRepository>())
            {
                return servicioRepositorio.GetServicesAsync(estado)
                                                    .GetAwaiter()
                                                    .GetResult();
            }
        }

        public bool UnsubscribeService(Service service)
        {
            using (var serviceRepository = _scope.ServiceProvider
                                            .GetRequiredService<IServiceRepository>())
            {
                return serviceRepository.UnsubscribeServiceAsync(service)
                                           .GetAwaiter()
                                           .GetResult();

            }
        }

        public bool UpdateAttempts(Service service, int attempts)
        {
            using (var serviceRepository = _scope.ServiceProvider
                                .GetRequiredService<IServiceRepository>())
            {
                return serviceRepository.UpdateAttemptsAsync(service, attempts)
                                           .GetAwaiter()
                                           .GetResult();

            }
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
                _applicationLifetime.ApplicationStopping.Register(() =>
                {
                    lock (lockHandle)
                    {
                        shouldExit = true;
                    }
                });

                try
                {
                    _logger.LogInformation("Obteniendo Servicios..");

                    var servicios = ObtenerServicios(true);

                    foreach (var servicio in servicios)
                    {
                        lock (lockHandle)
                        {
                            if (shouldExit)
                            {
                                _logger.LogDebug($"TestJob detected that application is shutting down - exiting");
                                break;
                            }
                        }

                        try
                        {
                            var verifyUrl = servicio.VerifyUrl;

                            if (string.IsNullOrWhiteSpace(verifyUrl))
                            {
                                verifyUrl = $"http://{servicio.ServiceId}/configuration/ping";
                                _logger.LogWarning("Url para notificaciones construida por defecto");
                            }

                            _logger.LogInformation("Validando servicio {ServicioId} {ServicioNombre} {ServicioVersion} en {Url}",
                                servicio.ServiceId, servicio.Environment, servicio.ServiceVersion, verifyUrl);                                                       

                            var uri = new Uri(verifyUrl);
                            var origin = uri.GetLeftPart(UriPartial.Authority);
                            var clienteRest = new RestClient(origin);
                            var solicitud = new RestRequest(uri.PathAndQuery, Method.GET);

                            var respuesta = clienteRest.Execute(solicitud);

                            if (respuesta.StatusCode != System.Net.HttpStatusCode.OK)
                            {
                                if (servicio.Attempts >= 10)
                                {
                                    var actualizo = UnsubscribeService(servicio);
                                    if (!actualizo)
                                    {
                                        _logger.LogError("No se pudo desabilitar el servicio");
                                    }
                                    else
                                    {
                                        _logger.LogInformation("Servicio desactivado");
                                    }                                    
                                }
                                else
                                {
                                    var attemtp = servicio.Attempts + 1;

                                    _logger.LogError("No se pudo contactar al servicio, número de intentos {@attemtp}",
                                        attemtp);

                                    _logger.LogInformation("Actualiza intentos a {Intentos}", attemtp);

                                    UpdateAttempts(servicio, attemtp);
                                    
                                }
                                
                            }
                            else
                            {
                                _logger.LogInformation("Reinicia intentos a 0");
                                UpdateAttempts(servicio, 0);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error al realizar ping al servicio {url}", servicio.VerifyUrl);
                        }

                        
                    }
                }
                catch (Exception exc)
                {
                    _logger.LogError(exc, "An error occurred during execution of scheduled job");
                }
            });
        }

        public void Dispose()
        {
            _scope?.Dispose();
        }
    }
}