using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConfiguracion.Models
{
    public class CloneModel
    {
        public string ServiceId { get; set; }
        public string ServiceVersion { get; set; }
        public string Environment { get; set; }
        public string NewServiceId { get; set; }
        public string NewEnvironment { get; set; }
        public string NewVersion { get; set; }
    }
}
