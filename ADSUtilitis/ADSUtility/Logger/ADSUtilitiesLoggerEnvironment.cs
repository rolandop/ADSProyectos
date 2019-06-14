using ADSUtilities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADSUtilities.Logger
{
    public class ADSUtilitiesLoggerEnvironment
    {
        public static  string TraceId => Current.Request.Headers["TraceId"];
        public static DateTime RequestStart => new DateTime(Convert.ToInt64(Current.Request.Headers["RequestStartDate"]));
        public static HttpContext Current => new HttpContextAccessor().HttpContext;

    }
}
