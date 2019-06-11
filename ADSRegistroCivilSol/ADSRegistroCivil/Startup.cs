using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ADSRegistroCivil.DAL;
using ADSRegistroCivil.DAL.Contexto;
using ADSRegistroCivil.DAL.Modelos;
using ADSRegistroCivil.DAL.Repositorio;
using ADSRegistroCivil.Servicios;
using ADSUtilities;
using ADSUtilities.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace ADSRegistroCivil
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RegistroCivilContexto>(options =>
            {
                var connection = Configuration.GetSection("Global:Services:Siaerp:ConnectionString").Value;
                options.UseOracle(connection);
            });

            services.AddDbContext<OdsContexto>(options =>
            {
                var connection = Configuration.GetSection("registrocivil:ConnectionStrings:OdsConnection").Value;
                options.UseOracle(connection);
            }
            );

            services.AddScoped<IRegistroCivilServicio, RegistroCivilServicio>();
            services.AddScoped<IConsultaLogRepositorio, ConsultaLogRepositorio>();
            services.AddScoped<IDatosPersonaRcRepositorio, DatosPersonaRcRepositorio>();
            services.AddADSConfiguration();
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
            services.AddADSLogger();
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var cultureInfo = new CultureInfo("ec-EC");
            cultureInfo.NumberFormat.CurrencySymbol = "$";
            cultureInfo.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(SwaggerConfiguration.EndpointUrl, SwaggerConfiguration.EndpointDescription);
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            loggerFactory.AddADSLogger(c => {
                c.LogLevel = LogLevel.Information;
                c.Service = Configuration.GetSection("ServiceId").Value;
            }, app);
            ResponseMessages.BAD_REQUEST = Configuration.GetSection("Global:Messages:BadRequest").Value;
            ResponseMessages.NOT_FOUND = Configuration.GetSection("Global:Messages:NotFound").Value;
            ResponseMessages.INTERNAL_ERROR = Configuration.GetSection("Global:Messages:InternalError").Value;
            ResponseMessages.OK = Configuration.GetSection("Global:Messages:Ok").Value;
            app.UseADSConfiguration();
            app.UseMvc();
            
        }
    }
}
