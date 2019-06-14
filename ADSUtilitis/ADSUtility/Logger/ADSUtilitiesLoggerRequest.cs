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
            HttpContext context_ = ADSUtilitiesLoggerEnvironment.Current;

            context.Request.Headers.Add("RequestStartDate", DateTime.Now.Ticks.ToString());
            if (!context.Request.Headers.ContainsKey("TraceId"))
            {
                var traceId = DateTime.Now.Ticks.ToString();
                context.Request.Headers.Add("TraceId", traceId);
            }
            
            await _next.Invoke(context);
        }
    }
}
