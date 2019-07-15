using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConsultaPla.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class DatosPersonaPlaModel
    {
        [JsonProperty("TipoDocumento")]
        public string TipoDocumento { get; set; }
        [JsonProperty("PrimerNombre")]
        public string PrimerNombre { get; set; }
        [JsonProperty("SegundoNombre")]
        public string SegundoNombre { get; set; }
        [JsonProperty("PrimerApellido")]
        public string PrimerApellido { get; set; }
        [JsonProperty("SegundoApellido")]
        public string SegundoApellido { get; set; }
        [JsonProperty("NombreCompleto")]
        public string NombreCompleto { get; set; }
        [JsonProperty("FechaNacimiento")]
        public string FechaNacimiento { get; set; }
        [JsonProperty("Profesion")]
        public string Profesion { get; set; }
        [JsonProperty("Genero")]
        public string Genero { get; set; }
        [JsonProperty("Pep")]
        public string Pep { get; set; }
        [JsonProperty("ListaNegra")]
        public string ListaNegra { get; set; }
        [JsonProperty("Mensaje")]
        public string Mensaje { get; set; }
        [JsonProperty("IndLista")]
        public int IndLista { get; set; }
        [JsonProperty("IndAutorizacion")]
        public int IndAutorizacion { get; set; }
        [NotMapped]
        [JsonProperty("Observacion")]
        public string Observacion { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DatosClienteModel
    {
        public string Identification { get; set; }
        public string Name { get; set; }
        public string App { get; set; }
    }
}
