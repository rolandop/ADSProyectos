using ADSConfiguracion.Utilities;
using ADSConfiguracion.Utilities.Models;
using ADSConfiguracion.Utilities.Services;
using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ADSConfigurationServiceExtensions
    {
        public static IServiceCollection AddADSConfiguration(this IServiceCollection services, 
                        Action<ADSConfigurationServiceOptions> setupAction = null)
        {
            var assembly = typeof(ADSConfigurationServiceExtensions).GetTypeInfo().Assembly;
            services.AddMvc()
                .AddApplicationPart(assembly);

            services.AddRouting(routing=> {
                
            });

            var options = new ADSConfigurationServiceOptions();

            ADSConfigurationServiceOptions.Parameters = new ConfigurationParamModel
            {
                ServiceId = "unnamed",
                Service = "unnamed",
                ServiceVersion = "0",
                ServiceEnvironment = "DEV",
                ServiceConfiguration = "adsconfiguracion"
            };

            if (setupAction != null)
            {
                setupAction?.Invoke(options);
            }
            
            services.AddSingleton<IConfigurationService, ConfigurationService>();

            var serviceProvider = services.BuildServiceProvider();
            var configurationService = serviceProvider.GetService<IConfigurationService>();
            configurationService.SubscribeService();

            return services;
        }        
    }
}
