
using ADSConfiguration.Utilities.Services;
using ADSRegistroCivil.DAL.Entidades.Siaerp;
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
    public class RegistroCivilContexto : DbContext
    {
        private readonly IRegistroCivilServicio _configuracionServicio;
        private readonly IConfigurationService _configurationService;

        /// <summary>
        /// 
        /// </summary>
        public RegistroCivilContexto()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configurationService"></param>
        //Constructor con parametros para la configuracion
        public RegistroCivilContexto(DbContextOptions<RegistroCivilContexto> options, IConfigurationService configurationService)
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
                var con = _configurationService.GetValue("Global:Services:Siaerp:ConnectionString");
                optionsBuilder.UseOracle(con);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<ConsultaPersonaLog> ConsultaPersonaLogs { get; set; }
    }
}
