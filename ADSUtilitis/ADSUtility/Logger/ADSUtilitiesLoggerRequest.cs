using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ADSUtilities.Logger
{
    public class ADSUtilitiesLoggerRequest
    {
        private readonly RequestDelegate _next;
        public ADSUtilitiesLoggerRequest(RequestDelegate next)
        {
            _next = next;
            
        }

        public async Task Invoke(HttpContext context)
        {
            ADSUtilitiesLoggerEnvironment.TraceId = 
                                    DateTime.Now.Ticks.ToString();

            if (context.Request.Headers.ContainsKey("TraceId"))
            {
                var traceId = context.Request.Headers["TraceId"];
                ADSUtilitiesLoggerEnvironment.TraceId = traceId;
            }            

            await _next.Invoke(context);
        }
    }
}
