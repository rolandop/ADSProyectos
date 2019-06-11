using ADSConfiguration.Utilities.Services;
using ADSDataBook.DAL.Entidades.MySql;
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
    public class MySqlContext : DbContext
    {
        private readonly IDataBookService _configuracionServicio;
        private readonly IConfigurationService _configurationService;
        
        /// <summary>
        /// 
        /// </summary>
        public MySqlContext()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configurationService"></param>
        //Constructor con parametros para la configuracion
        public MySqlContext(DbContextOptions<MySqlContext> options, IConfigurationService configurationService)
        : base(options)
        {
            _configurationService = configurationService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        //Sobreescribimos el metodo OnConfiguring para hacer los ajustes que queramos en caso de
        //llamar al constructor sin parametros
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //En caso de que el contexto no este configurado, lo configuramos mediante la cadena de conexion
            if (!optionsBuilder.IsConfigured)
            {
                var con = _configurationService.GetValue("databook:ConnectionStrings:MySqlConnection");
                optionsBuilder.UseMySql(con);
            }
        }

        //Tablas de datos
        /// <summary>
        /// 
        /// </summary>
        public DbSet<BaseCambios> BaseCambios { get; set; }

    }
}
