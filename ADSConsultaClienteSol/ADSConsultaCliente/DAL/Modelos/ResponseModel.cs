using ADSConsultaCliente.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConsultaCliente.DAL.Modelos
{
    /// <summary>
    /// 
    /// </summary>
    public class ResponseModel
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "Error")]
        public int Error { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "Data")]
        public object Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "Msg")]
        public string Msg { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DatabookResponseModel : ResponseModel
    {
        [JsonProperty(PropertyName = "Data")]
        public new ResponseDatabookModel Data { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PlaResponseModel : ResponseModel
    {
        [JsonProperty(PropertyName = "Data")]
        public new DatosPersonaPlaModel Data { get; set; }
    }



    /// <summary>
    /// 
    /// </summary>
    public class RegistroCivilResponseModel : ResponseModel
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "Data")]
        public new ResponseRegistroCivilModel Data { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RegistroCivilSuccessModel
    {
        [JsonProperty(PropertyName = "Min")]
        public int Min { get; set; }

        [JsonProperty(PropertyName = "Page")]
        public int Page { get; set; }

        [JsonProperty(PropertyName = "Data")]
        public ResponseRegistroCivilModel Data { get; set; }
    }

    
}
