using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADSConfiguracion.Cliente.Configuracion.Modelos;
using ADSConfiguracion.Cliente.Configuracion.Servicios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ADSConfiguracion.Cliente
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            StartConfiguracion(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        private void StartConfiguracion(IServiceCollection services)
        {
            services.Configure<ConfiguracionParamModelo>(options =>
            {
                options.ServiceConfiguracionUrl
                         = Configuration.GetSection("Global:Services:Logs:ServiceUrl").Value;

                options.ServiceUrl
                        = Configuration.GetSection("ServiceUrl").Value;

                options.ServiceId
                         = Configuration.GetSection("ServiceId").Value;

                options.Environment
                        = Configuration.GetSection("ServiceEnvironment").Value;

                options.ServiceVersion
                        = Configuration.GetSection("ServiceVersion").Value;



                /* var urlServicio = Environment.GetEnvironmentVariable("CONFIGURATION_SERVICE_URL");

                 if (!string.IsNullOrWhiteSpace(urlServicio))
                 {
                     options.UrlServicio = urlServicio;
                 }*/
            });

            services.AddSingleton<IConfiguracionServicio, ConfiguracionServicio>();

            var serviceProvider = services.BuildServiceProvider();
            var configuracionService = serviceProvider.GetService<IConfiguracionServicio>();
            configuracionService.SubscribirServicio();
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

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
