using ADSRegistroCivil.Domain.Siaerp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSRegistroCivil.DAL.Context
{
    public class ApplicationContext : DbContext
    {
        public static string GetConnectionSiaerp()
        {
            return Startup.ConnectionStringSiaerp;
        }

        protected override void OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
        {
            var con = GetConnectionSiaerp();
            optionsBuilder.UseOracle(con).EnableSensitiveDataLogging(true);
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<ConsultaPersonaLog> ConsultaPersonaLogs { get; set; }
    }
}
