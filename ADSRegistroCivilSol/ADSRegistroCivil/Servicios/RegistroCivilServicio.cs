
using ADSConfiguration.Utilities.Services;
using ADSRegistroCivil.DAL;
using ADSRegistroCivil.DAL.Entidades.Ods;
using ADSRegistroCivil.DAL.Entidades.Siaerp;
using ADSRegistroCivil.DAL.Enums;
using ADSRegistroCivil.DAL.Modelos;
using ADSRegistroCivil.DAL.Repositorio;
using ADSRegistroCivil.Modelos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Utilities;

namespace ADSRegistroCivil.Servicios
{
    /// <summary>
    /// Servicio Central del Registro Civil
    /// </summary>
    public class RegistroCivilServicio : IRegistroCivilServicio
    {
        private readonly IConfigurationService _configurationService;
        private readonly IConsultaLogRepositorio _consultaLogRepository;
        private readonly IDatosPersonaRcRepositorio _datosPersonaRcRepository;
        private readonly ILogger<RegistroCivilServicio> _logger;

        /// <summary>
        /// Constructor sobre cargado
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="consultaLogRepository"></param>
        /// <param name="datosPersonaRcRepository"></param>
        /// <param name="configurationService"></param>
        public RegistroCivilServicio(ILogger<RegistroCivilServicio> logger,
                            IConsultaLogRepositorio consultaLogRepository,
                            IDatosPersonaRcRepositorio datosPersonaRcRepository,
                            IConfigurationService configurationService)
        {
            _logger = logger;
            _consultaLogRepository = consultaLogRepository;
            _datosPersonaRcRepository = datosPersonaRcRepository;
            _configurationService = configurationService;

        }

        /// <summary>
        /// Metodo de Consulta al registro civil antiguo
        /// </summary>
        /// <param name="identificacion"></param>
        /// <param name="consulta"></param>
        /// <returns></returns>
        public async Task<ResponsePersonaModel> ConsultaRegistroCivilAsync(string identificacion, string consulta)
        {
            
            if (consulta != "S")
            {
                var registro = BuscarPorIdentificacion(identificacion);
                return registro;
            }
            else
            {
                var reg = await ConsultaAntiguoRCAsync(identificacion);
                return reg;
            }
        }

