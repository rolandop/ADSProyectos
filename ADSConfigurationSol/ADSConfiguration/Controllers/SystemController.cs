using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADSConfiguration.DAL;
using ADSConfiguration.DAL.Entities;
using ADSConfiguration.DAL.Models;
using ADSConfiguration.Models;
using ADSConfiguration.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ADSConfiguration.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly ILogger<ConfigurationController> _logger;
        private readonly IConfigurationService _configurationService;

        public SystemController(ILogger<ConfigurationController> logger,
                                        IConfigurationService configurarionService)
        {
            _logger = logger;
            _configurationService = configurarionService;
        }       

        [HttpGet("{option}")]
        public async Task<string> Options(string option)
        {
            _logger.LogInformation("Sistema", option);

            if (option == "init")
            {
                _logger.LogInformation("Sistema", "iniciar");

                await _configurationService.RemoveAllConfigutarionsAsync();

                await _configurationService.AddConfigurationAsync(new Configuration
                {
                    ServiceId = "Global",
                    ServiceVersion = "1.0",
                    Environment = "DEV",
                    Section = "Services/Logs",
                    Key = "ServiceUrl",
                    Value = "https://localhost:44343",
                    Description = ""
                });

                await _configurationService.AddConfigurationAsync(new Configuration
                {
                    ServiceId = "Global",
                    ServiceVersion = "1.0",
                    Environment = "DEV",
                    Section = "Services/Configuration",
                    Key = "Service",
                    Value = "https://localhost:60220",
                    Description = ""
                });

                await _configurationService.AddConfigurationAsync(new Configuration
                {
                    ServiceId = "Global",
                    ServiceVersion = "1.0",
                    Environment = "DEV",
                    Section = "Services/Siaerp",
                    Key = "ConnectionString",
                    Value = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.1.9)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=quito1.asegsur.com.ec)));User Id=AESERP;Password=AESERP;Pooling=false;",
                    Description = ""
                });

                await _configurationService.AddConfigurationAsync(new Configuration
                {
                    ServiceId = "Global",
                    ServiceVersion = "1.0",
                    Environment = "DEV",
                    Section = "Services/Siaerp",
                    Key = "ServiceUrl",
                    Value = "https://www.aseguradoradelsur.com.ec/siaerptesting",
                    Description = ""
                });

                await _configurationService.AddConfigurationAsync(new Configuration
                {
                    ServiceId = "Global",
                    ServiceVersion = "1.0",
                    Environment = "DEV",
                    Section = "MongoConnection",
                    Key = "ConnectionString",
                    Value = "mongodb://mongoadmin:asegsys@localhost",
                    Description = ""
                });

                await _configurationService.AddConfigurationAsync(new Configuration
                {
                    ServiceId = "Global",
                    ServiceVersion = "1.0",
                    Environment = "DEV",
                    Section = "MongoConnection",
                    Key = "Database",
                    Value = "ConfigurationsDb",
                    Description = ""
                });

                await _configurationService.AddConfigurationAsync(new Configuration
                {
                    ServiceId = "Global",
                    ServiceVersion = "1.0",
                    Environment = "DEV",
                    Section = "MongoConnection",
                    Key = "User",
                    Value = "mongoadmin",
                    Description = "Aplicación de logs y auditorias"
                });

                await _configurationService.AddConfigurationAsync(new Configuration
                {
                    ServiceId = "Global",
                    ServiceVersion = "1.0",
                    Environment = "DEV",
                    Section = "MongoConnection",
                    Key = "Password",
                    Value = "asegsys",
                    Description = ""
                });

               

                await _configurationService.AddConfigurationAsync(new Configuration
                {
                    ServiceId = "adsconfigurationcliente",
                    ServiceVersion = "1.0",
                    Environment = "DEV",
                    Section = "Logging/LogLevel",
                    Key = "Default",
                    Value = "Warning",
                    Description = ""
                });

                await _configurationService.AddConfigurationAsync(new Configuration
                {
                    ServiceId = "adsconfigurationcliente",
                    ServiceVersion = "1.0",
                    Environment = "DEV",
                    Section = "",
                    Key = "Endpoint",
                    Value = "https://",
                    Description = "Ruta para consulta de servicio externo databook"
                });

                _logger.LogInformation("Sistema", "Configuración creada exitosamente.!");

                return "Configuración creada exitosamente.!";
            }

            _logger.LogInformation("Sistema", "Opción no válida");

            return "Opción no válida";
        }
        

    }
}