using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADSConsultaCliente.DAL.Entidades;
using ADSConsultaCliente.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ADSConsultaCliente.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class ConsultaClienteRepositorio : IConsultaClienteRepositorio
    {
        private readonly ILogger<ConsultaClienteRepositorio> _logger;
        private readonly IMapper _mapper;
        private readonly RuiaContext _contexto = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="settings"></param>
        public ConsultaClienteRepositorio(ILogger<ConsultaClienteRepositorio> logger,
            IMapper mapper,
            RuiaContext settings)
        {
            _logger = logger;
            _mapper = mapper;
            _contexto = settings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databook"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public PersonaModel MapDatabookToModel(ResponseDatabookModel databook, PersonaModel model)
        {
            try
            {
                if (databook != null)
                {
                    model.MTR_ACTIVOS = databook.MTR_ACTIVOS;
                    model.MTR_ACT_ECONOMICA = databook.MTR_ACT_ECONOMICA;
                    model.MTR_ANTIG_TRABAJO = databook.MTR_ANTIG_TRABAJO;
                    model.MTR_ARCHIVO_DIGITAL = databook.MTR_ARCHIVO_DIGITAL;
                    model.MTR_BANCARIZADO = databook.MTR_BANCARIZADO;
                    model.MTR_BARRIO = databook.MTR_BARRIO;
                    model.MTR_CALIF_EQUIFAX = databook.MTR_CALIF_EQUIFAX;
                    model.MTR_CALLE_PRINC_DOM = databook.MTR_CALLE_PRINC_DOM;
                    model.MTR_CALLE_PRINC_OFI = databook.MTR_CALLE_PRINC_OFI;
                    model.MTR_CALLE_PRINC_OTR = databook.MTR_CALLE_PRINC_OTR;
                    model.MTR_CALLE_SECUN_DOM = databook.MTR_CALLE_SECUN_DOM;
                    model.MTR_CALLE_SECUN_OFI = databook.MTR_CALLE_SECUN_OFI;
                    model.MTR_CALLE_SECUN_OTR = databook.MTR_CALLE_SECUN_OTR;
                    model.MTR_CANTON = databook.MTR_CANTON;
                    model.MTR_CARGAS_FAMILIARES = Decimal.ToInt32(databook.MTR_CARGAS_FAMILIARES ?? 0);
                    model.MTR_CARGO = databook.MTR_CARGO;
                    model.MTR_CIIU = databook.MTR_CIIU;
                    model.MTR_CIIU_DESCRIPCION = databook.MTR_CIIU_DESCRIPCION;
                    model.MTR_CIUDAD = databook.MTR_CIUDAD;
                    model.MTR_CLASIF_CIA = databook.MTR_CLASIF_CIA;
                    model.MTR_COD_CANTON = databook.MTR_COD_CANTON;
                    model.MTR_COD_NACIONALIDAD = databook.MTR_COD_NACIONALIDAD;
                    model.MTR_COD_PAIS = databook.MTR_COD_PAIS;
                    model.MTR_COD_POSTAL = databook.MTR_COD_POSTAL;
                    model.MTR_COD_PROVINCIA = databook.MTR_COD_PROVINCIA;
                    model.MTR_DESC_ACT_ECONOMICA = databook.MTR_DESC_ACT_ECONOMICA;
                    model.MTR_DIRECCION_DOMICILIO = databook.MTR_DIRECCION_DOMICILIO;
                    model.MTR_DIRECCION_OFICINA = databook.MTR_DIRECCION_OFICINA;
                    model.MTR_DIRECCION_OTRA = databook.MTR_DIRECCION_OTRA;
                    model.MTR_DISCAPACIDAD = databook.MTR_DISCAPACIDAD;
                    model.MTR_EMAIL_FACTURACION = databook.MTR_EMAIL_FACTURACION;
                    model.MTR_EMAIL_OFICINA = databook.MTR_EMAIL_OFICINA;
                    model.MTR_EMAIL_PERSONAL = databook.MTR_EMAIL_PERSONAL;
                    model.MTR_ESTADO = databook.MTR_ESTADO;
                    model.MTR_ESTADO_CIA = databook.MTR_ESTADO_CIA;
                    model.MTR_ESTADO_CIVIL = databook.MTR_ESTADO_CIVIL;
                    model.MTR_ES_APS = databook.MTR_ES_APS;
                    model.MTR_ES_ASEGURADO = databook.MTR_ES_ASEGURADO;
                    model.MTR_ES_BENEFICIARIO = databook.MTR_ES_BENEFICIARIO;
                    model.MTR_ES_CANAL = databook.MTR_ES_CANAL;
                    model.MTR_ES_CONTRATISTA = databook.MTR_ES_CONTRATISTA;
                    model.MTR_ES_EMPLEADO = databook.MTR_ES_EMPLEADO;
                    model.MTR_ES_ENT_CONTROL = databook.MTR_ES_ENT_CONTROL;
                    model.MTR_ES_ENT_FINANCIERA = databook.MTR_ES_ENT_FINANCIERA;
                    model.MTR_ES_GARANTE = databook.MTR_ES_GARANTE;
                    model.MTR_ES_PERITO = databook.MTR_ES_PERITO;
                    model.MTR_ES_POLITICO = databook.MTR_ES_POLITICO;
                    model.MTR_ES_PROVEEDOR = databook.MTR_ES_PROVEEDOR;
                    model.MTR_ES_REF_COMERCIAL = databook.MTR_ES_REF_COMERCIAL;
                    model.MTR_ES_REF_PERSONAL = databook.MTR_ES_REF_PERSONAL;
                    model.MTR_ES_SOLICITANTE = databook.MTR_ES_SOLICITANTE;
                    model.MTR_ES_TALLER = model.MTR_ES_TALLER;
                    model.MTR_FACEBOOK = databook.MTR_FACEBOOK;
                    model.MTR_FEC_CONSTITUCION = databook.MTR_FEC_CONSTITUCION;
                    model.MTR_FEC_CREACION = databook.MTR_FEC_CREACION;
                    model.MTR_FEC_DEFUNCION = databook.MTR_FEC_DEFUNCION;
                    model.MTR_FEC_FORMULARIO_VINC = databook.MTR_FEC_FORMULARIO_VINC;
                    model.MTR_FEC_INI_ACT = databook.MTR_FEC_INI_ACT;
                    model.MTR_FEC_INSC_RUC = databook.MTR_FEC_INSC_RUC;
                    model.MTR_FEC_MATRIMONIO = databook.MTR_FEC_MATRIMONIO;
                    model.MTR_FEC_NACIMIENTO = databook.MTR_FEC_NACIMIENTO;
                    model.MTR_FEC_ULT_ACT = databook.MTR_FEC_ULT_ACT;
                    model.MTR_FUENTE_OTROS_ING = databook.MTR_FUENTE_OTROS_ING;
                    model.MTR_GASTOS_MENS = databook.MTR_GASTOS_MENS;
                    model.MTR_GENERO = databook.MTR_GENERO;
                    model.MTR_IDENTIFICACION = databook.MTR_IDENTIFICACION;
                    model.MTR_ID_CONYUGE = databook.MTR_ID_CONYUGE;
                    model.MTR_ID_EMPRESA = databook.MTR_ID_EMPRESA;
                    model.MTR_ID_GERENTE_GNRAL = databook.MTR_ID_GERENTE_GNRAL;
                    model.MTR_ID_MADRE = databook.MTR_ID_MADRE;
                    model.MTR_ID_PADRE = databook.MTR_ID_PADRE;
                    model.MTR_ID_REP_LEGAL = databook.MTR_ID_REP_LEGAL;
                    model.MTR_INGRESOS = databook.MTR_INGRESOS;
                    model.MTR_INSTAGRAM = databook.MTR_INSTAGRAM;
                    model.MTR_LATITUD = databook.MTR_LATITUD;
                    model.MTR_LINKEDIN = databook.MTR_LINKEDIN;
                    model.MTR_LONGITUD = databook.MTR_LONGITUD;
                    model.MTR_NACIONALIDAD = databook.MTR_NACIONALIDAD;
                    model.MTR_NIVEL_ESTUDIOS = databook.MTR_NIVEL_ESTUDIOS;
                    model.MTR_NOMBRE_COMPLETO = databook.MTR_NOMBRE_COMPLETO;
                    model.MTR_NOMBRE_CONYUGE = databook.MTR_NOMBRE_CONYUGE;
                    model.MTR_NOMBRE_EMPRESA = databook.MTR_NOMBRE_EMPRESA;
                    model.MTR_NOMBRE_MADRE = databook.MTR_NOMBRE_MADRE;
                    model.MTR_NOMBRE_PADRE = databook.MTR_NOMBRE_PADRE;
                    model.MTR_NOMBRE_REF = databook.MTR_NOMBRE_REF;
                    model.MTR_NOM_COMERCIAL = databook.MTR_NOM_COMERCIAL;
                    model.MTR_NOM_GERENTE_GNRAL = databook.MTR_NOM_GERENTE_GNRAL;
                    model.MTR_NOM_REP_LEGAL = databook.MTR_NOM_REP_LEGAL;
                    model.MTR_NUMERO_DOM = databook.MTR_NUMERO_DOM;
                    model.MTR_NUMERO_OFI = databook.MTR_NUMERO_OFI;
                    model.MTR_NUMERO_OTR = databook.MTR_NUMERO_OTR;
                    model.MTR_NUM_EMPLEADOS = databook.MTR_NUM_EMPLEADOS;
                    model.MTR_NUM_HIJOS = databook.MTR_NUM_HIJOS;
                    model.MTR_NUM_PROPIEDADES = databook.MTR_NUM_PROPIEDADES;
                    model.MTR_NUM_VEHICULOS = databook.MTR_NUM_VEHICULOS;
                    model.MTR_OBLIGADO_CONT = databook.MTR_OBLIGADO_CONT;
                    model.MTR_OPERADORA = databook.MTR_OPERADORA;
                    model.MTR_OTROS_INGRESOS = databook.MTR_OTROS_INGRESOS;
                    model.MTR_PAIS = databook.MTR_PAIS;
                    model.MTR_PARENTESCO_REF = databook.MTR_PARENTESCO_REF;
                    model.MTR_PARROQUIA = databook.MTR_PARROQUIA;
                    model.MTR_PASIVOS = databook.MTR_PASIVOS;
                    model.MTR_PATRIMONIO = databook.MTR_PATRIMONIO;
                    model.MTR_PLA_LISTA = databook.MTR_PLA_LISTA;
                    model.MTR_PRIMER_APELLIDO = databook.MTR_PRIMER_APELLIDO;
                    model.MTR_PRIMER_NOMBRE = databook.MTR_PRIMER_NOMBRE;
                    model.MTR_PROFESION = databook.MTR_PROFESION;
                    model.MTR_PROVINCIA = databook.MTR_PROVINCIA;
                    model.MTR_RAZON_SOCIAL = databook.MTR_RAZON_SOCIAL;
                    model.MTR_RUC_NATURAL = databook.MTR_RUC_NATURAL;
                    model.MTR_RUP = databook.MTR_RUP;
                    model.MTR_SCORE_FIANZAS = databook.MTR_SCORE_FIANZAS;
                    model.MTR_SECTOR_DOM = databook.MTR_SECTOR_DOM;
                    model.MTR_SECTOR_OFI = databook.MTR_SECTOR_OFI;
                    model.MTR_SECTOR_OTR = databook.MTR_SECTOR_OTR;
                    model.MTR_SEGUNDO_APELLIDO = databook.MTR_SEGUNDO_APELLIDO;
                    model.MTR_SEGUNDO_NOMBRE = databook.MTR_SEGUNDO_NOMBRE;
                    model.MTR_SKYPE = databook.MTR_SKYPE;
                    model.MTR_SUC_EMP_ASUR = databook.MTR_SUC_EMP_ASUR;
                    model.MTR_SUELDO_PROPIO = databook.MTR_SUELDO_PROPIO;
                    model.MTR_TARJETA_CRED = databook.MTR_TARJETA_CRED;
                    model.MTR_TELEFONO_CELULAR = databook.MTR_TELEFONO_CELULAR;
                    model.MTR_TELEFONO_DOMICILIO = databook.MTR_TELEFONO_DOMICILIO;
                    model.MTR_TELEFONO_OTRO = databook.MTR_TELEFONO_OTRO;
                    model.MTR_TIPO_CONTRIB = databook.MTR_TIPO_CONTRIB;
                    model.MTR_TIPO_IDENTIFICACION = databook.MTR_TIPO_IDENTIFICACION;
                    model.MTR_TIPO_PERSONA = databook.MTR_TIPO_PERSONA;
                    model.MTR_TIPO_PROVEEDOR = databook.MTR_TIPO_PROVEEDOR;
                    model.MTR_TWITTER = databook.MTR_TWITTER;
                    model.MTR_UTILIDAD = databook.MTR_UTILIDAD;
                    model.MTR_WEB = databook.MTR_WEB;

                }
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado.");
                throw ex;
            }
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="persona"></param>
       /// <param name="model"></param>
       /// <returns></returns>
        public PersonaModel MapPersonaToModel(Persona persona, PersonaModel model)
        {
            try
            {
                model = _mapper.Map<PersonaModel>(persona);
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado.");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registroCivil"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public PersonaModel MapRegitroCivilToModel(ResponseRegistroCivilModel registroCivil, PersonaModel model)
        {
            try
            {
                if (registroCivil != null)
                {
                    model.MTR_IDENTIFICACION = registroCivil.Cedula;
                    model.MTR_PRIMER_NOMBRE = registroCivil.PrimerNombre;
                    model.MTR_SEGUNDO_NOMBRE = registroCivil.SegundoNombre;
                    model.MTR_PRIMER_APELLIDO = registroCivil.PrimerApellido;
                    model.MTR_SEGUNDO_APELLIDO = registroCivil.SegundoApellido;
                    model.MTR_NOMBRE_COMPLETO = registroCivil.Nombre;
                    model.MTR_FEC_NACIMIENTO = string.IsNullOrEmpty(registroCivil.FechaNacimiento) ? default(DateTime) : DateTime.Parse(registroCivil.FechaNacimiento);
                    model.MTR_FEC_MATRIMONIO = string.IsNullOrEmpty(registroCivil.FechaMatrimonio) ? default(DateTime) : DateTime.Parse(registroCivil.FechaMatrimonio);
                    if (string.IsNullOrEmpty(registroCivil.FechaDefuncion))
                    {
                        model.MTR_FEC_DEFUNCION = default(DateTime);
                    }
                    else
                    {
                        registroCivil.FechaDefuncion =registroCivil.FechaDefuncion.Trim();

                        if (DateTime.TryParse(registroCivil.FechaDefuncion, out DateTime fecha))
                        {
                            model.MTR_FEC_DEFUNCION = fecha;
                        } 
                    }
                   
                    model.MTR_FEC_CONSTITUCION = string.IsNullOrEmpty(registroCivil.FechaCedulacion) ? default(DateTime) : DateTime.Parse(registroCivil.FechaCedulacion);
                    model.MTR_FEC_ULT_ACT = string.IsNullOrEmpty(registroCivil.FechaActualizacion) ? default(DateTime) : DateTime.Parse(registroCivil.FechaActualizacion);
                    model.MTR_ESTADO_CIVIL = registroCivil.EstadoCivil;
                    model.MTR_NACIONALIDAD = registroCivil.Nacionalidad;
                    model.MTR_GENERO = registroCivil.Genero;
                    
                    model.MTR_ID_CONYUGE = registroCivil.CIConyuge;
                    model.MTR_NOMBRE_CONYUGE = registroCivil.Conyuge;
                    model.MTR_ID_MADRE = registroCivil.CIMadre;
                    model.MTR_NOMBRE_MADRE = registroCivil.NombreMadre;
                    model.MTR_ID_PADRE = registroCivil.CIPadre;
                    model.MTR_NOMBRE_PADRE = registroCivil.NombrePadre;
                }
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado.");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="universo"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public PersonaModel MapUniversoToModel(PersonaUniverso universo, PersonaModel model)
        {
            try
            {
                model = _mapper.Map<PersonaModel>(universo);
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado.");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identificacion"></param>
        /// <returns></returns>
        public PersonaModel ObtenerPersonaServicioAsync(string identificacion)
        {
            try
            {
                var model = new PersonaModel();
                var persona = _contexto.Personas.FirstOrDefault(x => x.MTR_IDENTIFICACION == identificacion);
                if (persona != null)
                {
                    var result = MapPersonaToModel(persona, model);
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado.");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identificacion"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public PersonaModel ObtenerPersonaUniversoServicioAsync(string identificacion, PersonaModel model)
        {
            try
            {
                var universo = _contexto.PersonasUniverso.FirstOrDefault(x => x.MTR_IDENTIFICACION == identificacion);
                if (universo != null)
                {
                    var result = MapUniversoToModel(universo, model);
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado.");
                throw ex;
            }
        }
    }
}
