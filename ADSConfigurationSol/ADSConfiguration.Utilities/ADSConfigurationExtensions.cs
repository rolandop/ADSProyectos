using ADSConfiguration.Utilities;
using ADSConfiguration.Utilities.Models;
using ADSConfiguration.Utilities.Services;
using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ADSConfigurationExtensions
    {
        public static IApplicationBuilder UseADSConfiguration(this IApplicationBuilder app)
        {
            return app;
        }

        public static IConfigurationBuilder AddADSConfiguration
                                (this IConfigurationBuilder builder,
                        Action<ADSConfigurationServiceOptions> setup)
        {
            var options = new ADSConfigurationServiceOptions();
            setup(options);
            return builder.AddADSConfiguration(options);
        }

        public static IConfigurationBuilder AddADSConfiguration
                               (this IConfigurationBuilder builder, string environmentName)
        {
            var options = new ADSConfigurationServiceOptions
            {
                ServiceId = Environment.GetEnvironmentVariable("ServiceId"),
                Service = Environment.GetEnvironmentVariable("Service"),
                ServiceVersion = Environment.GetEnvironmentVariable("ServiceVersion"),
                ServiceEnvironment = Environment
                                .GetEnvironmentVariable("ServiceEnvironment"),
                ServiceConfiguration =
                                Environment.GetEnvironmentVariable("ServiceConfiguration")
            };

            return builder.AddADSConfiguration(options);
        }

        public static IConfigurationBuilder AddADSConfiguration(
                            this IConfigurationBuilder builder,
                            ADSConfigurationServiceOptions options)
        {
            builder.Add(new ADSConfigurationSource(options));            

            return builder;
        }

        public static IServiceCollection AddADSConfiguration(this IServiceCollection services)
        {
            var assembly = typeof(ADSConfigurationExtensions).GetTypeInfo().Assembly;
            services.AddMvc()
                .AddApplicationPart(assembly);

            services.AddRouting(routing=> {
                
            });

            var options = new ADSConfigurationServiceOptions();

            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();
            
          
           
            //services.AddSingleton<IConfigurationService, ConfigurationService>();
            
            //var configurationService = serviceProvider.GetService<IConfigurationService>();
            //configurationService.SubscribeService();

            return services;
        }

        
    }
}
