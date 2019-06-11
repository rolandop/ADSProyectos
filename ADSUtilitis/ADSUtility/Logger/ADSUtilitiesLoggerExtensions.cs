using ADSUtilities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADSUtilities.Logger
{
    public static class ADSUtilitiesLoggerExtensions
    {
        /// <summary>
        /// Obtiene current traceId
        /// </summary>
        /// <param name="controllerBase"></param>
        /// <returns></returns>
        public static string TraceId(this ControllerBase controllerBase)
        {

            if (string.IsNullOrWhiteSpace(ADSUtilitiesLoggerEnvironment.TraceId))
            {
                if (controllerBase.Request.Headers.ContainsKey("TraceId"))
                {
                    var traceId = controllerBase.Request.Headers["TraceId"];
                    ADSUtilitiesLoggerEnvironment.TraceId = traceId;
                }
                else
                {
                    var traceId = DateTime.Now.Ticks.ToString();
                    ADSUtilitiesLoggerEnvironment.TraceId = traceId;
                }
            }
           
            return ADSUtilitiesLoggerEnvironment.TraceId;
        }

        /// <summary>
        /// Set custom traceId
        /// </summary>
        /// <param name="controllerBase"></param>
        /// <param name="traceId"></param>
        public static void TraceId(this ControllerBase controllerBase, string traceId)
        {
            ADSUtilitiesLoggerEnvironment.TraceId = traceId;            
        }

        public static IServiceCollection AddADSLogger(this IServiceCollection services,
            Action<string> configure)
        {
            var kafkaServer = "";
            configure(kafkaServer);
            services.AddADSLogger(kafkaServer);

            return services;
        }

        public static IServiceCollection AddADSLogger(this IServiceCollection services)
        {
            services.AddADSLogger("");

            return services;
        }

        public static IServiceCollection AddADSLogger(this IServiceCollection services,
            string kafkaServer)
        {
            if (string.IsNullOrWhiteSpace(kafkaServer))
            {
                var serviceProvider = services.BuildServiceProvider();
                var configuration = serviceProvider.GetService<IConfiguration>();

                kafkaServer = configuration.GetSection("Global:Services:kafka:Service").Value;

                if (string.IsNullOrWhiteSpace(kafkaServer))
                    kafkaServer =
                            Environment
                                .GetEnvironmentVariable("Global__Services__kafka__Service");




            }
                

            if (string.IsNullOrWhiteSpace(kafkaServer))
                kafkaServer = "kafka:9092";

            ADSUtilitiesLoggerProducer.KafkaServer = kafkaServer;

            return services;
        }

        public static ILoggerFactory AddADSLogger(this ILoggerFactory loggerFactory,
            ADSUtilitiesLoggerConfiguration config, IApplicationBuilder app)
        {
            app.UseMiddleware<ADSUtilitiesLoggerRequest>();
            loggerFactory.AddProvider(new ADSUtilitiesLoggerProvider(config));
            return loggerFactory;
        }
        public static ILoggerFactory AddADSLogger(this ILoggerFactory loggerFactory
            , IApplicationBuilder app)
        {
            var config = new ADSUtilitiesLoggerConfiguration();
            return loggerFactory.AddADSLogger(config, app);
        }
        public static ILoggerFactory AddADSLogger(this ILoggerFactory loggerFactory, 
            Action<ADSUtilitiesLoggerConfiguration> configure
            , IApplicationBuilder app)
        {
            var config = new ADSUtilitiesLoggerConfiguration();
            configure(config);
            return loggerFactory.AddADSLogger(config, app);
        }

    }
}
