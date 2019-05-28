using ADSConfiguracion.DAL;
using ADSConfiguracion.DAL.Entidades;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ADSConfiguracion
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

        public IEnumerable<Servicio> ObtenerServicios(bool estado)
        {
            using (var servicioRepositorio = _scope.ServiceProvider.GetRequiredService<IServicioRepositorio>())
            {
                return servicioRepositorio.ObtenerServiciosAsync(estado)
                                                    .GetAwaiter()
                                                    .GetResult();
            }
        }

        public bool DesactivarServicio(Servicio servicio)
        {
            using (var servicioRepositorio = _scope.ServiceProvider.GetRequiredService<IServicioRepositorio>())
            {
                return servicioRepositorio.DesactivarServicioAsync(servicio)
                                           .GetAwaiter()
                                           .GetResult();

            }
        }

        public bool ActualizarIntentos(Servicio servicio, int intentos)
        {
            using (var servicioRepositorio = _scope.ServiceProvider.GetRequiredService<IServicioRepositorio>())
            {
                return servicioRepositorio.ActualizarIntentosAsync(servicio, intentos)
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
                            _logger.LogInformation("Validando servicio {ServicioId} {ServicioNombre} {ServicioVersion} en {Url}",
                                servicio.Id, servicio.Ambiente, servicio.ServicioVersion, servicio.UrlVerificacion);

                            var uri = new Uri(servicio.UrlVerificacion);
                            var origin = uri.GetLeftPart(UriPartial.Authority);
                            var clienteRest = new RestClient(origin);
                            var solicitud = new RestRequest(uri.PathAndQuery, Method.GET);

                            var respuesta = clienteRest.Execute(solicitud);

                            if (respuesta.StatusCode != System.Net.HttpStatusCode.OK
                                && servicio.Intentos > 10)
                            {

                                var actualizo = DesactivarServicio(servicio);
                                if (!actualizo)
                                {
                                    _logger.LogError("No se pudo desabilitar el servicio");
                                }
                                else
                                {
                                    _logger.LogInformation("Servicio desactivado");
                                }

                                _logger.LogInformation("Actualiza intentos a {Intentos}", servicio.Intentos + 1);
                                ActualizarIntentos(servicio, servicio.Intentos + 1);
                            }
                            else
                            {
                                _logger.LogInformation("Reinicia intentos a 0");
                                ActualizarIntentos(servicio, 0);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error al realizar ping al servicio {url}", servicio.UrlVerificacion);
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