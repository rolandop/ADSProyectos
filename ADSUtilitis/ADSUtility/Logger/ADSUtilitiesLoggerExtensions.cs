using ADSUtilities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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
            var traceId = "";

            if (!controllerBase.Request.Headers.ContainsKey("TraceId"))
            {
                traceId = DateTime.Now.Ticks.ToString();
                controllerBase.Request.Headers.Add("TraceId", traceId);
            }
            else
            {
                traceId = controllerBase.Request.Headers["TraceId"];
            }
           
            return traceId;
        }

        /// <summary>
        /// Set custom traceId
        /// </summary>
        /// <param name="controllerBase"></param>
        /// <param name="traceId"></param>
        public static void TraceId(this ControllerBase controllerBase, string traceId)
        {
            if (!controllerBase.Request.Headers.ContainsKey("TraceId"))
            {
                controllerBase.Request.Headers.Add("TraceId", traceId);
            }
            else
            {
                controllerBase.Request.Headers["TraceId"] = traceId;
            }
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
          
            services.AddSingleton<IHttpContextAccessor, ADSUtilities.Logger.HttpContextAccessor>();

            return services;
        }

        public static ILoggerFactory AddADSLogger(this ILoggerFactory loggerFactory,
            ADSUtilitiesLoggerConfiguration config, IApplicationBuilder app)
        {
            app.UseMiddleware<ADSUtilitiesLoggerRequest>();

          ///  var a = app.ApplicationServices.GetService<ADSUtilities.Logger.HttpContextAccessor>();

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
            Action<ADSUtilitiesLoggerConfiguration> configure, IApplicationBuilder app)
        {
            var config = new ADSUtilitiesLoggerConfiguration();
            configure(config);
            return loggerFactory.AddADSLogger(config, app);
        }

    }
}
