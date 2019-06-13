using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADSUtilities.Models
{
    public class ResultModel
    {
        [JsonProperty(PropertyName = "Error")]
        public int Error { get; set; }

        [JsonProperty(PropertyName = "Msg")]
        public string Msg { get; set; }

        [JsonProperty(PropertyName = "Data")]
        public object Data { get; set; }
    }
   
}
