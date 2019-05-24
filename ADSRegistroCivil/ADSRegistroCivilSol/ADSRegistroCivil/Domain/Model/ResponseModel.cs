using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSRegistroCivil.Domain.Model
{
    public class ResponseModel
    {
        [JsonProperty(PropertyName = "Error")]
        public int Error { get; set; }

        [JsonProperty(PropertyName = "Data")]
        public SucessModel Data { get; set; }

        [JsonProperty(PropertyName = "Msg")]
        public string Msg { get; set; }
    }
}
