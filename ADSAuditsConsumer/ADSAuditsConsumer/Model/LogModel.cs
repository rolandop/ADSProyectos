using System;
using System.Collections.Generic;
using System.Text;

namespace ADSAuditsConsumer.Model
{
    class LogModelIn
    {
        public string TraceId { get; set; }
        public string Service { get; set; }
        public int EventId { get; set; }
        public object LogDetail { get; set; }
        public string Message { get; set; }
        public string LogLevel { get; set; }
    }
}
