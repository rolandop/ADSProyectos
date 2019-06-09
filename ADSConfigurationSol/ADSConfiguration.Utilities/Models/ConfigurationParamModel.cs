using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ADSConfiguration.Utilities.Models
{
    public class ConfigurationParamModel
    {
        public string Service { get; set; }
        public string ServiceId { get; set; }
        public string ServiceConfiguration { get; set; }        
        public string ServiceVersion { get; set; }
        public string ServiceEnvironment { get; set; }
      
    }
}
