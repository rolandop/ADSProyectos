using ADSConfiguration.DAL.Entities;
using ADSConfiguration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConfiguration.DAL
{
    public interface IConfigurationRepository
    {
        Task<IEnumerable<Configuration>> GetConfigurationsAsync();
        Task<Configuration> GetConfigurationAsync(string id);
        Task<Configuration> GetConfigurationAsync(string serviceId, string environment, string version, string section, string key);
        Task<ICollection<Configuration>> GetConfigurationsAsync(string serviceId, string environment, string version);
        Task<ICollection<Configuration>> GetGlobalConfigurationsAsync(string environment);
        // add new Configuracion document
        Task AddConfigurationAsync(Configuration configuration);

        // remove a single document / Configuracion
        Task<bool> RemoveConfiguration(string id);

        // update just a single document / Configuracion
        Task<bool> UpdateConfiguration(string id, string section, string key,
                                                string value, string description);        

        // should be used with high cautious, only in relation with demo setup
        Task<bool> RemoveAllConfigurationsAsync();
        
    }
}
