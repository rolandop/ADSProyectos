
using ADSConfiguration.Utilities.Services;
using ADSConsultaCliente.DAL.Entidades;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConsultaCliente.DAL
{
    public class RuiaContext : DbContext
    {
        private readonly IConfigurationService _configurationService;

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
                var con = _configurationService.GetValue("Global:Services:Ruia:ConnectionString");
                optionsBuilder.UseOracle(con, options => options.UseOracleSQLCompatibility("11"));
            }
        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<PersonaUniverso> PersonasUniverso { get; set; }
    }
}
