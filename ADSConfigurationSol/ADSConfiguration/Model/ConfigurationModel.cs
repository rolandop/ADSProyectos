using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConfiguration.Models
{
    public class ConfigurationModel
    {
        public string ServiceId { get; set; }
        public string ServiceVersion { get; set; }
        public string Environment { get; set; }
        public string Section { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Descripction { get; set; }
        
    }
}
