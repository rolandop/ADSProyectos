using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ADSUtilities.Logger
{
    public class ADSUtilitiesLoggerProvider: ILoggerProvider
    {
        private readonly ADSUtilitiesLoggerConfiguration _config;
        private readonly ConcurrentDictionary<string, ADSUtilitiesLogger> _loggers = 
                                    new ConcurrentDictionary<string, ADSUtilitiesLogger>();
        private readonly ADSUtilitiesLoggerProducer _loggerProducer;

        public ADSUtilitiesLoggerProvider(ADSUtilitiesLoggerConfiguration config)
        {
            _config = config;
            _loggerProducer = new ADSUtilitiesLoggerProducer();
        }

        public ILogger CreateLogger(string categoryName)
        {
            var service = _config.Service;

            if (string.IsNullOrWhiteSpace(service))
            {
                service = Environment.GetEnvironmentVariable("Service");
            }       

            if (string.IsNullOrWhiteSpace(service))
            {
                var assembly = typeof(ADSUtilitiesLoggerProvider).GetTypeInfo().Assembly;
                service = assembly.GetName().Name;
            }

            return _loggers
                    .GetOrAdd(categoryName, name => 
                                                new ADSUtilitiesLogger(_config.Service, _config, _loggerProducer));
        }

        public void Dispose()
        {
            _loggers.Clear();
            _loggerProducer.Dispose();
        }
    }
}
