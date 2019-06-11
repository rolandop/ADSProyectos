using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSRegistroCivil.DAL.Modelos
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
}
