using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConsultaPla.Models
{
    public class DatosPersonaPlaModel
    {
        public string TipoDocumento { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string NombreCompleto { get; set; }
        public string FechaNacimiento { get; set; }
        public string Profesion { get; set; }
        public string Genero { get; set; }
        public string Pep { get; set; }
        public string ListaNegra { get; set; }
        public string Mensaje { get; set; }
        public int IndLista { get; set; }
        public int IndAutorizacion { get; set; }
    }
}
