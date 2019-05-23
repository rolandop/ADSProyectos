using ADSConfiguracion.DAL;
using ADSConfiguracion.DAL.Entidades;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConfiguracion.Servicios
{
    public class ServicioServicio : IServicioServicio
    {
        private readonly ILogger<ServicioServicio> _logger;
        private readonly IServicioRepositorio _servicioRepositorio;
        public ServicioServicio(ILogger<ServicioServicio> logger,          
                                        IServicioRepositorio servicioRepositorio)
        {
            _logger = logger;            
            _servicioRepositorio = servicioRepositorio;
        }

        public Task<Servicio> ObtenerServicioAsync(string id, string ambiente, string version)
        {
            return _servicioRepositorio.ObtenerServicioAsync(id, ambiente, version);
        }

        public Task<ICollection<Servicio>> ObtenerServiciosAsync(bool activo)
        {
            _logger.LogInformation("ObtenerServiciosAsync");

            return  _servicioRepositorio.ObtenerServiciosAsync(activo);
        }
    }



}
