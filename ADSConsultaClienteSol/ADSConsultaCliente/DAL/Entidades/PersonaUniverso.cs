using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConsultaCliente.DAL.Entidades
{
    [Table("MTR_PERSONA_UNIVERSO")]
    public class PersonaUniverso
    {
        #region Datos personales
        [Key]
        [JsonProperty("Identificacion")]
        public string MTR_IDENTIFICACION { get; set; }
        [JsonProperty("Rup")]
        public string MTR_RUP { get; set; }
        [JsonProperty("TipoIdentificacion")]
        public string MTR_TIPO_IDENTIFICACION { get; set; }
        [JsonProperty("TipoPersona")]
        public string MTR_TIPO_PERSONA { get; set; }
        [JsonProperty("NombreCompleto")]
        public string MTR_NOMBRE_COMPLETO { get; set; }
        [JsonProperty("PrimerNombre")]
        public string MTR_PRIMER_NOMBRE { get; set; }
        [JsonProperty("SegundoNombre")]
        public string MTR_SEGUNDO_NOMBRE { get; set; }
        [JsonProperty("PrimerApellido")]
        public string MTR_PRIMER_APELLIDO { get; set; }
        [JsonProperty("SegundoApellido")]
        public string MTR_SEGUNDO_APELLIDO { get; set; }
        [JsonProperty("CodNacionalidad")]
        public string MTR_COD_NACIONALIDAD { get; set; }
        [JsonProperty("Nacionalidad")]
        public string MTR_NACIONALIDAD { get; set; }
        [JsonProperty("Genero")]
        public string MTR_GENERO { get; set; }
        [JsonProperty("Discapacidad")]
        public string MTR_DISCAPACIDAD { get; set; }
        [JsonProperty("CargasFamiliares")]
        public int? MTR_CARGAS_FAMILIARES { get; set; }
        [JsonProperty("NivvelEstudios")]
        public string MTR_NIVEL_ESTUDIOS { get; set; }
        [JsonProperty("EstadoCivil")]
        public string MTR_ESTADO_CIVIL { get; set; }
        [JsonProperty("EsPolitico")]
        public string MTR_ES_POLITICO { get; set; }
        [JsonProperty("Profesion")]
        public string MTR_PROFESION { get; set; }
        [JsonProperty("FecMatrimonio")]
        public DateTime? MTR_FEC_MATRIMONIO { get; set; }
        [JsonProperty("FecNacimineto")]
        public DateTime? MTR_FEC_NACIMIENTO { get; set; }
        [JsonProperty("FecDefuncion")]
        public DateTime? MTR_FEC_DEFUNCION { get; set; }
        [JsonProperty("NumHijos")]
        public decimal? MTR_NUM_HIJOS { get; set; }
        [JsonProperty("ArchivoDigital")]
        public string MTR_ARCHIVO_DIGITAL { get; set; }
        #endregion

        #region Direccion 
        [JsonProperty("DireccionDomicilio")]
        public string MTR_DIRECCION_DOMICILIO { get; set; }
        [JsonProperty("CallePrincDom")]
        public string MTR_CALLE_PRINC_DOM { get; set; }
        [JsonProperty("NumeroDom")]
        public string MTR_NUMERO_DOM { get; set; }
        [JsonProperty("CalleSecunDom")]
        public string MTR_CALLE_SECUN_DOM { get; set; }
        [JsonProperty("DireccionOficina")]
        public string MTR_DIRECCION_OFICINA { get; set; }
        [JsonProperty("CallePrincOfi")]
        public string MTR_CALLE_PRINC_OFI { get; set; }
        [JsonProperty("NumeroOfi")]
        public string MTR_NUMERO_OFI { get; set; }
        [JsonProperty("CalleSecunOfi")]
        public string MTR_CALLE_SECUN_OFI { get; set; }
        [JsonProperty("DireccionOtra")]
        public string MTR_DIRECCION_OTRA { get; set; }
        [JsonProperty("CallePrincOtr")]
        public string MTR_CALLE_PRINC_OTR { get; set; }
        [JsonProperty("NumeroOtr")]
        public string MTR_NUMERO_OTR { get; set; }
        [JsonProperty("CalleSecunOtr")]
        public string MTR_CALLE_SECUN_OTR { get; set; }
        [JsonProperty("SectorDom")]
        public string MTR_SECTOR_DOM { get; set; }
        [JsonProperty("SectorOfi")]
        public string MTR_SECTOR_OFI { get; set; }
        [JsonProperty("SectorOtr")]
        public string MTR_SECTOR_OTR { get; set; }
        [JsonProperty("TelefonoDomicilio")]
        public string MTR_TELEFONO_DOMICILIO { get; set; }
        [JsonProperty("TelefonoCelular")]
        public string MTR_TELEFONO_CELULAR { get; set; }
        [JsonProperty("TelefonoOtro")]
        public string MTR_TELEFONO_OTRO { get; set; }
        [JsonProperty("Operadora")]
        public string MTR_OPERADORA { get; set; }
        [JsonProperty("EmailPersonal")]
        public string MTR_EMAIL_PERSONAL { get; set; }
        [JsonProperty("EmailOficina")]
        public string MTR_EMAIL_OFICINA { get; set; }
        [JsonProperty("EmailFacturacion")]
        public string MTR_EMAIL_FACTURACION { get; set; }
        #endregion

        #region Localizacion
        [JsonProperty("Pais")]
        public string MTR_PAIS { get; set; }
        [JsonProperty("CodPais")]
        public string MTR_COD_PAIS { get; set; }
        [JsonProperty("Provinicia")]
        public string MTR_PROVINCIA { get; set; }
        [JsonProperty("CodProvincia")]
        public string MTR_COD_PROVINCIA { get; set; }
        [JsonProperty("Canton")]
        public string MTR_CANTON { get; set; }
        [JsonProperty("CodCanton")]
        public string MTR_COD_CANTON { get; set; }
        [JsonProperty("Ciudad")]
        public string MTR_CIUDAD { get; set; }
        [JsonProperty("Parroquia")]
        public string MTR_PARROQUIA { get; set; }
        [JsonProperty("Barrio")]
        public string MTR_BARRIO { get; set; }
        [JsonProperty("CodPostal")]
        public string MTR_COD_POSTAL { get; set; }
        [JsonProperty("Latitud")]
        public string MTR_LATITUD { get; set; }
        [JsonProperty("Longitud")]
        public string MTR_LONGITUD { get; set; }
        #endregion

        #region Redes sociales
        [JsonProperty("Skype")]
        public string MTR_SKYPE { get; set; }
        [JsonProperty("Facebook")]
        public string MTR_FACEBOOK { get; set; }
        [JsonProperty("Twitter")]
        public string MTR_TWITTER { get; set; }
        [JsonProperty("Linkedin")]
        public string MTR_LINKEDIN { get; set; }
        [JsonProperty("Instragram")]
        public string MTR_INSTAGRAM { get; set; }
        [JsonProperty("Web")]
        public string MTR_WEB { get; set; }
        #endregion

        #region Tipo de persona
        [JsonProperty("EsSolicitante")]
        public string MTR_ES_SOLICITANTE { get; set; }
        [JsonProperty("EsAsegurado")]
        public string MTR_ES_ASEGURADO { get; set; }
        [JsonProperty("EsBeneficiario")]
        public string MTR_ES_BENEFICIARIO { get; set; }
        [JsonProperty("EsAps")]
        public string MTR_ES_APS { get; set; }
        [JsonProperty("EsCanal")]
        public string MTR_ES_CANAL { get; set; }
        [JsonProperty("EsTaller")]
        public string MTR_ES_TALLER { get; set; }
        [JsonProperty("EsPerito")]
        public string MTR_ES_PERITO { get; set; }
        [JsonProperty("EsProveedor")]
        public string MTR_ES_PROVEEDOR { get; set; }
        [JsonProperty("EsContratista")]
        public string MTR_ES_CONTRATISTA { get; set; }
        [JsonProperty("EsGarante")]
        public string MTR_ES_GARANTE { get; set; }
        [JsonProperty("EsEmpleado")]
        public string MTR_ES_EMPLEADO { get; set; }
        [JsonProperty("EsRefComercial")]
        public string MTR_ES_REF_COMERCIAL { get; set; }
        [JsonProperty("EsRefPersonal")]
        public string MTR_ES_REF_PERSONAL { get; set; }
        [JsonProperty("EsEntFinanciera")]
        public string MTR_ES_ENT_FINANCIERA { get; set; }
        [JsonProperty("EsEntControl")]
        public string MTR_ES_ENT_CONTROL { get; set; }
        [JsonProperty("EsTipoProveedor")]
        public string MTR_TIPO_PROVEEDOR { get; set; }
        #endregion

        #region Informacion Tributaria
        [JsonProperty("RucNatural")]
        public string MTR_RUC_NATURAL { get; set; }
        [JsonProperty("RazonSocial")]
        public string MTR_RAZON_SOCIAL { get; set; }
        [JsonProperty("NomComercial")]
        public string MTR_NOM_COMERCIAL { get; set; }
        [JsonProperty("ClasifCia")]
        public string MTR_CLASIF_CIA { get; set; }
        [JsonProperty("FecInscRuc")]
        public DateTime? MTR_FEC_INSC_RUC { get; set; }
        [JsonProperty("FecIniAct")]
        public DateTime? MTR_FEC_INI_ACT { get; set; }
        [JsonProperty("ObligadoCont")]
        public string MTR_OBLIGADO_CONT { get; set; }
        [JsonProperty("TipoContrib")]
        public string MTR_TIPO_CONTRIB { get; set; }
        [JsonProperty("EstadoCia")]
        public string MTR_ESTADO_CIA { get; set; }
        [JsonProperty("FecConstitucion")]
        public DateTime? MTR_FEC_CONSTITUCION { get; set; }
        #endregion

        #region Relaciones
        [JsonProperty("IdConyuge")]
        public string MTR_ID_CONYUGE { get; set; }
        [JsonProperty("NombreConyuge")]
        public string MTR_NOMBRE_CONYUGE { get; set; }
        [JsonProperty("IdPadre")]
        public string MTR_ID_PADRE { get; set; }
        [JsonProperty("NombrePadre")]
        public string MTR_NOMBRE_PADRE { get; set; }
        [JsonProperty("IdMadre")]
        public string MTR_ID_MADRE { get; set; }
        [JsonProperty("NombreMadre")]
        public string MTR_NOMBRE_MADRE { get; set; }
        [JsonProperty("NombreRef")]
        public string MTR_NOMBRE_REF { get; set; }
        [JsonProperty("ParenrezcoRef")]
        public string MTR_PARENTESCO_REF { get; set; }
        [JsonProperty("IdRepLegal")]
        public string MTR_ID_REP_LEGAL { get; set; }
        [JsonProperty("NomRepLegal")]
        public string MTR_NOM_REP_LEGAL { get; set; }
        [JsonProperty("IdGerenteGnral")]
        public string MTR_ID_GERENTE_GNRAL { get; set; }
        [JsonProperty("NomGerenteGnral")]
        public string MTR_NOM_GERENTE_GNRAL { get; set; }
        [JsonProperty("IdEmpresa")]
        public string MTR_ID_EMPRESA { get; set; }
        [JsonProperty("NombreEmpresa")]
        public string MTR_NOMBRE_EMPRESA { get; set; }
        #endregion

        #region Situacion Laboral
        [JsonProperty("Ciiu")]
        public string MTR_CIIU { get; set; }
        [JsonProperty("CiiuDescripcion")]
        public string MTR_CIIU_DESCRIPCION { get; set; }
        [JsonProperty("ActEconomica")]
        public string MTR_ACT_ECONOMICA { get; set; }
        [JsonProperty("DescActEconomica")]
        public string MTR_DESC_ACT_ECONOMICA { get; set; }
        [JsonProperty("Cargo")]
        public string MTR_CARGO { get; set; }
        [JsonProperty("AntigTrabajo")]
        public decimal? MTR_ANTIG_TRABAJO { get; set; }
        [JsonProperty("SucEmpAsur")]
        public string MTR_SUC_EMP_ASUR { get; set; }
        #endregion

        #region Datos financieros
        [JsonProperty("SueldoPropio")]
        public decimal? MTR_SUELDO_PROPIO { get; set; }
        [JsonProperty("Ingresos")]
        public decimal? MTR_INGRESOS { get; set; }
        [JsonProperty("OtrosIngresos")]
        public decimal? MTR_OTROS_INGRESOS { get; set; }
        [JsonProperty("FuenteOtrosIng")]
        public string MTR_FUENTE_OTROS_ING { get; set; }
        [JsonProperty("GastosMens")]
        public decimal? MTR_GASTOS_MENS { get; set; }
        [JsonProperty("Activos")]
        public decimal? MTR_ACTIVOS { get; set; }
        [JsonProperty("Pasivos")]
        public decimal? MTR_PASIVOS { get; set; }
        [JsonProperty("Patrimonio")]
        public decimal? MTR_PATRIMONIO { get; set; }
        [JsonProperty("Utilidad")]
        public decimal? MTR_UTILIDAD { get; set; }
        [JsonProperty("NumVehiculos")]
        public decimal? MTR_NUM_VEHICULOS { get; set; }
        [JsonProperty("NumEmpleados")]
        public decimal? MTR_NUM_EMPLEADOS { get; set; }
        [JsonProperty("NumPropiedades")]
        public decimal? MTR_NUM_PROPIEDADES { get; set; }
        [JsonProperty("TarjetaCred")]
        public string MTR_TARJETA_CRED { get; set; }
        [JsonProperty("Bancarizado")]
        public string MTR_BANCARIZADO { get; set; }
        #endregion

        #region Calificacion
        [JsonProperty("CalifEquifax")]
        public string MTR_CALIF_EQUIFAX { get; set; }
        [JsonProperty("ScoreFianzas")]
        public string MTR_SCORE_FIANZAS { get; set; }
        [JsonProperty("PlaLista")]
        public string MTR_PLA_LISTA { get; set; }
        #endregion

        #region Auditoria
        [JsonProperty("FecCreacion")]
        public DateTime? MTR_FEC_CREACION { get; set; }
        [JsonProperty("FecUltAct")]
        public DateTime? MTR_FEC_ULT_ACT { get; set; }
        [JsonProperty("Estado")]
        public string MTR_ESTADO { get; set; }
        [JsonProperty("FecFormularioVinc")]
        public DateTime? MTR_FEC_FORMULARIO_VINC { get; set; }
        #endregion

        #region Datos de ejecucion
        //[JsonProperty("Sistema")]
        //public string MTR_SISTEMA { get; set; }
        //[JsonProperty("Validacion")]
        //public string MTR_VALIDACION { get; set; }
        //[JsonProperty("Sucursal")]
        //public string MTR_SUCURSAL { get; set; }
        //[JsonProperty("Codigo")]
        //public int MTR_CODIGO { get; set; }
        //[JsonProperty("SistemaPrioridad")]
        //public string MTR_SISTEMA_PRIORIDAD { get; set; }
        //[JsonProperty("Ocurrencias")]
        //public int MTR_OCURRENCIAS { get; set; }
        //[JsonProperty("IdentificacionReal")]
        //public string MTR_IDENTIFICACION_REAL { get; set; }
        //[JsonProperty("AuditFecha")]        
        //public DateTime AUDIT_FECHA { get; set; }
        //[JsonProperty("AuditUsuario")]
        //public string AUDIT_USUARIO { get; set; }
        //[JsonProperty("Novedad")]
        //public string MTR_NOVEDAD { get; set; }
        #endregion
    }
}
