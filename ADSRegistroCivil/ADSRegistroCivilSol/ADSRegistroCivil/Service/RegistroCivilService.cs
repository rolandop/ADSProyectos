using ADSRegistroCivil.DAL.Context;
using ADSRegistroCivil.Domain.Enums;
using ADSRegistroCivil.Domain.Model;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace ADSRegistroCivil.Service
{
    public class RegistroCivilService
    {
        private readonly ApplicationContext _siaerp;

        private readonly IConfiguration configuration;

        private readonly IMapper _mapper;

        public RegistroCivilService(IConfiguration iConfig, ApplicationContext siaerp)
        {
            configuration = iConfig;
            this._siaerp = siaerp;
        }


        public async Task<ResponsePersonaModel> ConsultaRegistroCivilAsync(string identificacion, string consulta, PersonaLogService log)
        {
            var response = new ResponsePersonaModel();
            if (consulta != "S")
            {
                return null;
            }
            else
            {
                var usuario = configuration.GetSection("RegistroCivilOld").GetSection("usuarioRC").Value;
                var clave = configuration.GetSection("RegistroCivilOld").GetSection("claveRC").Value;
                var con = configuration.GetConnectionString("SiaerpConnection");
                WSRegistroCivil.WSRegistroCivilConsultaClient service = new WSRegistroCivil.WSRegistroCivilConsultaClient();
                WSRegistroCivil.BusquedaPorCedulaResponse personaRc = await service.BusquedaPorCedulaAsync(identificacion, usuario, clave);
                log.CrearLog(OrigenBusqueda.RegistroCivil, personaRc.@return.Error,AccionConsulta.Consulta, con);

                return response;
            }
        }

        public async Task<ResponsePersonaModel> ConsultaCiudadanoAsync (string identification, string consulta, PersonaLogService log)
        {
            var nombreCiudadano = "";
            var response = new ResponsePersonaModel();
            if (consulta != "S")
            {
                return null;
            }
            else
            {
                var codInstitucion = configuration.GetSection("RegistroCivilNew").GetSection("codInstitucion").Value;
                var codAgencia = configuration.GetSection("RegistroCivilNew").GetSection("codAgencia").Value;
                var newUsuarioRC = configuration.GetSection("RegistroCivilNew").GetSection("newUsuarioRC").Value;
                var newClaveRC = configuration.GetSection("RegistroCivilNew").GetSection("newClaveRC").Value;
                var con = configuration.GetConnectionString("SiaerpConnection");

                WSConsultaCiudadano.ConsultaCiudadanoClient service = new WSConsultaCiudadano.ConsultaCiudadanoClient();
                WSConsultaCiudadano.BusquedaPorNuiResponse personaRc = await service.BusquedaPorNuiAsync(codInstitucion, codAgencia, newUsuarioRC, newClaveRC, identification);
                log.CedulaConsultada = personaRc.@return.NUI;
                log.NombreCompletoUsuario = personaRc.@return.Nombre;
                log.CrearLog(OrigenBusqueda.RegistroCivil, personaRc.@return.Error, AccionConsulta.Consulta, con);
                //var model = _mapper.Map<ResponsePersonaModel>(personaRc.@return);
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
        }

    }
}
