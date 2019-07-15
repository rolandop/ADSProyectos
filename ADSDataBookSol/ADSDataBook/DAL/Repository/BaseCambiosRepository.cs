
using ADSConfiguration.Utilities.Services;
using ADSDataBook.DAL.Contexto;
//using ADSDataBook.DAL.Entidades.MySql;
using ADSDataBook.DAL.Entidades.Ruia;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSDataBook.DAL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseCambiosRepository : IBaseCambiosRepository
    {
        private readonly ILogger<BaseCambiosRepository> _logger;
        //private readonly MySqlContext _Context;
        private readonly RuiaContext _Context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationService"></param>
        /// <param name="Context"></param>
        /// <param name="logger"></param>
        public BaseCambiosRepository(
                        IConfigurationService configurationService,
                        //MySqlContext Context,
                        RuiaContext Context,
                        ILogger<BaseCambiosRepository> logger)
        {
            _Context = Context;
            _logger = logger;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool GuardarBaseIntermedia(BaseCambios model)
        {
            try
            {
                var person = _Context.BaseCambios.FirstOrDefault(x => x.MTR_IDENTIFICACION == model.MTR_IDENTIFICACION);
                if (person != null)
                {
                    person.MTR_DIRECCION_DOMICILIO = model.MTR_DIRECCION_DOMICILIO;
                    person.MTR_ACT_ECONOMICA = model.MTR_ACT_ECONOMICA;
                    person.MTR_DESC_ACT_ECONOMICA = model.MTR_DESC_ACT_ECONOMICA;
                    person.MTR_ACTIVOS = model.MTR_ACTIVOS;
                    person.MTR_ANTIG_TRABAJO = model.MTR_ANTIG_TRABAJO;
                    person.MTR_ARCHIVO_DIGITAL = model.MTR_ARCHIVO_DIGITAL;
                    person.MTR_CALIF_EQUIFAX = model.MTR_CALIF_EQUIFAX;
                    person.MTR_CALLE_PRINC_DOM = model.MTR_CALLE_PRINC_DOM;
                    person.MTR_CALLE_PRINC_OFI = model.MTR_CALLE_PRINC_OFI;
                    person.MTR_CALLE_PRINC_OTR = model.MTR_CALLE_PRINC_OTR;
                    person.MTR_CALLE_SECUN_DOM = model.MTR_CALLE_SECUN_DOM;
                    person.MTR_CALLE_SECUN_OFI = model.MTR_CALLE_SECUN_OFI;
                    person.MTR_CALLE_SECUN_OTR = model.MTR_CALLE_SECUN_OTR;
                    person.MTR_COD_CANTON = model.MTR_COD_CANTON;
                    person.MTR_CANTON = model.MTR_CANTON;
                    person.MTR_CARGAS_FAMILIARES = model.MTR_CARGAS_FAMILIARES;

                    person.MTR_CARGO = model.MTR_CARGO;
                    person.MTR_CIIU = model.MTR_CIIU;
                    person.MTR_CIIU_DESCRIPCION = model.MTR_CIIU_DESCRIPCION;
                    person.MTR_CIUDAD = model.MTR_CIUDAD;
                    person.MTR_CLASIF_CIA = model.MTR_CLASIF_CIA;
                    person.MTR_COD_CANTON = model.MTR_COD_CANTON;
                    person.MTR_COD_NACIONALIDAD = model.MTR_COD_NACIONALIDAD;
                    person.MTR_COD_PAIS = model.MTR_COD_PAIS;
                    person.MTR_COD_POSTAL = model.MTR_COD_POSTAL;
                    person.MTR_COD_PROVINCIA = model.MTR_COD_PROVINCIA;
                    person.MTR_DESC_ACT_ECONOMICA = model.MTR_DESC_ACT_ECONOMICA;
                    person.MTR_DIRECCION_DOMICILIO = model.MTR_DIRECCION_DOMICILIO;
                    person.MTR_DIRECCION_OFICINA = model.MTR_DIRECCION_OFICINA;
                    person.MTR_DIRECCION_OTRA = model.MTR_DIRECCION_OTRA;
                    person.MTR_DISCAPACIDAD = model.MTR_DISCAPACIDAD;
                    person.MTR_EMAIL_FACTURACION = model.MTR_EMAIL_FACTURACION;
                    person.MTR_EMAIL_OFICINA = model.MTR_EMAIL_OFICINA;
                    person.MTR_EMAIL_PERSONAL = model.MTR_EMAIL_PERSONAL;
                    person.MTR_ESTADO = model.MTR_ESTADO;
                    person.MTR_ESTADO_CIA = model.MTR_ESTADO_CIA;
                    person.MTR_ESTADO_CIVIL = model.MTR_ESTADO_CIVIL;
                    person.MTR_ES_APS = model.MTR_ES_APS;
                    person.MTR_ES_ASEGURADO = model.MTR_ES_ASEGURADO;
                    person.MTR_ES_BENEFICIARIO = model.MTR_ES_BENEFICIARIO;
                    person.MTR_ES_CANAL = model.MTR_ES_CANAL;
                    person.MTR_ES_CONTRATISTA = model.MTR_ES_CONTRATISTA;
                    person.MTR_ES_EMPLEADO = model.MTR_ES_EMPLEADO;
                    person.MTR_ES_ENT_CONTROL = model.MTR_ES_ENT_CONTROL;
                    person.MTR_ES_ENT_FINANCIERA = model.MTR_ES_ENT_FINANCIERA;
                    person.MTR_ES_GARANTE = model.MTR_ES_GARANTE;
                    person.MTR_ES_PERITO = model.MTR_ES_PERITO;
                    person.MTR_ES_POLITICO = model.MTR_ES_PROVEEDOR;
                    person.MTR_ES_REF_COMERCIAL = model.MTR_ES_REF_COMERCIAL;
                    person.MTR_ES_REF_PERSONAL = model.MTR_ES_REF_PERSONAL;
                    person.MTR_ES_SOLICITANTE = model.MTR_ES_SOLICITANTE;
                    person.MTR_ES_TALLER = model.MTR_ES_TALLER;
                    person.MTR_FACEBOOK = model.MTR_FACEBOOK;
                    person.MTR_FEC_CONSTITUCION = model.MTR_FEC_CONSTITUCION;
                    person.MTR_FEC_DEFUNCION = model.MTR_FEC_DEFUNCION;


                    person.MTR_FEC_FORMULARIO_VINC = model.MTR_FEC_FORMULARIO_VINC;
                    person.MTR_FEC_INI_ACT = model.MTR_FEC_INI_ACT;
                    person.MTR_FEC_INSC_RUC = model.MTR_FEC_INSC_RUC;
                    person.MTR_FEC_MATRIMONIO = model.MTR_FEC_MATRIMONIO;
                    var today = DateTime.Today;
                    if(today != person.MTR_FEC_ULT_ACT)
                        person.MTR_FEC_ULT_ACT = today;
                    person.MTR_FUENTE_OTROS_ING = model.MTR_FUENTE_OTROS_ING;
                    person.MTR_GASTOS_MENS = model.MTR_GASTOS_MENS;
                    person.MTR_GENERO = model.MTR_GENERO;
                    person.MTR_ID_CONYUGE = model.MTR_ID_CONYUGE;
                    person.MTR_ID_EMPRESA = model.MTR_ID_EMPRESA;
                    person.MTR_ID_GERENTE_GNRAL = model.MTR_ID_GERENTE_GNRAL;
                    person.MTR_ID_MADRE = model.MTR_ID_MADRE;
                    person.MTR_ID_PADRE = model.MTR_ID_PADRE;
                    person.MTR_NACIONALIDAD = model.MTR_NACIONALIDAD;
                    person.MTR_NOMBRE_COMPLETO = model.MTR_NOMBRE_COMPLETO;
                    person.MTR_NOMBRE_CONYUGE = model.MTR_NOMBRE_CONYUGE;
                    person.MTR_NOMBRE_MADRE = model.MTR_NOMBRE_MADRE;
                    person.MTR_NOMBRE_PADRE = model.MTR_NOMBRE_PADRE;
                    person.MTR_NUM_HIJOS = model.MTR_NUM_HIJOS;
                    person.MTR_NUM_VEHICULOS = model.MTR_NUM_VEHICULOS;
                    person.MTR_OBLIGADO_CONT = model.MTR_OBLIGADO_CONT;
                    person.MTR_TELEFONO_CELULAR = model.MTR_TELEFONO_CELULAR;
                    person.MTR_TELEFONO_DOMICILIO = model.MTR_TELEFONO_DOMICILIO;
                    person.MTR_PRIMER_APELLIDO = model.MTR_PRIMER_APELLIDO;
                    person.MTR_SEGUNDO_APELLIDO = model.MTR_SEGUNDO_APELLIDO;
                    person.MTR_PRIMER_NOMBRE = model.MTR_PRIMER_NOMBRE;
                    person.MTR_SEGUNDO_NOMBRE = model.MTR_SEGUNDO_NOMBRE;
                    person.MTR_NUMERO_OFI = model.MTR_NUMERO_OFI;

                    person.MTR_TELEFONO_DOMICILIO = model.MTR_TELEFONO_DOMICILIO;
                    person.MTR_TELEFONO_CELULAR = model.MTR_TELEFONO_CELULAR;
                    person.MTR_TELEFONO_OTRO = model.MTR_TELEFONO_OTRO;
                    person.MTR_FEC_NACIMIENTO = model.MTR_FEC_NACIMIENTO;
                    person.MTR_COD_PAIS = model.MTR_COD_PAIS;
                    person.MTR_SUELDO_PROPIO = model.MTR_SUELDO_PROPIO;
                    person.MTR_INGRESOS = model.MTR_INGRESOS;

                    _Context.SaveChanges();
                    return true;
                }
                else
                {
                    model.MTR_FEC_CREACION = DateTime.Now;
                    _Context.BaseCambios.Add(model);
                    _Context.SaveChanges();
                    _logger.LogInformation("Cambios grabados. en Base de Cambios");
                    return true;
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error al grabar/actualizar registro en Base de Cambios");
                return false;
            }

        }
    }
}
