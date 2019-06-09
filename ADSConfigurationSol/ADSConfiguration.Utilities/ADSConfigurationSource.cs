using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADSConfiguration.Utilities
{   
    public class ADSConfigurationSource : IConfigurationSource
    {
        private readonly ADSConfigurationServiceOptions _options;
        public ADSConfigurationSource(ADSConfigurationServiceOptions options)
        {   
            _options = options;
        }
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ADSConfigurationProvider(builder, _options);
        }
    }
}
