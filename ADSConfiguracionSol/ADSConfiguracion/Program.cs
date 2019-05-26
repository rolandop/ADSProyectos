using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
               //.UseUrls($"http://localhost:6808")
               .ConfigureAppConfiguration((builderContext, config) =>
               {
                   var env = builderContext.HostingEnvironment;
                   config
                        .SetBasePath(env.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true); // optional extra provider

                   //if (env.IsDevelopment()) // different providers in dev
                   //{
                   //    var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                   //    if (appAssembly != null)
                   //    {
                   //        config.AddUserSecrets(appAssembly, optional: true);
                   //    }
                   //}

                   config.AddEnvironmentVariables(); // overwrites previous values

                   if (args != null)
                   {
                       config.AddCommandLine(args);
                   }
               })
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
