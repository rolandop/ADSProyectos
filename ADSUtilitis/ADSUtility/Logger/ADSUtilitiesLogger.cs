using ADSUtilities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSUtilities.Logger
{
    public class ADSUtilitiesLogger : IADSUtilitiesLogger
    {
        private readonly string _service;        
        private readonly ADSUtilitiesLoggerConfiguration _config;
        private readonly ADSUtilitiesLoggerProducer _loggerProducer;

        public ADSUtilitiesLogger(string service, 
            ADSUtilitiesLoggerConfiguration config,
            ADSUtilitiesLoggerProducer loggerProducer)
        {
            _service = service;
            _config = config;
            _loggerProducer = loggerProducer;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {   
            var logLevelConfig = _config.LogLevel;
            var logLevelEnv = 
                Environment.GetEnvironmentVariable("Logging__LogLevel_Default");
            
            Enum.TryParse(logLevelEnv, true, out logLevelConfig);

            if (logLevelConfig == LogLevel.None)
            {
                return false;
            }

            return (int)logLevel >= (int)logLevelConfig;
        }

        public void Log<TState>(LogLevel logLevel, 
                                    EventId eventId, 
                                    TState state, 
                                    Exception exception, 
                                    Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (_config.EventId == 0 || _config.EventId == eventId.Id)
            {
                var message = formatter(state, exception);

                var traceId = ADSUtilitiesLoggerEnvironment.TraceId;

                var log = new LogModel
                {
                    LogLevel = logLevel.ToString(),
                    Service = _service,
                    EventId = eventId.Id,
                    TraceId = string.IsNullOrWhiteSpace(eventId.Name)
                                ? traceId
                                : eventId.Name,
                    Message = message,
                    //LogDetail = state
                };
                _loggerProducer.WriteMessage(log);
            }
        }
      

    }
}
