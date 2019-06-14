using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ADSUtilities.Logger
{
    public class HttpContextAccessor : IHttpContextAccessor
    {
        private static AsyncLocal<HttpContext> _httpContextCurrent = new AsyncLocal<HttpContext>();

        public HttpContext HttpContext
        {
            get => _httpContextCurrent.Value;
            set => _httpContextCurrent.Value = value;
        }
    }
}
