using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ADSRegistroCivil.DAL.Entidades.Ods
{
    /// <summary>
    /// 
    /// </summary>
    [Table("DATA_CLIENTES_RC")]
    public class DataClientesRc
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string IDENTIFICACION { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NOMBRE_COMPLETO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string COD_GENERO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string GENERO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DIA_NACIMIENTO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MES_NACIMIENTO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ANIO_NACIMIENTO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FECHA_NACIMIENTO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string COD_ESTADO_CIVIL { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ESTADO_CIVIL { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NOMBRE_CONYUGE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DIRECCION_DOMICILIO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NRO_DIRECCION_DOMICILIO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NOMBRE_PADRE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NOMBRE_MADRE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FECHA_ACTUALIZACION { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CONDICION { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FECHA_DEFUNCION { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NACIONALIDAD { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PROFESION { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SEXO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FECHA_CEDULACION { get; set; }
        
    }
}
