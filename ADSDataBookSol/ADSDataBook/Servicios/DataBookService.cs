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
using System.Threading.Tasks;
using System.Xml;

namespace ADSDataBook.Servicios
{
    /// <summary>
    /// 
    /// </summary>
    public class DataBookService : IDataBookService
    {

        private readonly IConfigurationService _configurationService;
        private readonly IBaseCambiosRepository _baseCambiosRepository;
        private readonly IConsultaLogRepository _consultaLogRepository;
        private readonly ILogger<DataBookService> _logger;


        /// <summary>
        /// 
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
        /// 
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
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
                var response = client.Execute(request);
                _logger.LogInformation("Termina Invocacion Servicio DataBook");
                _logger.LogInformation("Invoca Transformación de Objeto");
                var result = MapearEntidad(response.Content);
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
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public BaseCambios MapearEntidad (string xml)
        {
            _logger.LogInformation("Inicia transformación de respuesta DataBook {@xml}", xml);
            try
            {

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
                    MTR_PROFESION = profesion.InnerText,
                    MTR_NACIONALIDAD = nacionalidad.InnerText,
                    MTR_FEC_NACIMIENTO = Convert.ToDateTime(diaNacimiento.InnerText + "/" + mesNacimiento.InnerText + "/" + anioNacimiento.InnerText),
                    MTR_FEC_MATRIMONIO = Convert.ToDateTime(fechaMatrimonio),
                    MTR_FEC_DEFUNCION = Convert.ToDateTime(fechaDefuncion),
                    MTR_ESTADO_CIVIL = estadoCivil.InnerText,
                    MTR_GENERO = genero.InnerText,
                    MTR_NOMBRE_COMPLETO = primerApellido.InnerText + " " + segundoApellido.InnerText + " " + primerNombre.InnerText + " " + segundoNombre.InnerText,

                    //Sin Datos
                    MTR_ACT_ECONOMICA = null,
                    MTR_ACTIVOS = null,
                    MTR_ARCHIVO_DIGITAL = null,
                    MTR_CALIF_EQUIFAX = null,
                    MTR_CIIU_DESCRIPCION = null,
                    MTR_CIUDAD = null,
                    MTR_CLASIF_CIA = null,
                    MTR_COD_POSTAL = null,
                    MTR_DISCAPACIDAD = null,
                    MTR_EMAIL_FACTURACION = null,
                    MTR_EMAIL_OFICINA = null,
                    MTR_FEC_CONSTITUCION = null,

                    //Valores Fijos
                    MTR_ESTADO = "ACTIVO",
                    MTR_TIPO_IDENTIFICACION = "C",
                    MTR_TIPO_PERSONA = "NATURAL",


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
