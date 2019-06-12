using ADSConsultaPla.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;

        public ConsultaPlaService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        //public Task<PlaService.ExecuteResponse> ConsultaPlaService(string identificacion, string nombre, string aplicacion)
        //{
        //    try
        //    {
                
        //        var datosPla = new PlaService.WSBuscaListaTotalSoapPortClient();
        //        var temp = datosPla.ExecuteAsync(identificacion, nombre, aplicacion);
        //        return temp;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //}

        public string ConsultaSisprevService(string identificacion, string nombre, string app)
        {
            try
            {
                var result = ExecuteSisprevService(identificacion, nombre, app);
                return result;
            }
            catch (Exception)
            {
                return "";
            }
        }

        [HttpGet]
        public string ExecuteSisprevService(string identificacion, string nombre, string app)
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
    }
}
