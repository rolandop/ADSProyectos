using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSDataBook.DAL.Modelos
{
    /// <summary>
    /// 
    /// </summary>
    public class SuccessModel
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "Min")]
        public int Min { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "Page")]
        public int Page { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "Data")]
        public object Data { get; set; }
    }
}
