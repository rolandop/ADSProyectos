using ADSConfiguration.DAL;
using ADSConfiguration.DAL.Entities;
using ADSConfiguration.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConfiguration.Servicios
{
    public interface IConfigurationService
    {
        Task<object> GetConfigurationService(string id, string ambiente, string version);
        Task Subscribe(SubscribeModel subscribe);
        Task Clonar(CloneModel clonar);
        Task<bool> RemoveAllConfigutarionsAsync();
        Task AddConfigurationAsync(Configuration elemento);
        Task<bool> Save(ICollection<ConfigurationModel> configuration);
        Task<bool> Notify(string id, string ambiente, string version);
    }
}
