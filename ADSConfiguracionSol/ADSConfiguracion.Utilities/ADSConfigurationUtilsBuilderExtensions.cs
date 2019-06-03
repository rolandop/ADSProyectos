using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADSConfiguracion.Utilities
{
    public static class ADSConfigurationUtilsBuilderExtensions
    {
        public static IApplicationBuilder UseADSConfiguracionUtils(this IApplicationBuilder app, 
                                Action<ADSConfigurationBuildOptions> setupAction = null)
        {
            var options = new ADSConfigurationBuildOptions {
                Url = "configuration"
            };

            if (setupAction != null)
            {
                setupAction.Invoke(options);
            }           

            return app;
        }
    }
}
