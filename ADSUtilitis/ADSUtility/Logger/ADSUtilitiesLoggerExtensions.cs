using ADSUtilities.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADSUtilities.Logger
{
    public static class ADSUtilitiesLoggerExtensions
    {
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
                kafkaServer =
                    Environment
                        .GetEnvironmentVariable("Global__Services__kafka__Service");

            if (string.IsNullOrWhiteSpace(kafkaServer))
                kafkaServer = "kafka:9092";

            ADSUtilitiesLoggerProducer.KafkaServer = kafkaServer;

            return services;
        }

        public static ILoggerFactory AddADSLogger(this ILoggerFactory loggerFactory,
            ADSUtilitiesLoggerConfiguration config)
        {
            loggerFactory.AddProvider(new ADSUtilitiesLoggerProvider(config));
            return loggerFactory;
        }
        public static ILoggerFactory AddADSLogger(this ILoggerFactory loggerFactory)
        {
            var config = new ADSUtilitiesLoggerConfiguration();
            return loggerFactory.AddADSLogger(config);
        }
        public static ILoggerFactory AddADSLogger(this ILoggerFactory loggerFactory, 
            Action<ADSUtilitiesLoggerConfiguration> configure)
        {
            var config = new ADSUtilitiesLoggerConfiguration();
            configure(config);
            return loggerFactory.AddADSLogger(config);
        }
    }
}
