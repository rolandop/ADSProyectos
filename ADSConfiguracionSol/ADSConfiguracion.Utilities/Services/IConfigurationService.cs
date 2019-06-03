using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConfiguracion.Utilities.Services
{
    public interface IConfigurationService
    {
        string GetConfigurationJson();
        void GetConfiguracion();
        void UpdateConfiguration(string configuracionJson);
        void SubscribeService();
        string GetValue(string clave);
    }
}
