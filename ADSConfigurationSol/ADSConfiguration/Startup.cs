using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ADSConfiguration.DAL;
using ADSConfiguration.DAL.Models;
using ADSConfiguration.Servicios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace ADSConfiguration
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Log.Logger = 
                new LoggerConfiguration()
                  .Enrich.FromLogContext()
                  .WriteTo.Console()
                  .CreateLogger();

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            services.Configure<DatabaseConfigurationModel>(options =>
            {
               options.Host
                         = Configuration.GetSection("MongoDB:Host").Value;
                options.Database
                         = Configuration.GetSection("MongoDB:Database").Value;
                options.User
                         = Configuration.GetSection("MongoDB:User").Value;
                options.Password
                         = Configuration.GetSection("MongoDB:Password").Value;
                
            });

            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddScoped<IServiceService, ServiceService>();

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

            services.AddSwaggerGen(swagger =>
            {
                var contact = new Contact() {
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            ConfigureJobsIoc(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env
                , IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    Configuration.GetSection("Swagger:EndpointUrl").Value, 
                    Configuration.GetSection("Swagger:EndpointDescription").Value);
            });

            app.UseHttpsRedirection();
            app.UseMvc();
            StartJobs(app, lifetime);
        }

        #region Quartz

        protected void ConfigureJobsIoc(IServiceCollection services)
        {
            ConfigureQuartz(services, typeof(ValidacionServicioJob));
        }

        private void ConfigureQuartz(IServiceCollection services, params Type[] jobs)
        {
            services.AddSingleton<IJobFactory, QuartzJobFactory>();
            services.Add(
                jobs.Select(jobType => new ServiceDescriptor(jobType, jobType, ServiceLifetime.Singleton))
            );

            services.AddSingleton(provider =>
            {
                var schedulerFactory = new StdSchedulerFactory();
                var scheduler = schedulerFactory.GetScheduler().Result;
                scheduler.JobFactory = provider.GetService<IJobFactory>();
                scheduler.Start();
                return scheduler;
            });
        }

        protected void StartJobs(IApplicationBuilder app, IApplicationLifetime lifetime)
        {
            var scheduler = app.ApplicationServices.GetService<IScheduler>();

            //TODO: use some config
            QuartzServicesUtilities.StartJob<ValidacionServicioJob>(scheduler, TimeSpan.FromSeconds(60));

            lifetime.ApplicationStarted.Register(() => scheduler.Start());
            lifetime.ApplicationStopping.Register(() => scheduler.Shutdown());
        }




        #endregion


    }
}
