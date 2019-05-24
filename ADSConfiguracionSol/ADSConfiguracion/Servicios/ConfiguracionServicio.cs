using ADSConfiguracion.DAL;
using ADSConfiguracion.DAL.Entidades;
using ADSConfiguracion.Modelos;
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

namespace ADSConfiguracion.Servicios
{
    public class ConfiguracionServicio: IConfiguracionServicio
    {
        private readonly ILogger<ConfiguracionServicio> _logger;
        private readonly IConfiguracionRepositorio _configuracionRepositorio;
        private readonly IServicioRepositorio _servicioRepositorio;

        private Dictionary<string, object> ConfigModelo;

        public ConfiguracionServicio(ILogger<ConfiguracionServicio> logger,
                                        IConfiguracionRepositorio configuracionRepositorio,
                                        IServicioRepositorio servicioRepositorio)
        {
            _logger = logger;
            _configuracionRepositorio = configuracionRepositorio;
            _servicioRepositorio = servicioRepositorio;
        }

        public async Task<object> ObtenerConfiguracionServicio(string id, string ambiente, string version)
        {
            var configs = await _configuracionRepositorio
                                    .ObtenerConfiguracionesAsync(id, ambiente, version);
          
            if (!configs.Any())
            {
                return null;
            }

            var globales = await _configuracionRepositorio
                                  .ObtenerConfiguracionesGlobalesAsync(ambiente);                       

            ConfigModelo = new Dictionary<string, object>();

            ConfigModelo.Add("ServiceId", id);
            ConfigModelo.Add("Environment", ambiente);
            ConfigModelo.Add("Version", version);
            ConfigModelo.Add("RequestDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            CrearSeccion(globales, "Global");
            CrearSeccion(configs, id);

            _logger.LogInformation("ObtenerConfiguracionServicio", ConfigModelo);

            return ConfigModelo;
        }

        private void CrearSeccion(ICollection<Configuracion> configuraciones,
                                       string seccionPrincipal)
        {
            Dictionary<string, object> seccionServicio = null;

            if (!ConfigModelo.ContainsKey(seccionPrincipal))
            {
                ConfigModelo.Add(seccionPrincipal, new Dictionary<string, object>());
            }
            seccionServicio = ConfigModelo[seccionPrincipal] as Dictionary<string, object>;

            if (configuraciones.Any())
            {
                foreach (var seccionGrupo in configuraciones
                                       .GroupBy(config => config.Seccion))
                {
                    Dictionary<string, object> seccionActual = seccionServicio;

                    if (!string.IsNullOrWhiteSpace(seccionGrupo.Key))
                    {
                        var seccion = string.IsNullOrWhiteSpace(seccionGrupo.Key)
                                        ? seccionPrincipal : seccionGrupo.Key;

                        var subSecciones = seccion.Split('/');

                        foreach (var subSecccion in subSecciones)
                        {
                            if (!seccionActual.ContainsKey(subSecccion))
                            {
                                seccionActual.Add(subSecccion, new Dictionary<string, object>());
                            }
                            seccionActual = seccionActual[subSecccion] as Dictionary<string, object>;
                        }
                    }                    

                    foreach (var config in seccionGrupo
                                        .OrderBy(config => config.Clave)
                                        .ToList())
                    {
                        if (!seccionActual.ContainsKey(config.Clave))
                        {
                            seccionActual.Add(config.Clave, config.Valor);
                        }
                    }                                        
                }
            }           
        }
        
        public async Task Subscribe(SubscribeModelo subscribe)
        {
            var servicio = await _servicioRepositorio.ObtenerServicioAsync(subscribe.ServicioId,
                                                            subscribe.Ambiente,
                                                            subscribe.ServicioVersion);
            if (servicio == null)
            {
                servicio = new Servicio
                {
                    ServicioId = subscribe.ServicioId,
                    Ambiente = subscribe.Ambiente,
                    ServicioVersion = subscribe.ServicioVersion,
                    UrlActualizacion = subscribe.UrlActualizacion,
                    UrlVerificacion = subscribe.UrlVerificacion
                };

                await _servicioRepositorio.AgregarServicioAsync(servicio);
            }
            else
            {
                if (servicio.UrlActualizacion != subscribe.UrlActualizacion
                    || servicio.UrlVerificacion != subscribe.UrlVerificacion)
                {
                    await _servicioRepositorio
                            .ActualizarServicio(servicio.Id,
                                                subscribe.UrlActualizacion,
                                                subscribe.UrlVerificacion);
                }
            }
        }

        public async Task Clonar(ClonarModelo clonar)
        {
            var configs = await _configuracionRepositorio
                                        .ObtenerConfiguracionesAsync(clonar.ServicioId, 
                                                        clonar.Ambiente, 
                                                        clonar.ServicioVersion);

            foreach (var config in configs)
            {
                //Busca si ya existe la version nueva del servicio
                var configAux = await _configuracionRepositorio
                                            .ObtenerConfiguracionAsync(clonar.NuevoServicioId, 
                                                                        clonar.NuevoAmbiente, 
                                                                        clonar.NuevaVersion,
                                                                        config.Seccion,
                                                                        config.Clave);
                if (configAux == null)
                {
                    config.ServicioId = clonar.NuevoServicioId;
                    config.ServicioVersion = clonar.NuevaVersion;
                    config.Ambiente = clonar.NuevoAmbiente;

                    await _configuracionRepositorio
                                .AgregarConfiguracionAsync(config);
                }
            }

        }

        public async Task Notificar(string id, string ambiente, string version)
        {
            var servicio = await _servicioRepositorio
                                            .ObtenerServicioAsync(id, ambiente, version);

            if (servicio == null)
            {
                _logger.LogInformation("Notificar NotFound");
            }

            var uri = new Uri(servicio.UrlActualizacion);
            var origin = uri.GetLeftPart(UriPartial.Authority);
            var clienteRest = new RestClient(origin);
            var solicitud = new RestRequest(uri.PathAndQuery, Method.POST);

            var configs = await
                                ObtenerConfiguracionServicio(id, ambiente, version);

            solicitud.AddJsonBody(new
            {
                configuracionJson = JsonConvert.SerializeObject(configs)
            });

            try
            {
                clienteRest.ExecuteAsync(solicitud, respuesta =>
                {
                    if (respuesta.StatusCode == HttpStatusCode.OK)
                    {
                        _logger.LogInformation("Notificar OK Servicio {Servicio} {Ambiente} {Version} ", 
                                    id, ambiente, version);
                    }
                    else
                    {
                        _logger.LogError("Notificar OK Servicio {Servicio} {Ambiente} {Version}, Mensate {Mensaje}",
                                    id, ambiente, version, respuesta.Content);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No se pudo enviar la configuración al servicio ", id);
            }
        }

        public Task<bool> EliminarTodasConfiguracionesAsync()
        {
            return _configuracionRepositorio.EliminarTodasConfiguracionesAsync();
        }

        public Task AgregarConfiguracionAsync(Configuracion elemento)
        {
            return _configuracionRepositorio.AgregarConfiguracionAsync(elemento);
        }

        public async Task<bool> GrabarConfiguraciones(ICollection<ConfiguracionModelo> configuraciones)
        {
            
            foreach (var configuracion in configuraciones)
            {
                var configAux = await _configuracionRepositorio.ObtenerConfiguracionAsync(
                                                    configuracion.ServicioId,
                                                    configuracion.Ambiente,
                                                    configuracion.ServicioVersion,
                                                    configuracion.Seccion,
                                                    configuracion.Clave
                                                );
                if (configAux == null)
                {
                    await _configuracionRepositorio
                                                .AgregarConfiguracionAsync(new Configuracion {
                                                    ServicioId = configuracion.ServicioId,
                                                    Ambiente = configuracion.Ambiente,
                                                    ServicioVersion = configuracion.ServicioVersion,
                                                    Seccion = configuracion.Seccion,
                                                    Clave = configuracion.Clave,
                                                    Valor = configuracion.Valor,
                                                    Descripcion = configuracion.Descripcion
                                                });
                }
                else
                {
                    await _configuracionRepositorio
                                                .ActualizarConfiguracion(
                                                    configAux.Id.ToString(),
                                                    configuracion.Seccion, 
                                                    configuracion.Clave,
                                                    configuracion.Valor, 
                                                    configuracion.Descripcion
                                                );
                }
            }

            var servciosNotificar = configuraciones
                                        .GroupBy(conf => new
                                        {
                                            conf.ServicioId,
                                            conf.Ambiente,
                                            conf.ServicioVersion
                                        });

            foreach (var servicio in servciosNotificar)
            {
                if (servicio.Key.ServicioId == "Global")
                {
                    var servicios = await 
                            _servicioRepositorio.ObtenerServiciosAsync(true);
                    foreach (var servicio2 in servicios)
                    {
                        _ = Notificar(servicio2.ServicioId, 
                                        servicio2.Ambiente, 
                                            servicio2.ServicioVersion);
                    }
                    break;
                }
                else
                {
                    _ = Notificar(servicio.Key.ServicioId, 
                                    servicio.Key.Ambiente, 
                                        servicio.Key.ServicioVersion);
                }
            }

            return true;
        }
    }

    public class ClaveValorModelo {

        public ClaveValorModelo()
        {
            
        }
        public ClaveValorModelo(string clave)
        {
            Clave = clave;
        }
        public ClaveValorModelo(string clave, object valor)
        {
            Clave = clave;
            Valor = Valor;
        }
        public string Clave { get; set; }
        public object Valor { get; set; }
    }
    
}
