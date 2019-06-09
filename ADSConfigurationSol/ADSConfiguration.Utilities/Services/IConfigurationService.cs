using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConfiguration.Utilities.Services
{
    public interface IConfigurationService: IConfiguration
    {
        string GetConfigurationJson();
        void GetConfiguration();
        void UpdateConfiguration(string configurationJson);
        void SubscribeService();
        string GetValue(string clave);
    }
}
