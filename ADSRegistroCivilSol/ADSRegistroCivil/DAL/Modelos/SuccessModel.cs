using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSRegistroCivil.DAL.Modelos
{
    public class SuccessModel
    {
        [JsonProperty(PropertyName = "Min")]
        public int Min { get; set; }

        [JsonProperty(PropertyName = "Page")]
        public int Page { get; set; }

        [JsonProperty(PropertyName = "Data")]
        public object Data { get; set; }
    }
}