        /// <summary>
        /// Metodo de Consulta al registro civil Nuevo
        /// </summary>
        /// <param name="identification"></param>
        /// <param name="consulta"></param>
        /// <returns></returns>
        public async Task<ResponsePersonaModel> ConsultaCiudadanoAsync(string identification, string consulta)
        {
            
            if (consulta != "S")
            {
                var tiempo = _configurationService.GetValue("registrocivil:Tiempo");
                var tiempoMax = Convert.ToInt32(tiempo);
                var registro = BuscarPorIdentificacion(identification);
                if(registro != null)
                {
                    var valida = ValidarActualizacion(registro, tiempoMax);
                    if(!valida)
                    {
                        return registro;
                    }
                    var reg = await ConsultaNuevoRCAsync(identification);
                    await ActualizaAsync(identification);
                    return reg;

                }
                else
                {
                    var reg = await ConsultaNuevoRCAsync(identification);
                    //var model = MapeaRCDataCliente(reg);
                    //await GrabaAsync(model);
                    return reg;
                }

                
            }
            else
            {
                var reg = await ConsultaNuevoRCAsync(identification);
                var model = BuscarPorIdentificacion(identification);
                if(model != null)
                {
                    await ActualizaAsync(identification);
                }
                else
                {
                    //var model = MapeaRCDataCliente(reg);
                    //await GrabaAsync(model);
                }

                return reg;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> GuardarLogAsync(ResponsePersonaModel model)
        {
            try
            {
                return await _consultaLogRepository.CrearLogAsync(model);
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identificación"></param>
        /// <returns></returns>
        public ResponsePersonaModel BuscarPorIdentificacion(string identificación)
        {
            try
            {
                var response = _datosPersonaRcRepository.BuscarPorId(identificación);
                var model = new ResponsePersonaModel
                {
                    Cedula = response.IDENTIFICACION,
                    Nombre = response.NOMBRE_COMPLETO,
                    Profesion = response.PROFESION,
                    NombreMadre = response.NOMBRE_MADRE,
                    NombrePadre = response.NOMBRE_PADRE,
                    Conyuge = response.NOMBRE_CONYUGE,
                    EstadoCivil = response.ESTADO_CIVIL,
                    FechaCedulacion = response.FECHA_CEDULACION.ToString() == null ? null : response.FECHA_CEDULACION.ToString("dd/MM/yyyy"),
                    FechaActualizacion = response.FECHA_ACTUALIZACION.ToString() == null ? null : response.FECHA_ACTUALIZACION.ToString("dd/MM/yyyy"),
                    //FechaDefuncion = response.FECHA_DEFUNCION.ToString() == null ? null : response.FECHA_DEFUNCION

                };
                return model;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tiempo"></param>
        /// <returns></returns>
        public bool ValidarActualizacion(ResponsePersonaModel model, int tiempo)
        {
            try
            {
                var fechaActualizacion = Convert.ToDateTime(model.FechaActualizacion);
                var fechaActual = DateTime.Now;
                var dif = fechaActual - fechaActualizacion;
                var dias = dif.Days;
                if(dif.Days > tiempo)
                {
                    return true;
                }
                return false;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identification"></param>
        /// <returns></returns>
        public async Task<ResponsePersonaModel> ConsultaNuevoRCAsync(string identification)
        {
            var nombreCiudadano = "";
            var codInstitucion = _configurationService.GetValue("registrocivil:CredencialesRC:codInstitucion");
            var codAgencia = _configurationService.GetValue("registrocivil:CredencialesRC:codAgencia");
            var newUsuarioRC = _configurationService.GetValue("registrocivil:CredencialesRC:newUsuarioRC");
            var newClaveRC = _configurationService.GetValue("registrocivil:CredencialesRC:newClaveRC");
            var response = new ResponsePersonaModel();
            WSConsultaCiudadano.ConsultaCiudadanoClient service = new WSConsultaCiudadano.ConsultaCiudadanoClient();
            WSConsultaCiudadano.BusquedaPorNuiResponse personaRc = await service.BusquedaPorNuiAsync(codInstitucion, codAgencia, newUsuarioRC, newClaveRC, identification);
            nombreCiudadano = personaRc.@return.Nombre;
            var strNombres = Helper.TransformarNombres(nombreCiudadano, Helper.NombreOrden.PrimeroApellidos);
            response.Acta = personaRc.@return.Acta;
            response.CIConyuge = personaRc.@return.NuiConyuge;
            response.Genero = personaRc.@return.Genero;
            response.Nombre = personaRc.@return.Nombre;
            response.Cedula = personaRc.@return.NUI;
            response.CodigoError = personaRc.@return.CodigoError;
            response.Error = personaRc.@return.Error;
            response.CondicionCedulado = personaRc.@return.CondicionCedulado;
            response.Conyuge = personaRc.@return.Conyuge;
            response.EstadoCivil = personaRc.@return.EstadoCivil;
            response.FechaCedulacion = personaRc.@return.FechaCedulacion;
            response.FechaDefuncion = personaRc.@return.FechaFallecimiento;
            response.FechaNacimiento = personaRc.@return.FechaNacimiento;
            response.FechaMatrimonio = personaRc.@return.FechaMatrimonio;
            response.Nacionalidad = personaRc.@return.Nacionalidad;
            response.Profesion = personaRc.@return.Profesion;
            response.PrimerNombre = strNombres.PrimerNombre;
            response.SegundoNombre = strNombres.SegundoNombre;
            response.PrimerApellido = strNombres.PrimerApellido;
            response.SegundoApellido = strNombres.SegundoApellido;
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identificacion"></param>
        /// <returns></returns>
        public async Task<ResponsePersonaModel> ConsultaAntiguoRCAsync(string identificacion)
        {
            var usuario = _configurationService.GetValue("registrocivil:CredencialesRCAntiguo:usuarioRC");
            var clave = _configurationService.GetValue("registrocivil:CredencialesRCAntiguo:claveRC");
            var nombreCiudadano = "";
            var response = new ResponsePersonaModel();
            WSRegistroCivil.WSRegistroCivilConsultaClient service = new WSRegistroCivil.WSRegistroCivilConsultaClient();
            WSRegistroCivil.BusquedaPorCedulaResponse personaRc = await service.BusquedaPorCedulaAsync(identificacion, usuario, clave);
            //log.CrearLog(OrigenBusqueda.RegistroCivil, personaRc.@return.Error, AccionConsulta.Consulta, con);
            nombreCiudadano = personaRc.@return.Nombre;
            var strNombres = Helper.TransformarNombres(nombreCiudadano, Helper.NombreOrden.PrimeroApellidos);
            response.Acta = personaRc.@return.Acta;
            response.CIConyuge = personaRc.@return.CIConyuge;
            response.Genero = personaRc.@return.Genero;
            response.Nombre = personaRc.@return.Nombre;
            response.Cedula = personaRc.@return.Cedula;
            response.CodigoError = personaRc.@return.CodigoError;
            response.Error = personaRc.@return.Error;
            response.CondicionCedulado = personaRc.@return.CondicionCedulado;
            response.Conyuge = personaRc.@return.Conyuge;
            response.EstadoCivil = personaRc.@return.EstadoCivil;
            response.FechaCedulacion = personaRc.@return.FechaCedulacion;
            response.FechaDefuncion = personaRc.@return.FechaDefuncion;
            response.FechaNacimiento = personaRc.@return.FechaNacimiento;
            response.FechaMatrimonio = personaRc.@return.FechaMatrimonio;
            response.Nacionalidad = personaRc.@return.Nacionalidad;
            response.Profesion = personaRc.@return.Profesion;
            response.PrimerNombre = strNombres.PrimerNombre;
            response.SegundoNombre = strNombres.SegundoNombre;
            response.PrimerApellido = strNombres.PrimerApellido;
            response.SegundoApellido = strNombres.SegundoApellido;
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identification"></param>
        /// <returns></returns>
        public async Task<bool> ActualizaAsync(string identification)
        {
            try
            {
                var model = _datosPersonaRcRepository.BuscarPorId(identification);
                var resul = await _datosPersonaRcRepository.ActualizarRegistroAsync(model);
                return resul;
            }
            catch(Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> GrabaAsync(DataClientesRc model)
        {
            try
            {
                var result = await _datosPersonaRcRepository.InsertarRegistroAsync(model);
                return result;
            }
            catch(Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public DataClientesRc MapeaRCDataCliente (ResponsePersonaModel pm)
        {
            try
            {
                var model = new DataClientesRc
                {
                    
                    
                };
                return model;
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
