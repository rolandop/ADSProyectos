using ADSRegistroCivil.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSRegistroCivil.Modelos
{
    public class PersonaLogModelo
    {
        public int IdConsultaPersonaLog { get; set; }
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public RolBusqueda Rol { get; set; }
        public OrigenBusqueda Origen { get; set; }
        public DateTime Fecha { get; set; }
        public string Aplicacion { get; set; }
        public string UrlLlamada { get; set; }
        public string Respuesta { get; set; }
        public AccionConsulta Accion { get; set; }
        public int BasePropia { get; set; }
        public int RegistroCivil { get; set; }
        public string NombreCompletoUsuario { get; set; }
        public string CedulaConsultada { get; set; }
    }
}
