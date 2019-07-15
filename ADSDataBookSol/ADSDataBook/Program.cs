﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ADSDataBook
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {

            var host = WebHost
              .CreateDefaultBuilder(args)
              .ConfigureAppConfiguration((builderContext, config) =>
              {
                  var env = builderContext.HostingEnvironment;
                  config
                       .SetBasePath(env.ContentRootPath)
                       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                       .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true); // optional extra provider

                  config.AddADSConfiguration();
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

            //CreateWebHostBuilder(args).Build().Run();
            //var host = WebHost
            //  .CreateDefaultBuilder(args)
            //  .ConfigureLogging((hostingContext, logging) =>
            //  {
            //      logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
            //      logging.AddConsole();
            //      logging.AddDebug();
            //      logging.AddEventSourceLogger();
            //  })
            //  .UseStartup<Startup>()
            //  .Build();

            //host.Run();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
