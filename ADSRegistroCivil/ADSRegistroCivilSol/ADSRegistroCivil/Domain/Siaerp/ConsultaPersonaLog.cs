using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ADSRegistroCivil.Domain.Siaerp
{
    [Table("ConsultaPersonaLog")]
    public class ConsultaPersonaLog
    {
        [Key]
        public int IdConsultaPersonaLog { get; set;}
        public int? IdUsuario { get; set; }
        public string Usuario { get; set; }
        public int? Rol { get; set; }
        public int? Origen { get; set; }
        public DateTime? Fecha { get; set; }
        public string Aplicacion { get; set; }
        public string UrlLlamada { get; set; }
        public string Respuesta { get; set; }
        public int? Accion { get; set; }
        public int? BasePropia { get; set; }
        public int? RegistroCivil { get; set; }
        public string NombreCompletoUsuario { get; set; }
        public string CedulaConsultada { get; set; }
    }
}
