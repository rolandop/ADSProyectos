
using ADSConfiguration.Utilities.Services;
using ADSRegistroCivil.DAL.Entidades.Ods;
using ADSRegistroCivil.Servicios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSRegistroCivil.DAL.Contexto
{
    /// <summary>
    /// 
    /// </summary>
    public class OdsContexto : DbContext
    {
        private readonly IRegistroCivilServicio _configuracionServicio;
        private readonly IConfigurationService _configurationService;

        /// <summary>
        /// 
        /// </summary>
        public OdsContexto()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configurationService"></param>
        public OdsContexto(DbContextOptions<OdsContexto> options, IConfigurationService configurationService)
                : base(options)
        {
            _configurationService = configurationService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var con = _configurationService.GetValue("registrocivil:ConnectionStrings:OdsConnection");
                optionsBuilder.UseOracle(con);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<DataClientesRc> DataClientes { get; set; }
    }


}
