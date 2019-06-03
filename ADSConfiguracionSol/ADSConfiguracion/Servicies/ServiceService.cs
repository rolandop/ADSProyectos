using ADSConfiguracion.DAL;
using ADSConfiguracion.DAL.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConfiguracion.Servicios
{
    public class ServiceService : IServiceService
    {
        private readonly ILogger<ServiceService> _logger;
        private readonly IServiceRepository _servicioRepositorio;
        public ServiceService(ILogger<ServiceService> logger,          
                                        IServiceRepository servicioRepositorio)
        {
            _logger = logger;            
            _servicioRepositorio = servicioRepositorio;
        }

        public Task<Service> GetServiceAsync(string id, string ambiente, string version)
        {
            return _servicioRepositorio.GetServiceAsync(id, ambiente, version);
        }

        public Task<ICollection<Service>> GetServicesAsync(bool activo)
        {
            _logger.LogInformation("ObtenerServiciosAsync");

            return  _servicioRepositorio.GetServicesAsync(activo);
        }
    }



}
