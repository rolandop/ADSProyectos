using ADSUtilities.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSUtilities.Logger
{
    public interface IADSUtilitiesLogger : ILogger
    {
        //void Log<T>(LogLevel warning, EventId eventId, T logModel, Func<object, object, string> p);
        //void Log<T>(LogLevel warning, EventId eventId, T logModel, Func<object, object, string> p);
        //void Log<TState>(LogLevel warning, EventId eventId, TState logModel, Func<TState, Exception, string> formater);
//        void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter);
    }
}
