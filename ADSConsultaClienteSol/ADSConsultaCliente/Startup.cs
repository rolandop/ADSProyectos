using ADSConfiguration.Utilities;
using ADSConfiguration.Utilities.Models;
using ADSConsultaCliente.DAL;
using ADSConsultaCliente.DAL.Modelos;
using ADSConsultaCliente.Models;
using ADSConsultaCliente.Services;
using ADSUtilities.Logger;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace ADSConsultaCliente
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Log.Logger =
               new LoggerConfiguration()
                 .Enrich.FromLogContext()
                 .WriteTo.Console()
                 .CreateLogger();
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appSettings.json").Build();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>

        public void ConfigureServices(IServiceCollection services)
        {
            StartConfiguracion(services);

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingObjects());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<RuiaContext>(options => options.UseMySql(Configuration.GetSection("ConnectionStrings").GetSection("RuiaConnection").Value));
            services.AddDbContext<SiaerpContext>(options => options.UseOracle(Configuration.GetSection("ConnectionStrings").GetSection("SiaerpConnection").Value));
            services.AddScoped<IConsultaClienteRepositorio, ConsultaClienteRepositorio>();
            services.AddScoped<ISiaerpRepositorio, SiaerpRepositorio>();
            services.AddScoped<IConsultaClienteService, ConsultaClienteService>();

            services.AddSwaggerGen(swagger =>
            {
                var contact = new Contact() { Name = SwaggerConfiguration.ContactName, Url = SwaggerConfiguration.ContactUrl };
                swagger.SwaggerDoc(SwaggerConfiguration.DocNameV1,
                                   new Info
                                   {
                                       Title = SwaggerConfiguration.DocInfoTitle,
                                       Version = SwaggerConfiguration.DocInfoVersion,
                                       Description = SwaggerConfiguration.DocInfoDescription,
                                       Contact = contact
                                   }
                                    );
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swagger.IncludeXmlComments(xmlPath);
            });

        }

        private void StartConfiguracion(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
            services.AddADSConfiguration();
            services.AddADSLogger();

            services.AddSingleton<IConsultaClienteRepositorio, ConsultaClienteRepositorio>();
            services.AddSingleton<IConsultaClienteService, ConsultaClienteService>();

        }

        // This method gets called by the runtime. Use this method to add services to the container.


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// /
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddADSLogger(c =>
            {
                c.LogLevel = LogLevel.Warning;
                c.Service = Configuration.GetSection("ServiceId").Value;
            }, app);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(SwaggerConfiguration.EndpointUrl, SwaggerConfiguration.EndpointDescription);
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseADSConfiguration();
            app.UseMvc();
        }
    }
}
