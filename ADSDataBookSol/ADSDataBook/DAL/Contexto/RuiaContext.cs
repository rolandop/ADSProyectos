
using ADSConfiguration.Utilities.Services;
using ADSDataBook.DAL.Entidades.Oracle;
using ADSDataBook.DAL.Entidades.Ruia;
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
    public class RuiaContext : DbContext
    {
        private readonly IDataBookService _configuracionServicio;
        private readonly IConfigurationService _configurationService;

        /// <summary>
        /// 
        /// </summary>
        public RuiaContext()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configurationService"></param>
        //Constructor con parametros para la configuracion
        public RuiaContext(DbContextOptions<RuiaContext> options, IConfigurationService configurationService)
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
            modelBuilder.HasDefaultSchema("RUIA");
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
                var con = _configurationService.GetValue("databook:ConnectionStrings:Ruia");
                optionsBuilder.UseOracle(con, options => options.UseOracleSQLCompatibility("11"));
            }
            

        }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<BaseCambios> BaseCambios { get; set; }

    }
}
