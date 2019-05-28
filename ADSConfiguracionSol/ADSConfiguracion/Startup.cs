using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADSConfiguracion.DAL;
using ADSConfiguracion.DAL.Modelos;
using ADSConfiguracion.Servicios;
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

namespace ADSConfiguracion
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
            //services.Configure<BaseDatosConfiguracionModelo>(Configuration.GetSection("MongoSettings"));
            services.Configure<BaseDatosConfiguracionModelo>(options =>
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

            services.AddScoped<IConfiguracionRepositorio, ConfiguracionRepositorio>();
            services.AddScoped<IServicioRepositorio, ServicioRepositorio>();
            services.AddScoped<IConfiguracionServicio, ConfiguracionServicio>();
            services.AddScoped<IServicioServicio, ServicioServicio>();

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));


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
