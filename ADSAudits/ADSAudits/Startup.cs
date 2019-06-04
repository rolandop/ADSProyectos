using System;
using ADSAudits.Controllers;
using ADSAudits.DAL.Models;
using ADSAudits.Interfaces;
using ADSAudits.Interfaces.Imp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ADSConfiguracion.Utilities;
using ADSConfiguracion.Utilities.Models;

namespace ADSAudits
{
    public class Startup
    {


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });



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

            services.AddTransient<ILogRepository, LogRepository>();
            services.AddSingleton<LogContext>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
          

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseADSConfiguracionUtils();
            app.UseMvc();
        }
    }
}
