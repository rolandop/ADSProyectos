using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConfiguracion.Models
{
    public class SubscribeModel
    {
        public string ServiceId { get; set; }
        public string ServiceVersion { get; set; }
        public string Environment { get; set; }
        public string UpdateUrl { get; set; }
        public string VerifyUrl { get; set; }
    }
}
