using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADSUtilities
{
    public static class ResponseExtensions
    {
        public static OkObjectResult ADSOk(this ControllerBase services, object parameter)
        {
            return services.Ok(new
            {

            });
            
        }

        public static OkObjectResult ADSOk(this ControllerBase services)
        {
            return services.Ok(new
            {

            });

        }

    }
}
