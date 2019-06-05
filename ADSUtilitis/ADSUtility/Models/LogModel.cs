using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADSUtilities.Models
{
    public class LogModel
    {
        public string TraceId { get; set; }
        public string Service { get; set; }
        public string LogLevel { get; set; }
        public int EventId { get; set; }
        public object LogDetail { get; set; }
        public string Message { get; set; }
    }
}
