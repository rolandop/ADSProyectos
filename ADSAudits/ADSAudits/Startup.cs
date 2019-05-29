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
using HostedServices = Microsoft.Extensions.Hosting;
using Confluent.Kafka;
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<Settings>(options =>
            {
                if (Environment.GetEnvironmentVariable("ConnectionString") != null)
                {
                    options.ConnectionString
                    = Environment.GetEnvironmentVariable("ConnectionString").ToString();
                }
                else
                {
                    options.ConnectionString
                    = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                }
                if (Environment.GetEnvironmentVariable("ConnectionString") != null)
                {
                    options.Database = Environment.GetEnvironmentVariable("Database").ToString();
                }
                else
                {
                    options.Database
                    = Configuration.GetSection("MongoConnection:Database").Value;
                }
                

            });
            services.AddTransient<ILogRepository, LogRepository>();

        


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
            app.UseMvc();
        }
    }
}
