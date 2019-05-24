using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADSConfiguracion.DAL;
using ADSConfiguracion.DAL.Entidades;
using ADSConfiguracion.Modelos;
using ADSConfiguracion.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ADSConfiguracion.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SistemaController : ControllerBase
    {
        private readonly ILogger<ConfiguracionController> _logger;
        private readonly IConfiguracionServicio _configuracionServicio;

        public SistemaController(ILogger<ConfiguracionController> logger,
                                        IConfiguracionServicio configuracionServicio)
        {
            _logger = logger;
            _configuracionServicio = configuracionServicio;
        }       

        [HttpGet("{opcion}")]
        public async Task<string> Sistema(string opcion)
        {
            _logger.LogTrace("Sistema", opcion);

            if (opcion == "iniciar")
            {
                _logger.LogTrace("Sistema", "iniciar");

                await _configuracionServicio.EliminarTodasConfiguracionesAsync();

                //var name = _configuracionRepositorio.CreateIndex();                
               

                await _configuracionServicio.AgregarConfiguracionAsync(new Configuracion
                {
                    ServicioId = "Global",
                    ServicioVersion = "1.0",
                    Ambiente = "DEV",
                    Seccion = "Services/Logs",
                    Clave = "ServiceUrl",
                    Valor = "https://localhost:44343",
                    Descripcion = ""
                });

                await _configuracionServicio.AgregarConfiguracionAsync(new Configuracion
                {
                    ServicioId = "Global",
                    ServicioVersion = "1.0",
                    Ambiente = "DEV",
                    Seccion = "Services/Siaerp",
                    Clave = "ConnectionString",
                    Valor = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.1.9)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=quito1.asegsur.com.ec)));User Id=AESERP;Password=AESERP;Pooling=false;",
                    Descripcion = ""
                });

                await _configuracionServicio.AgregarConfiguracionAsync(new Configuracion
                {
                    ServicioId = "Global",
                    ServicioVersion = "1.0",
                    Ambiente = "DEV",
                    Seccion = "Services/Siaerp",
                    Clave = "ServiceUrl",
                    Valor = "https://www.aseguradoradelsur.com.ec/siaerptesting",
                    Descripcion = ""
                });

                await _configuracionServicio.AgregarConfiguracionAsync(new Configuracion
                {
                    ServicioId = "Global",
                    ServicioVersion = "1.0",
                    Ambiente = "DEV",
                    Seccion = "MongoConnection",
                    Clave = "ConnectionString",
                    Valor = "mongodb://mongoadmin:asegsys@localhost",
                    Descripcion = ""
                });

                await _configuracionServicio.AgregarConfiguracionAsync(new Configuracion
                {
                    ServicioId = "Global",
                    ServicioVersion = "1.0",
                    Ambiente = "DEV",
                    Seccion = "MongoConnection",
                    Clave = "Database",
                    Valor = "ConfiguracionesDb",
                    Descripcion = ""
                });

                await _configuracionServicio.AgregarConfiguracionAsync(new Configuracion
                {
                    ServicioId = "Global",
                    ServicioVersion = "1.0",
                    Ambiente = "DEV",
                    Seccion = "MongoConnection",
                    Clave = "User",
                    Valor = "mongoadmin",
                    Descripcion = "Aplicación de logs y auditorias"
                });

                await _configuracionServicio.AgregarConfiguracionAsync(new Configuracion
                {
                    ServicioId = "Global",
                    ServicioVersion = "1.0",
                    Ambiente = "DEV",
                    Seccion = "MongoConnection",
                    Clave = "Password",
                    Valor = "asegsys",
                    Descripcion = ""
                });

               

                await _configuracionServicio.AgregarConfiguracionAsync(new Configuracion
                {
                    ServicioId = "Databook",
                    ServicioVersion = "1.0",
                    Ambiente = "DEV",
                    Seccion = "Logging/LogLevel",
                    Clave = "Default",
                    Valor = "Warning",
                    Descripcion = ""
                });

                await _configuracionServicio.AgregarConfiguracionAsync(new Configuracion
                {
                    ServicioId = "Databook",
                    ServicioVersion = "1.0",
                    Ambiente = "DEV",
                    Seccion = "",
                    Clave = "Endpoint",
                    Valor = "https://",
                    Descripcion = "Ruta para consulta de servicio externo databook"
                });

                _logger.LogInformation("Sistema", "Configuración creada exitosamente.!");

                return "Configuración creada exitosamente.!";
            }

            _logger.LogTrace("Sistema", "Opción no válida");

            return "Opción no válida";
        }
        

    }
}