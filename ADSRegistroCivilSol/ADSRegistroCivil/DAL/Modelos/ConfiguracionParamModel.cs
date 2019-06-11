using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSRegistroCivil.DAL.Modelos
{
    public class ConfiguracionParamModel
    {
        public string ServiceConfiguracionUrl { get; set; }
        public string ServiceName { get; set; }
        public string ServiceEnvironment { get; set; }
        public string ServiceVersion { get; set; }
        public string ServiceUrl { get; set; }

    }
}
