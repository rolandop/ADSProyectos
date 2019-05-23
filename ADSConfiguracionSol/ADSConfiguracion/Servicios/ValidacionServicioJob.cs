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

                        _logger.LogInformation("Validando servicio ", servicio.Id);
                        var uri = new Uri(servicio.UrlVerificacion);
                        var origin = uri.GetLeftPart(UriPartial.Authority);
                        var clienteRest = new RestClient(origin);
                        var solicitud = new RestRequest(uri.PathAndQuery, Method.GET);

                        var respuesta = clienteRest.Execute(solicitud);

                        if (respuesta.StatusCode != System.Net.HttpStatusCode.OK
                            && servicio.Intentos > 10) {

                            var actualizo = DesactivarServicio(servicio);
                            if (!actualizo)
                            {
                                _logger.LogError("No se pudo desabilitar el servicio ", servicio.Id);
                            }
                            else
                            {
                                _logger.LogInformation("Servicio desactivado ", servicio.Id);
                            }
                            ActualizarIntentos(servicio, servicio.Intentos++);
                        }
                        else
                        {
                            ActualizarIntentos(servicio, 0);
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