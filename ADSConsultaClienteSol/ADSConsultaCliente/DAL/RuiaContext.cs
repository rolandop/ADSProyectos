
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

        public string ConnectionString { get; set; }

        public RuiaContext(DbContextOptions<RuiaContext> options) : base(options)
        {
            //_configurationService = configurationService;
        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<PersonaUniverso> PersonasUniverso { get; set; }
    }
}
