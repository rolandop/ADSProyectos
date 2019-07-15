using ADSConsultaPla.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace ADSConsultaPla.Services
{
    public class ConsultaPlaService : IConsultaPlaService
    {
        private readonly ILogger<ConsultaPlaService> _logger;
        private readonly IConfiguration _configuration;

        public ConsultaPlaService(ILogger<ConsultaPlaService> logger, IConfiguration configuration)
        {
            this._logger = logger;
            this._configuration = configuration;
        }

        /// <summary>
        /// Servicio consulta Pla para saber si está en lista negra
        /// </summary>
        /// <param name="identificacion"></param>
        /// <param name="nombre"></param>
        /// <param name="aplicacion"></param>
        /// <returns></returns>
        public string ConsultaSisprevServiceGet(string identificacion, string nombre, string aplicacion)
        {
            try
            {
                _logger.LogInformation("Inicio llamada servicio ExecuteSisprevService: {@identificacion} {@nombre} {@aplicacion}", identificacion, nombre, aplicacion);
                var result = ExecuteSisprevServiceGet(identificacion, nombre, aplicacion);
                _logger.LogInformation("Fin llamada servicio ExecuteSisprevService: {@identificacion} {@respuesta}", identificacion, result);
                return result;                
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// Servicio para consultar a Pla los datos de la persona
        /// </summary>
        /// <param name="persona"></param>
        /// <returns></returns>
        public DatosPersonaPlaModel ConsultaSisprevServicePost(DatosClienteModel persona)
        {
            try
            {
                _logger.LogInformation("Inicio ejecución de servicio ExecuteSisprevService: {@persona}", persona);
                var result = ExecuteSisprevServicePost(persona);
                _logger.LogInformation("Fin ejecución de servicio ExecuteSisprevService: {@persona} {@respuesta}", persona, result);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado.");
                throw ex;
            }
        }

        /// <summary>
        /// Llamada al servicio de SISPREV del ASURAPI para saber si está en lista negra
        /// </summary>
        /// <param name="identificacion"></param>
        /// <param name="nombre"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        [HttpGet]
        public string ExecuteSisprevServiceGet(string identificacion, string nombre, string app)
        {
            try
            {
                var url = string.Format("{0}?identification={1}&name={2}&app={3}", _configuration.GetSection("Global:Services:UrlSisprev:Service").Value, identificacion, nombre, app);
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
                request.RequestFormat = DataFormat.Json;
                var response = client.Execute(request);
                if (response.StatusDescription == "OK")
                {
                    var aux = response.Content;
                    aux = aux.Replace("\\", "").TrimStart('"').TrimEnd('"');
                    //var responseLogin = JsonConvert.DeserializeObject<string>(aux);
                    return aux;
                }
                return "";

            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// Llamada al servicio de SISPREV del ASURAPI para obtener datos de la persona
        /// </summary>
        /// <param name="persona"></param>
        /// <returns></returns>
        [HttpPost]
        public DatosPersonaPlaModel ExecuteSisprevServicePost(DatosClienteModel persona)
        {
            try
            {
                var url = string.Format("{0}", _configuration.GetSection("Global:Services:UrlSisprev:Service").Value);
                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                var data = JsonConvert.SerializeObject(persona);
                request.AddParameter("application/json", data, ParameterType.RequestBody);
                var response = client.Execute(request);
                if (response.StatusDescription == "OK")
                {
                    var aux = response.Content;
                    aux = aux.Replace("\\", "").TrimStart('"').TrimEnd('"');
                    var result = JsonConvert.DeserializeObject<DatosPersonaPlaModel>(aux);
                    return result;
                }
                return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
