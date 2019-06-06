using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ADSConfiguracion.Utilities;
using ADSConfiguracion.Utilities.Models;
using ADSUtilities.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace ADSConfiguracion.Cliente
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Log.Logger =
                new LoggerConfiguration()
                  .Enrich.FromLogContext()
                  .WriteTo.Console()
                  .CreateLogger();

            Configuration = configuration;
        }       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddADSConfiguration(options =>
            {
                options.Configure(new ConfigurationParamModel
                {

                    Service
                        = Configuration.GetSection("Service").Value,
                    ServiceId
                             = Configuration.GetSection("ServiceId").Value,
                    ServiceConfiguration
                            = Configuration.GetSection("Global:Services:Configuration:Service").Value,
                    ServiceVersion
                            = Configuration.GetSection("ServiceVersion").Value,
                    ServiceEnvironment
                        = Configuration.GetSection("ServiceEnvironment").Value
                });

            });
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
            services.AddADSLogger();

            services.AddSwaggerGen(swagger =>
            {
                var contact = new Contact()
                {
                    Name = Configuration.GetSection("Swagger:ContactName").Value,
                    Url = Configuration.GetSection("Swagger:ContactUrl").Value,
                    Email = Configuration.GetSection("Swagger:ContactEmail").Value
                };

                swagger.SwaggerDoc(Configuration.GetSection("Swagger:DocNameV1").Value,
                                   new Info
                                   {
                                       Title = Configuration.GetSection("Swagger:DocInfoTitle").Value,
                                       Version = Configuration.GetSection("Swagger:DocInfoVersion").Value,
                                       Description = Configuration.GetSection("Swagger:DocInfoDescription").Value,
                                       Contact = contact
                                   });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swagger.IncludeXmlComments(xmlPath);
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env
            , ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            loggerFactory.AddADSLogger(c => {
                c.LogLevel = LogLevel.Warning;
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    Configuration.GetSection("Swagger:EndpointUrl").Value,
                    Configuration.GetSection("Swagger:EndpointDescription").Value);
            });

            app.UseHttpsRedirection();
            app.UseADSConfiguracion();
            
            app.UseMvc();
        }
    }
}
