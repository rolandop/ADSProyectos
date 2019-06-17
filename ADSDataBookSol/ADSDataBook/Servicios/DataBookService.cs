using ADSConfiguration.Utilities.Services;
using ADSDataBook.DAL.Contexto;
using ADSDataBook.DAL.Entidades.MySql;
using ADSDataBook.DAL.Entidades.Oracle;
using ADSDataBook.DAL.Enums;
using ADSDataBook.DAL.Modelos;
using ADSDataBook.DAL.Repository;
using ADSDataBook.Modelos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Oracle.ManagedDataAccess.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ADSDataBook.Servicios
{
    /// <summary>
    /// Clase de servicio para gestión de MS DataBook
    /// </summary>
    public class DataBookService : IDataBookService
    {

        private readonly IConfigurationService _configurationService;
        private readonly IBaseCambiosRepository _baseCambiosRepository;
        private readonly IConsultaLogRepository _consultaLogRepository;
        private readonly ILogger<DataBookService> _logger;


        /// <summary>
        /// Consturctor Sobre cargado
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="baseCambiosRepository"></param>
        /// <param name="consultaLogRepository"></param>
        /// <param name="configurationService"></param>
        public DataBookService(ILogger<DataBookService> logger,
                            IBaseCambiosRepository baseCambiosRepository,
                            IConsultaLogRepository consultaLogRepository,
                            IConfigurationService configurationService)
        {
            
            _logger = logger;
            
            _baseCambiosRepository = baseCambiosRepository;
            _consultaLogRepository = consultaLogRepository;
            _configurationService = configurationService;


        }
        
        /// <summary>
        /// Metodo de Consulta al servicio externo DataBook
        /// </summary>
        /// <param name="identification"></param>
        /// <returns></returns>
        public BaseCambios ConsultarDataBook(string identification)
        {
            try
            {
                _logger.LogInformation("Invoca Servicio DataBook");
                var urlServicio = _configurationService.GetValue("databook:UrlDataBook");
                var con = _configurationService.GetValue("Global:Services:Siaerp:ConnectionString");
                var url = urlServicio + identification +"&usr=SUR";
                var client = new RestClient(url) { Encoding = Encoding.GetEncoding("iso-8859-1") };
                var request = new RestRequest(Method.GET);
                var response = client.Execute(request);
                _logger.LogInformation("Termina Invocacion Servicio DataBook");
                _logger.LogInformation("Inicio Transformación encoding");
                Encoding encoding = Encoding.GetEncoding("ISO-8859-1");
                var result1 = encoding.GetString(response.RawBytes);
                _logger.LogInformation("Termina Transformación encoding");
                _logger.LogInformation("Invoca Transformación de Objeto");
                var result = MapearEntidad(result1);
                _logger.LogInformation("Termina Invocacion Transformación de Objeto");
                if (result == null)
                {
                    _logger.LogInformation("Transformación con error {@identification}", identification);
                    return null;
                }
                _logger.LogInformation("Transformación Exitosa {@identification}", identification);
                return result;
            }
            catch(Exception)
            {
                _logger.LogError("Transformación con error {@identification}", identification);
                return null;
            }

            
        }
        
        /// <summary>
        /// Metodo para mapeo de Entidad Obtenida en DataBook (XML) a un Objeto
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public BaseCambios MapearEntidad (string xml)
        {
            _logger.LogInformation("Inicia transformación de respuesta DataBook {@xml}", xml);
            try
            {
                #region Civil
                var xDoc = new XmlDocument();
                xDoc.LoadXml(xml);
                var civil = xDoc.GetElementsByTagName("civil");
                var cedula = ((XmlElement)civil[0]).GetElementsByTagName("cedula")[0];
                var primerNombre = ((XmlElement)civil[0]).GetElementsByTagName("primernombre")[0];
                var segundoNombre = ((XmlElement)civil[0]).GetElementsByTagName("segundonombre")[0];
                var primerApellido = ((XmlElement)civil[0]).GetElementsByTagName("primerapellido")[0];
                var segundoApellido = ((XmlElement)civil[0]).GetElementsByTagName("segundoapellido")[0];
                var nombreConyugue = ((XmlElement)civil[0]).GetElementsByTagName("nombreconyuge")[0];
                var nombrePadre = ((XmlElement)civil[0]).GetElementsByTagName("nombrepadre")[0];
                var nombreMadre = ((XmlElement)civil[0]).GetElementsByTagName("nombremadre")[0];
                var profesion = ((XmlElement)civil[0]).GetElementsByTagName("profesion")[0];
                var nacionalidad = ((XmlElement)civil[0]).GetElementsByTagName("nacionalidad")[0];
                var diaNacimiento = ((XmlElement)civil[0]).GetElementsByTagName("dianacimiento")[0];
                var mesNacimiento = ((XmlElement)civil[0]).GetElementsByTagName("mesnacimiento")[0];
                var anioNacimiento = ((XmlElement)civil[0]).GetElementsByTagName("anionacimiento")[0];
                var estadoCivil = ((XmlElement)civil[0]).GetElementsByTagName("estadocivil")[0];
                var genero = ((XmlElement)civil[0]).GetElementsByTagName("genero")[0];
                var diaMatrimonio = ((XmlElement)civil[0]).GetElementsByTagName("diamatrimonio")[0];
                var mesMatrimonio = ((XmlElement)civil[0]).GetElementsByTagName("mesmatrimonio")[0];
                var anioMatrimonio = ((XmlElement)civil[0]).GetElementsByTagName("aniomatrimonio")[0];
                var diaDefuncion = ((XmlElement)civil[0]).GetElementsByTagName("diadefuncion")[0];
                var mesDefuncion = ((XmlElement)civil[0]).GetElementsByTagName("mesdefuncion")[0];
                var anioDefuncion = ((XmlElement)civil[0]).GetElementsByTagName("aniodefuncion")[0];
                #endregion

                #region Actual
                var actual = xDoc.GetElementsByTagName("actual");
                var nombreEmpleador = ((XmlElement)actual[0]).GetElementsByTagName("nombreempleador")[0];
                var rucempleador = ((XmlElement)actual[0]).GetElementsByTagName("rucempleador")[0];
                var direccionempleador = ((XmlElement)actual[0]).GetElementsByTagName("direccionempleador")[0];
                var actividadempleador = ((XmlElement)actual[0]).GetElementsByTagName("actividadempleador")[0];
                var cargo = ((XmlElement)actual[0]).GetElementsByTagName("cargo")[0];
                var salarioactual = ((XmlElement)actual[0]).GetElementsByTagName("salarioactual")[0];
                var fechaentrada = ((XmlElement)actual[0]).GetElementsByTagName("fechaentrada")[0];
                var fechasalida = ((XmlElement)actual[0]).GetElementsByTagName("fechasalida")[0];
                var telefonoEmpleador = ((XmlElement)actual[0]).GetElementsByTagName("telefonoempleador")[0];

                #endregion

                #region PrimerAnterior
                var primero = xDoc.GetElementsByTagName("primeroAnterior");
                var nombreEmpleador1 = ((XmlElement)primero[0]).GetElementsByTagName("nombreempleador")[0];
                var rucempleador1 = ((XmlElement)primero[0]).GetElementsByTagName("rucempleador")[0];
                var direccionempleador1 = ((XmlElement)primero[0]).GetElementsByTagName("direccionempleador")[0];
                var actividadempleador1 = ((XmlElement)primero[0]).GetElementsByTagName("actividadempleador")[0];
                var cargo1 = ((XmlElement)primero[0]).GetElementsByTagName("cargo")[0];
                var salarioactual1 = ((XmlElement)primero[0]).GetElementsByTagName("salarioempleador")[0];
                var fechaentrada1 = ((XmlElement)primero[0]).GetElementsByTagName("fechasalidaempleador")[0];
                var fechasalida1 = ((XmlElement)primero[0]).GetElementsByTagName("fechasalidaempleador")[0];
                #endregion

                #region SegundoAnterior
                var segundo = xDoc.GetElementsByTagName("segundoAnterior");
                var nombreEmpleador2 = ((XmlElement)segundo[0]).GetElementsByTagName("nombreempleador")[0];
                var rucempleador2 = ((XmlElement)segundo[0]).GetElementsByTagName("rucempleador")[0];
                var direccionempleador2 = ((XmlElement)segundo[0]).GetElementsByTagName("direccionempleador")[0];
                var actividadempleador2 = ((XmlElement)segundo[0]).GetElementsByTagName("actividadempleador")[0];
                var cargo2 = ((XmlElement)segundo[0]).GetElementsByTagName("cargo")[0];
                var salarioactual2 = ((XmlElement)segundo[0]).GetElementsByTagName("salarioempleador")[0];
                var fechaentrada2 = ((XmlElement)segundo[0]).GetElementsByTagName("fechasalidaempleador")[0];
                var fechasalida2 = ((XmlElement)segundo[0]).GetElementsByTagName("fechasalidaempleador")[0];
                #endregion

                #region Sri
                var sri = xDoc.GetElementsByTagName("sri");
                var rucpersonanatual = ((XmlElement)sri[0]).GetElementsByTagName("rucpersonanatual")[0];
                var razonsocial = ((XmlElement)sri[0]).GetElementsByTagName("razonsocial")[0];
                var nombrefantasia = ((XmlElement)sri[0]).GetElementsByTagName("nombrefantasia")[0];
                var actividad = ((XmlElement)sri[0]).GetElementsByTagName("actividad")[0];
                var calle = ((XmlElement)sri[0]).GetElementsByTagName("calle")[0];
                var numero = ((XmlElement)sri[0]).GetElementsByTagName("numero")[0];
                var interseccion = ((XmlElement)sri[0]).GetElementsByTagName("interseccion")[0];
                var referencia = ((XmlElement)sri[0]).GetElementsByTagName("referencia")[0];
                var obligado = ((XmlElement)sri[0]).GetElementsByTagName("obligado")[0];
                var fechainicio = ((XmlElement)sri[0]).GetElementsByTagName("fechainicio")[0];
                var suspension = ((XmlElement)sri[0]).GetElementsByTagName("suspension")[0];
                var reinicio = ((XmlElement)sri[0]).GetElementsByTagName("reinicio")[0];
                var telefono = ((XmlElement)sri[0]).GetElementsByTagName("telefono")[0];
                #endregion

                #region Contactabilidad
                var contacto = xDoc.GetElementsByTagName("contactabilidad");
                var tel1 = ((XmlElement)contacto[0]).GetElementsByTagName("tel1")[0];
                var tel2 = ((XmlElement)contacto[0]).GetElementsByTagName("tel2")[0];
                var tel3 = ((XmlElement)contacto[0]).GetElementsByTagName("tel3")[0];
                var tel4 = ((XmlElement)contacto[0]).GetElementsByTagName("tel4")[0];
                var tel5 = ((XmlElement)contacto[0]).GetElementsByTagName("tel5")[0];
                var provincia = ((XmlElement)contacto[0]).GetElementsByTagName("provincia")[0];
                var canton = ((XmlElement)contacto[0]).GetElementsByTagName("canton")[0];
                var parroquia = ((XmlElement)contacto[0]).GetElementsByTagName("parroquia")[0];
                var direccion = ((XmlElement)contacto[0]).GetElementsByTagName("direccion")[0];
                #endregion

                #region Vehicular
                var vehicular = xDoc.GetElementsByTagName("vehicular");
                var placa = ((XmlElement)vehicular[0]).GetElementsByTagName("placa")[0];
                var anio = ((XmlElement)vehicular[0]).GetElementsByTagName("anio")[0];
                var modelocoche = ((XmlElement)vehicular[0]).GetElementsByTagName("modelocoche")[0];
                var cilindraje = ((XmlElement)vehicular[0]).GetElementsByTagName("cilindraje")[0];
                var placa1 = ((XmlElement)vehicular[0]).GetElementsByTagName("placa")[1];
                var anio1 = ((XmlElement)vehicular[0]).GetElementsByTagName("anio")[1];
                var modelocoche1 = ((XmlElement)vehicular[0]).GetElementsByTagName("modelocoche")[1];
                var cilindraje1 = ((XmlElement)vehicular[0]).GetElementsByTagName("cilindraje")[1];
                #endregion

                #region Hijos
                var hijos = xDoc.GetElementsByTagName("hijos");
                var hijo1 = ((XmlElement)hijos[0]).GetElementsByTagName("hijo1")[0];
                var hijo2 = ((XmlElement)hijos[0]).GetElementsByTagName("hijo2")[0];
                var hijo3 = ((XmlElement)hijos[0]).GetElementsByTagName("hijo3")[0];
                var hijo4 = ((XmlElement)hijos[0]).GetElementsByTagName("hijo4")[0];
                var hijo5 = ((XmlElement)hijos[0]).GetElementsByTagName("hijo5")[0];
                var hijo6 = ((XmlElement)hijos[0]).GetElementsByTagName("hijo6")[0];
                var hijo7 = ((XmlElement)hijos[0]).GetElementsByTagName("hijo7")[0];
                var hijo8 = ((XmlElement)hijos[0]).GetElementsByTagName("hijo8")[0];
                #endregion

                #region Conyugue
                var conyugue = xDoc.GetElementsByTagName("conyugue");
                if(conyugue.Count > 0)
                {
                    var conynombre = "";
                    var lista = ((XmlElement)conyugue[0]).GetElementsByTagName("conyugenombre");
                    if (lista.Count > 0)
                    {
                        var conyugenombre = ((XmlElement)conyugue[0]).GetElementsByTagName("conyugenombre")[0];
                        conynombre = conyugenombre.InnerText;
                    }
                    
                }
                #endregion

                #region ConyugueCedula
                var conyugueCe = xDoc.GetElementsByTagName("conyugecedula");
                var conyugeCedula = "";
                if (conyugueCe != null)
                {
                    var conyugecedula = ((XmlElement)conyugueCe[0]).GetElementsByTagName("conyugecedula")[0].InnerText ?? "";
                    conyugeCedula = conyugecedula;
                }
                
                #endregion

                #region Padres
                var padres = xDoc.GetElementsByTagName("padres");
                var nombrepadre = ((XmlElement)padres[0]).GetElementsByTagName("nombrepadre")[0];
                var nombremadre = ((XmlElement)padres[0]).GetElementsByTagName("nombremadre")[0];
                #endregion

                #region CedulasPadres
                var padresCe = xDoc.GetElementsByTagName("cedulaspadres");
                var tipo1 = ((XmlElement)padresCe[0]).GetElementsByTagName("tipo1")[0];
                var cedtipo1 = ((XmlElement)padresCe[0]).GetElementsByTagName("cedtipo1")[0];
                var tipo2 = ((XmlElement)padresCe[0]).GetElementsByTagName("tipo2")[0];
                var cedtipo2 = ((XmlElement)padresCe[0]).GetElementsByTagName("cedtipo2")[0];
                #endregion

                #region Email
                var email = xDoc.GetElementsByTagName("email");
                var correo = "";
                if(email != null)
                {
                    var email1 = ((XmlElement)email[0]).GetElementsByTagName("email")[0];
                    correo = email1.InnerText;
                }
                
                #endregion

                #region Medidor
                var medidor = xDoc.GetElementsByTagName("medidor");
                var cantonMedidor = "";
                if(medidor.Count > 0)
                {
                    var direccionmedidor = ((XmlElement)medidor[0]).GetElementsByTagName("direccionmedidor")[0];
                    var provinciamedidor = ((XmlElement)medidor[0]).GetElementsByTagName("provinciamedidor")[0];
                    var parroquiamedidor = ((XmlElement)medidor[0]).GetElementsByTagName("parroquiamedidor")[0];
                }
                
                #endregion
                var fechaMatrimonio = "";
                if (string.IsNullOrEmpty(diaMatrimonio.InnerText) && string.IsNullOrEmpty(mesMatrimonio.InnerText) && string.IsNullOrEmpty(anioMatrimonio.InnerText))
                {
                    fechaMatrimonio = null;
                }
                else
                {
                    fechaMatrimonio = diaMatrimonio.InnerText + "/" + mesMatrimonio.InnerText + "/" + anioMatrimonio.InnerText;
                }
                var fechaDefuncion = "";
                if (string.IsNullOrEmpty(diaDefuncion.InnerText) && string.IsNullOrEmpty(mesDefuncion.InnerText) && string.IsNullOrEmpty(anioDefuncion.InnerText))
                {
                    fechaDefuncion = null;
                }
                else
                {
                    fechaDefuncion = diaDefuncion.InnerText + "/" + mesDefuncion.InnerText + "/" + anioDefuncion.InnerText;
                }

                decimal antiguedadTrabajo;

                if(string.IsNullOrEmpty(fechasalida.InnerText))
                {
                    var fechaActual = DateTime.Now;
                    var fechaInicioT = Convert.ToDateTime(fechaentrada.InnerText);
                    antiguedadTrabajo = (fechaActual - fechaInicioT).Days;
                }
                else
                {
                    var fechaActual = Convert.ToDateTime(fechasalida.InnerText);
                    var fechaInicioT = Convert.ToDateTime(fechaentrada.InnerText);
                    antiguedadTrabajo = (fechaActual - fechaInicioT).Ticks;
                }

                var estadoCivilDesc = "";
                
                switch(estadoCivil.InnerText)
                {
                    case "1":
                        estadoCivilDesc = "SOLTERO";
                        break;
                    case "2":
                        estadoCivilDesc = "CASADO";
                        break;
                    case "3":
                        estadoCivilDesc = "DIVORCIADO";
                        break;
                    case "4":
                        estadoCivilDesc = "VIUDO";
                        break;
                    case "6":
                        estadoCivilDesc = "UNIO LIBRE";
                        break;
                    default:
                        
                        break;
                }

                var generoDesc = "";

                switch (genero.InnerText)
                {
                    case "1":
                        generoDesc = "MASCULINO";
                        break;
                    case "2":
                        generoDesc = "FEMENINO";
                        break;
                    default:
                        break;
                }

                var numHijos = 0;
                if(!string.IsNullOrEmpty(hijo1.InnerText))
                {
                    numHijos += 1;
                }
                if (!string.IsNullOrEmpty(hijo2.InnerText))
                {
                    numHijos += 1;
                }
                if (!string.IsNullOrEmpty(hijo3.InnerText))
                {
                    numHijos += 1;
                }
                if (!string.IsNullOrEmpty(hijo4.InnerText))
                {
                    numHijos += 1;
                }
                if (!string.IsNullOrEmpty(hijo5.InnerText))
                {
                    numHijos += 1;
                }
                if (!string.IsNullOrEmpty(hijo6.InnerText))
                {
                    numHijos += 1;
                }
                if (!string.IsNullOrEmpty(hijo7.InnerText))
                {
                    numHijos += 1;
                }
                if (!string.IsNullOrEmpty(hijo8.InnerText))
                {
                    numHijos += 1;
                }

                var numVehiculos = 0;
                if (!string.IsNullOrEmpty(placa.InnerText))
                {
                    numVehiculos += 1;
                }
                //if (!string.IsNullOrEmpty(placa1.InnerText))
                //{
                //    numVehiculos += 1;
                //}


                var result = new BaseCambios
                {
                    MTR_IDENTIFICACION = cedula.InnerText,
                    MTR_PRIMER_NOMBRE = primerNombre.InnerText,
                    MTR_SEGUNDO_NOMBRE = segundoNombre.InnerText,
                    MTR_PRIMER_APELLIDO = primerApellido.InnerText,
                    MTR_SEGUNDO_APELLIDO = segundoApellido.InnerText,
                    MTR_NOMBRE_CONYUGE = nombreConyugue.InnerText,
                    MTR_NOMBRE_PADRE = nombrePadre.InnerText,
                    MTR_NOMBRE_MADRE = nombreMadre.InnerText,
                    MTR_COD_NACIONALIDAD = nacionalidad.InnerText,
                    MTR_PROFESION = profesion.InnerText,
                    MTR_NACIONALIDAD = nacionalidad.InnerText,
                    MTR_FEC_NACIMIENTO = Convert.ToDateTime(diaNacimiento.InnerText + "/" + mesNacimiento.InnerText + "/" + anioNacimiento.InnerText),
                    MTR_FEC_MATRIMONIO = Convert.ToDateTime(fechaMatrimonio),
                    MTR_FEC_DEFUNCION = Convert.ToDateTime(fechaDefuncion),
                    MTR_ESTADO_CIVIL = estadoCivilDesc,
                    MTR_GENERO = generoDesc,
                    MTR_NOMBRE_COMPLETO = primerApellido.InnerText + " " + segundoApellido.InnerText + " " + primerNombre.InnerText + " " + segundoNombre.InnerText,
                    MTR_DIRECCION_DOMICILIO = direccion.InnerText ?? direccionempleador.InnerText,
                    MTR_DESC_ACT_ECONOMICA = actividadempleador.InnerText ?? actividadempleador1.InnerText ?? actividadempleador2.InnerText,
                    MTR_ANTIG_TRABAJO = antiguedadTrabajo,
                    MTR_CALLE_PRINC_OFI = calle.InnerText,
                    MTR_CALLE_SECUN_OFI = interseccion.InnerText,
                    MTR_COD_CANTON = canton.InnerText,
                    MTR_CANTON = cantonMedidor,
                    MTR_CARGO = cargo.InnerText ?? cargo1.InnerText ?? cargo2.InnerText,
                    MTR_CIIU = parroquia.InnerText,
                    MTR_DIRECCION_OFICINA = direccionempleador.InnerText ?? direccionempleador1.InnerText ?? direccionempleador2.InnerText,
                    MTR_DIRECCION_OTRA = direccionempleador1.InnerText ?? direccionempleador2.InnerText,
                    MTR_EMAIL_PERSONAL = correo,
                    MTR_ID_CONYUGE = conyugeCedula,
                    MTR_ID_EMPRESA = rucempleador.InnerText ?? rucempleador1.InnerText ?? rucempleador2.InnerText,
                    MTR_INGRESOS = Convert.ToDecimal(salarioactual.InnerText),
                    MTR_NOM_COMERCIAL = nombreEmpleador.InnerText,
                    MTR_NOMBRE_EMPRESA = nombreEmpleador.InnerText,
                    MTR_NUM_HIJOS = numHijos,
                    MTR_NUM_VEHICULOS = numVehiculos,
                    MTR_NUMERO_OFI = telefonoEmpleador.InnerText,
                    MTR_PARROQUIA = parroquia.InnerText,
                    MTR_COD_PROVINCIA = provincia.InnerText,
                    MTR_SECTOR_OFI = referencia.InnerText,
                    MTR_TELEFONO_CELULAR = tel2.InnerText,
                    MTR_TELEFONO_DOMICILIO = tel1.InnerText,
                    MTR_TELEFONO_OTRO = tel3.InnerText,
                    
                    //Valores Fijos
                    MTR_ESTADO = "ACTIVO",
                    MTR_TIPO_IDENTIFICACION = "C",
                    MTR_TIPO_PERSONA = "NATURAL",

                    //Valores Fecha
                    MTR_FEC_CREACION = DateTime.Now,
                    MTR_FEC_ULT_ACT = DateTime.Now,
                


                };
                _logger.LogInformation("Transformación exitosa {@identification}", result.MTR_IDENTIFICACION);

                return result;
                
            }
            catch(Exception e)
            {
                _logger.LogError("Transformación con error");
                return null;
            }
            

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool GuardarBaseIntermedia (BaseCambios model)
        {
            try
            {

                return _baseCambiosRepository.GuardarBaseIntermedia(model);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool GuardarLog (BaseCambios model)
        {
            try
            {
                return _consultaLogRepository.CrearLog(model);
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        

    }
}
