using ADSConfiguracion.DAL;
using ADSConfiguracion.DAL.Entities;
using ADSConfiguracion.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConfiguracion.Servicios
{
    public interface IConfigurationService
    {
        Task<object> GetConfigurationService(string id, string ambiente, string version);
        Task Subscribe(SubscribeModel subscribe);
        Task Clonar(CloneModel clonar);
        Task<bool> RemoveAllConfigutarionsAsync();
        Task AddConfigurationAsync(Configuration elemento);
        Task<bool> Save(ICollection<ConfigurationModel> configuraciones);
        Task Notify(string id, string ambiente, string version);
    }
}
