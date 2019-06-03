using ADSConfiguracion.DAL.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConfiguracion.Servicios
{
    public interface IServiceService
    {
        Task<ICollection<Service>> GetServicesAsync(bool activo);
        Task<Service> GetServiceAsync(string id, string ambiente, string version);
    }


    
}
