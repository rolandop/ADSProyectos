using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADSUtilities.Util
{
    public class Structure
    {
        public int Code { get; set; }
        public String Message { get; set; }
        public Object Data { get; set; }
    }
    public class LogStructure
    {
        public string TransactionId { get; set; }
        public string Application { get; set; }
        public string LogLevel { get; set; }
        public Object Cuerpo { get; set; }
        public string Mesagge { get; set; }
    }
    public struct LogLevel
    {
        public string Info;
        public string Warning;
        public string Error;
        public string Critical;
    }
}
