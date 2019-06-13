
using ADSConfiguration.Utilities.Services;
using ADSConsultaCliente.DAL.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConsultaCliente.DAL
{    
    public class SiaerpContext : DbContext
    {
        private readonly IConfigurationService _configurationService;
        public SiaerpContext(DbContextOptions<SiaerpContext> options) : base(options)
        {
            //_configurationService = configurationService;
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
