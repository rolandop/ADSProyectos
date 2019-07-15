
using ADSConfiguration.Utilities.Services;
using ADSDataBook.DAL.Entidades.Oracle;
using ADSDataBook.Servicios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSDataBook.DAL.Contexto
{
    
    /// <summary>
    /// 
    /// </summary>
    public class OracleContext : DbContext
    {
        private readonly IDataBookService _configuracionServicio;
        private readonly IConfigurationService _configurationService;

        /// <summary>
        /// 
        /// </summary>
        public OracleContext()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configurationService"></param>
        //Constructor con parametros para la configuracion
        public OracleContext(DbContextOptions<OracleContext> options, IConfigurationService configurationService)
                : base(options)
        {
            _configurationService = configurationService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("AESERP");
            base.OnModelCreating(modelBuilder);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var con = _configurationService.GetValue("Global:Services:Siaerp:ConnectionString");
                optionsBuilder.UseOracle(con, options => options.UseOracleSQLCompatibility("11"));
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<ConsultaPersonaLog> ConsultaPersonaLogs { get; set; }
    }
}
