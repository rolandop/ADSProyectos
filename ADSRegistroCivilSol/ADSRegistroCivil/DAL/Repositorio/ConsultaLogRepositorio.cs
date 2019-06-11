
using ADSConfiguration.Utilities.Services;
using ADSRegistroCivil.DAL.Contexto;
using ADSRegistroCivil.DAL.Entidades.Siaerp;
using ADSRegistroCivil.DAL.Enums;
using ADSRegistroCivil.Modelos;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSRegistroCivil.DAL.Repositorio
{
    /// <summary>
    /// 
    /// </summary>
    public class ConsultaLogRepositorio : IConsultaLogRepositorio
    {
        private readonly IConfigurationService _configurationService;
        private readonly RegistroCivilContexto _oracleContext;
        private readonly ILogger<ConsultaLogRepositorio> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationService"></param>
        /// <param name="oracleContext"></param>
        /// <param name="logger"></param>
        public ConsultaLogRepositorio(
                        IConfigurationService configurationService,
                        RegistroCivilContexto oracleContext,
                        ILogger<ConsultaLogRepositorio> logger)
        {
            _oracleContext = oracleContext;
            _logger = logger;
            _configurationService = configurationService;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>

        public async Task<bool> CrearLogAsync(ResponsePersonaModel info)
        {
            try
            {
                var connection = _configurationService.GetValue("Global:Services:Siaerp:ConnectionString");
                var model = new ConsultaPersonaLog
                {
                    IdConsultaPersonaLog = 0,
                    IdUsuario = 15663,
                    Usuario = "DATOSPERSONAWS",
                    Rol = (int)RolBusqueda.Normal,
                    Origen = (int)OrigenBusqueda.DataBook,
                    Fecha = DateTime.Now,
                    Aplicacion = "ROLES",
                    UrlLlamada = "",
                    Respuesta = "CONSULTA REALIZADA.",
                    Accion = (int)AccionConsulta.Consulta,
                    BasePropia = 0,
                    RegistroCivil = 1,
                    NombreCompletoUsuario = info.Nombre,
                    CedulaConsultada = info.Cedula,
                    DataBook = 0
                };

                var flag = AddLog(model, connection);

                _oracleContext.ConsultaPersonaLogs.Add(model);
                //await _oracleContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public bool AddLog(ConsultaPersonaLog log, string con)
        {
            try
            {
                using (var cnn = new OracleConnection(con))
                {
                    cnn.Open();
                    var query = @"INSERT INTO 
                                    ""ConsultaPersonaLog"" (""IdUsuario"", ""Usuario"", ""Rol"", ""Origen"", ""Fecha"", ""Aplicacion"", ""UrlLlamada"", ""Respuesta"", ""Accion"", ""BasePropia"", ""RegistroCivil"", ""NombreCompletoUsuario"", ""CedulaConsultada"", ""DataBook"")
                                                  VALUES (:pIdUsuario, :pUsuario, :pRol, :pOrigen, :pFecha, :pAplicacion, :pUrlLlamada, :pRespuesta, :pAccion, :pBasePropia, :pRegistroCivil, :pNombreCompletoUsuario, :pCedulaConsultada, :pDataBook)";

                    var command = new OracleCommand(query, cnn);

                    command.Parameters.Add(new OracleParameter(":pIdUsuario", OracleDbType.Int32) { Value = log.IdUsuario });
                    command.Parameters.Add(new OracleParameter(":pUsuario", OracleDbType.NVarchar2) { Value = log.Usuario });
                    command.Parameters.Add(new OracleParameter(":pRol", OracleDbType.Int32) { Value = log.Rol });
                    command.Parameters.Add(new OracleParameter(":pOrigen", OracleDbType.Int32) { Value = log.Origen });
                    command.Parameters.Add(new OracleParameter(":pFecha", OracleDbType.Date) { Value = log.Fecha });
                    command.Parameters.Add(new OracleParameter(":pAplicacion", OracleDbType.NVarchar2) { Value = log.Aplicacion });
                    command.Parameters.Add(new OracleParameter(":pUrlLlamada", OracleDbType.NVarchar2) { Value = log.UrlLlamada });
                    command.Parameters.Add(new OracleParameter(":pRespuesta", OracleDbType.NVarchar2) { Value = log.Respuesta });
                    command.Parameters.Add(new OracleParameter(":pAccion", OracleDbType.Int32) { Value = log.Accion });
                    command.Parameters.Add(new OracleParameter(":pBasePropia", OracleDbType.Int32) { Value = log.BasePropia });
                    command.Parameters.Add(new OracleParameter(":pRegistroCivil", OracleDbType.Int32) { Value = log.RegistroCivil });
                    command.Parameters.Add(new OracleParameter(":pNombreCompletoUsuario", OracleDbType.NVarchar2) { Value = log.NombreCompletoUsuario });
                    command.Parameters.Add(new OracleParameter(":pCedulaConsultada", OracleDbType.NVarchar2) { Value = log.CedulaConsultada });
                    command.Parameters.Add(new OracleParameter(":pDataBook", OracleDbType.Int32) { Value = log.DataBook });

                    command.ExecuteNonQuery();
                    command.Dispose();
                    cnn.Close();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


    }
}

