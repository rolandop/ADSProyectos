using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADSUtilities.Models
{
    public class ResultModel
    {
        public int Code { get; set; }
        public String Message { get; set; }
        public Object Data { get; set; }
    }
   
}
