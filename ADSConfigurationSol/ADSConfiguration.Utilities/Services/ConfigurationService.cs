using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.Configuration;

namespace ADSConfiguration.Utilities.Services
{
    public class ConfigurationService : IConfigurationService
    {       
        private readonly ILogger<ConfigurationService> _logger;        
        private readonly IConfiguration _config;

        public ConfigurationService(IConfiguration config,
            ILogger<ConfigurationService> logger
            )
        {
            _config = config;
            _logger = logger;
        }

        public string GetValue(string clave)
        {
            _logger.LogDebug("GetValue clave = {clave}", clave);

            try
            {
                var value = _config[clave];
                _logger.LogDebug("GetValue valor= {value}", value);

                return value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetValue");
                return "";
            }
        }
    }
}
