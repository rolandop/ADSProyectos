using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConsultaCliente.DAL.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public int IdDepartamento { get; set; }
        public int Profesion { get; set; }
        public int IdCompania { get; set; }
        public int IdPosicion { get; set; }
        public string Identificacion { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Nacionalidad { get; set; }
        public string Sexo { get; set; }
        public string Estado { get; set; }
        public int Hijos { get; set; }
        public string Ocupacion { get; set; }
        public int Edad { get; set; }
        public string NivelEstudio { get; set; }
        public string UsuarioLogin { get; set; }
        public string UsuarioClave { get; set; }
        public int IdGrupoEconomico { get; set; }
        public string RazonSocial { get; set; }
        public string TipoIdentificacion { get; set; }
        public int IdTipoPersona { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public int ProveedorSoat { get; set; }
        public string Web { get; set; }
        public string CodigoPostal { get; set; }
        public int EstadoCivil { get; set; }
        public string ContribuyenteEspecial { get; set; }
        public string LugarNacimiento { get; set; }
        public string SessionId { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public Decimal Sueldo { get; set; }
        public int IdActividadEconomica { get; set; }
        public int IdTipoActividadEconomica { get; set; }
        public int IdCiudadNacimiento { get; set; }
        public int CargasFamiliares { get; set; }
        public string SeparacionBienes { get; set; }
        public string ObjetoSocial { get; set; }
        public string DireccionElectronica { get; set; }
        public string ClaveCaducada { get; set; }
        public DateTime? FechaUltimoCambio { get; set; }
        public string ZohoToken { get; set; }
        public string ZohoUsuarioId { get; set; }
        public string CorisUsuario { get; set; }
        public string CorisClave { get; set; }
        public string EstadoUsuario { get; set; }
        public string CambiarClave { get; set; }
        public int IntentosLogin { get; set; }
    }
}
