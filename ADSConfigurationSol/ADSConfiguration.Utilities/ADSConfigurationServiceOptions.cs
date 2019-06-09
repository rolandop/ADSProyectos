using ADSConfiguration.Utilities;
using ADSConfiguration.Utilities.Models;
using ADSConfiguration.Utilities.Services;
using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public class ADSConfigurationServiceOptions
    {
        public string Service { get; set; }
        public string ServiceId { get; set; }
        public string ServiceConfiguration { get; set; }
        public string ServiceVersion { get; set; }
        public string ServiceEnvironment { get; set; }
        public string Path { get; set; }
    }
}
