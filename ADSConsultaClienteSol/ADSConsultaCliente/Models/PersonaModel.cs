using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConsultaCliente.Models
{
    public class PersonaModel
    {
        #region Datos Persnoales
        public string MTR_IDENTIFICACION { get; set; }
        public string MTR_RUP { get; set; }
        public string MTR_TIPO_IDENTIFICACION { get; set; }
        public string MTR_TIPO_PERSONA { get; set; }
        public string MTR_NOMBRE_COMPLETO { get; set; }
        public string MTR_PRIMER_NOMBRE { get; set; }
        public string MTR_SEGUNDO_NOMBRE { get; set; }
        public string MTR_PRIMER_APELLIDO { get; set; }
        public string MTR_SEGUNDO_APELLIDO { get; set; }
        public string MTR_COD_NACIONALIDAD { get; set; }
        public string MTR_NACIONALIDAD { get; set; }
        public string MTR_GENERO { get; set; }
        public string MTR_DISCAPACIDAD { get; set; }
        public int? MTR_CARGAS_FAMILIARES { get; set; }
        public string MTR_NIVEL_ESTUDIOS { get; set; }
        public string MTR_ESTADO_CIVIL { get; set; }
        public string MTR_ES_POLITICO { get; set; }
        public string MTR_PROFESION { get; set; }
        public DateTime? MTR_FEC_MATRIMONIO { get; set; }
        public DateTime? MTR_FEC_NACIMIENTO { get; set; }
        public DateTime? MTR_FEC_DEFUNCION { get; set; }
        public decimal? MTR_NUM_HIJOS { get; set; }
        public string MTR_ARCHIVO_DIGITAL { get; set; }
        #endregion

        #region Direccion 
        public string MTR_DIRECCION_DOMICILIO { get; set; }
        public string MTR_CALLE_PRINC_DOM { get; set; }
        public string MTR_NUMERO_DOM { get; set; }
        public string MTR_CALLE_SECUN_DOM { get; set; }
        public string MTR_DIRECCION_OFICINA { get; set; }
        public string MTR_CALLE_PRINC_OFI { get; set; }
        public string MTR_NUMERO_OFI { get; set; }
        public string MTR_CALLE_SECUN_OFI { get; set; }
        public string MTR_DIRECCION_OTRA { get; set; }
        public string MTR_CALLE_PRINC_OTR { get; set; }
        public string MTR_NUMERO_OTR { get; set; }
        public string MTR_CALLE_SECUN_OTR { get; set; }
        public string MTR_SECTOR_DOM { get; set; }
        public string MTR_SECTOR_OFI { get; set; }
        public string MTR_SECTOR_OTR { get; set; }
        public string MTR_TELEFONO_DOMICILIO { get; set; }
        public string MTR_TELEFONO_CELULAR { get; set; }
        public string MTR_TELEFONO_OTRO { get; set; }
        public string MTR_OPERADORA { get; set; }
        public string MTR_EMAIL_PERSONAL { get; set; }
        public string MTR_EMAIL_OFICINA { get; set; }
        public string MTR_EMAIL_FACTURACION { get; set; }
        #endregion

        #region Localizacion
        public string MTR_PAIS { get; set; }
        public string MTR_COD_PAIS { get; set; }
        public string MTR_PROVINCIA { get; set; }
        public string MTR_COD_PROVINCIA { get; set; }
        public string MTR_CANTON { get; set; }
        public string MTR_COD_CANTON { get; set; }
        public string MTR_CIUDAD { get; set; }
        public string MTR_PARROQUIA { get; set; }
        public string MTR_BARRIO { get; set; }
        public string MTR_COD_POSTAL { get; set; }
        public string MTR_LATITUD { get; set; }
        public string MTR_LONGITUD { get; set; }
        #endregion

        #region Redes sociales
        public string MTR_SKYPE { get; set; }
        public string MTR_FACEBOOK { get; set; }
        public string MTR_TWITTER { get; set; }
        public string MTR_LINKEDIN { get; set; }
        public string MTR_INSTAGRAM { get; set; }
        public string MTR_WEB { get; set; }
        #endregion

        #region Tipo de persona
        public string MTR_ES_SOLICITANTE { get; set; }
        public string MTR_ES_ASEGURADO { get; set; }
        public string MTR_ES_BENEFICIARIO { get; set; }
        public string MTR_ES_APS { get; set; }
        public string MTR_ES_CANAL { get; set; }
        public string MTR_ES_TALLER { get; set; }
        public string MTR_ES_PERITO { get; set; }
        public string MTR_ES_PROVEEDOR { get; set; }
        public string MTR_ES_CONTRATISTA { get; set; }
        public string MTR_ES_GARANTE { get; set; }
        public string MTR_ES_EMPLEADO { get; set; }
        public string MTR_ES_REF_COMERCIAL { get; set; }
        public string MTR_ES_REF_PERSONAL { get; set; }
        public string MTR_ES_ENT_FINANCIERA { get; set; }
        public string MTR_ES_ENT_CONTROL { get; set; }
        public string MTR_TIPO_PROVEEDOR { get; set; }
        #endregion

        #region Informacion Tributaria
        public string MTR_RUC_NATURAL { get; set; }
        public string MTR_RAZON_SOCIAL { get; set; }
        public string MTR_NOM_COMERCIAL { get; set; }
        public string MTR_CLASIF_CIA { get; set; }
        public DateTime? MTR_FEC_INSC_RUC { get; set; }
        public DateTime? MTR_FEC_INI_ACT { get; set; }
        public string MTR_OBLIGADO_CONT { get; set; }
        public string MTR_TIPO_CONTRIB { get; set; }
        public string MTR_ESTADO_CIA { get; set; }
        public DateTime? MTR_FEC_CONSTITUCION { get; set; }
        #endregion

        #region Relaciones
        public string MTR_ID_CONYUGE { get; set; }
        public string MTR_NOMBRE_CONYUGE { get; set; }
        public string MTR_ID_PADRE { get; set; }
        public string MTR_NOMBRE_PADRE { get; set; }
        public string MTR_ID_MADRE { get; set; }
        public string MTR_NOMBRE_MADRE { get; set; }
        public string MTR_NOMBRE_REF { get; set; }
        public string MTR_PARENTESCO_REF { get; set; }
        public string MTR_ID_REP_LEGAL { get; set; }
        public string MTR_NOM_REP_LEGAL { get; set; }
        public string MTR_ID_GERENTE_GNRAL { get; set; }
        public string MTR_NOM_GERENTE_GNRAL { get; set; }
        public string MTR_ID_EMPRESA { get; set; }
        public string MTR_NOMBRE_EMPRESA { get; set; }
        #endregion

        #region Situacion Laboral
        public string MTR_CIIU { get; set; }
        public string MTR_CIIU_DESCRIPCION { get; set; }
        public string MTR_ACT_ECONOMICA { get; set; }
        public string MTR_DESC_ACT_ECONOMICA { get; set; }
        public string MTR_CARGO { get; set; }
        public decimal? MTR_ANTIG_TRABAJO { get; set; }
        public string MTR_SUC_EMP_ASUR { get; set; }
        #endregion

        #region Datos financieros
        public decimal? MTR_SUELDO_PROPIO { get; set; }
        public decimal? MTR_INGRESOS { get; set; }
        public decimal? MTR_OTROS_INGRESOS { get; set; }
        public string MTR_FUENTE_OTROS_ING { get; set; }
        public decimal? MTR_GASTOS_MENS { get; set; }
        public decimal? MTR_ACTIVOS { get; set; }
        public decimal? MTR_PASIVOS { get; set; }
        public decimal? MTR_PATRIMONIO { get; set; }
        public decimal? MTR_UTILIDAD { get; set; }
        public decimal? MTR_NUM_VEHICULOS { get; set; }
        public decimal? MTR_NUM_EMPLEADOS { get; set; }
        public decimal? MTR_NUM_PROPIEDADES { get; set; }
        public string MTR_TARJETA_CRED { get; set; }
        public string MTR_BANCARIZADO { get; set; }
        #endregion

        #region Calificacion
        public string MTR_CALIF_EQUIFAX { get; set; }
        public string MTR_SCORE_FIANZAS { get; set; }
        public string MTR_PLA_LISTA { get; set; }
        #endregion

        #region Auditoria
        public DateTime? MTR_FEC_CREACION { get; set; }
        public DateTime? MTR_FEC_ULT_ACT { get; set; }
        public string MTR_ESTADO { get; set; }
        public DateTime? MTR_FEC_FORMULARIO_VINC { get; set; }
        #endregion

        #region Datos de ejecucion
        //public string MTR_SISTEMA { get; set; }
        //public string MTR_VALIDACION { get; set; }
        //public string MTR_SUCURSAL { get; set; }
        //public int MTR_CODIGO { get; set; }
        //public string MTR_SISTEMA_PRIORIDAD { get; set; }
        //public int MTR_OCURRENCIAS { get; set; }
        //public string MTR_IDENTIFICACION_REAL { get; set; }
        //public DateTime AUDIT_FECHA { get; set; }
        //public string AUDIT_USUARIO { get; set; }
        //public string MTR_NOVEDAD { get; set; }
        #endregion
        
    }
}
