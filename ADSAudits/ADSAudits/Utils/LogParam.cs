using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;

namespace ADSAudits.Utils
{
    public class LogParam
    {
        public string TraceId { get; set; }
        public string Service { get; set; }
        public string LogLevel { get; set; }
        public int EventId { get; set; }
        public object LogDetail { get; set; }
        public string Message { get; set; }

    }

    public class Resul : IEnumerable
    {
        public int Code { get; set; }
        public String Message { get; set; }
        public Object Data { get; set; }

        public IEnumerator GetEnumerator()
        {
            yield return new Resul();
        }
    }

}
