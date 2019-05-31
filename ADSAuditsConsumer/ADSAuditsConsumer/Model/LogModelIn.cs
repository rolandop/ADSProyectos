using System;
using System.Collections.Generic;
using System.Text;

namespace ADSAuditsConsumer.Model
{
    class LogModelIn
    {
        public string TransactionId { get; set; }
        public string Application { get; set; }
        public string LogLevel { get; set; }
        public Object Cuerpo { get; set; }
        public string Mesagge { get; set; }
    }
}
