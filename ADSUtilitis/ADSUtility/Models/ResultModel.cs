using ADSUtilities.Logger;
using Microsoft.AspNetCore.Mvc;
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

        [JsonProperty(PropertyName = "Request")]
        public RequestModel Request { get; set; }

    }

    public class RequestModel
    {   
        [JsonProperty(PropertyName = "Start")]
        public string Start { get; set; }

        [JsonProperty(PropertyName = "End")]
        public string End { get; set; }

        [JsonProperty(PropertyName = "Elapsed")]
        public double Elapsed { get; set; }

        [JsonProperty(PropertyName = "TraceId")]
        public string TraceId { get; set; }

        public static RequestModel GetRequest(ControllerBase services)
        {
            var start = ADSUtilitiesLoggerEnvironment.RequestStart;
            var now = DateTime.Now;
            return new RequestModel
            {
                Start = start.ToString("dd/MM/yyyy HH:mm:ss:fff"),
                End = now.ToString("dd/MM/yyyy HH:mm:ss:fff"),
                TraceId = ADSUtilitiesLoggerEnvironment.TraceId,
                Elapsed = (now - start).TotalMilliseconds
            };
        }

    }
}
