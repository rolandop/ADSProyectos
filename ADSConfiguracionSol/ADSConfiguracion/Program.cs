using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ADSConfiguracion
{
    public class Program
    {
        public static void Main(string[] args)
        {          

            var host = WebHost
               .CreateDefaultBuilder(args)
               .UseUrls($"http://localhost:6808")
               .ConfigureLogging((hostingContext, logging) =>
               {
                   logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                   logging.AddConsole();
                   logging.AddDebug();
                   logging.AddEventSourceLogger(); 
                   
               })
               .UseStartup<Startup>()
               
               .Build();            

            host.Run();

        }

      

    }
}
