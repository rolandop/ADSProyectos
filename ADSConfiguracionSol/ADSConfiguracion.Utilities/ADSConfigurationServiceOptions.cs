using ADSConfiguracion.Utilities;
using ADSConfiguracion.Utilities.Models;
using ADSConfiguracion.Utilities.Services;
using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public class ADSConfigurationServiceOptions
    {
        public static ConfigurationParamModel Parameters { get; set; }
        public void Configure(ConfigurationParamModel parameters) {

            Parameters = parameters;

        }
    }
}
