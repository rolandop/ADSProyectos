
using ADSConfiguration.Utilities.Services;
using ADSDataBook.DAL.Contexto;
//using ADSDataBook.DAL.Entidades.MySql;
using ADSDataBook.DAL.Entidades.Ruia;
using ADSDataBook.DAL.Entidades.Oracle;
using ADSDataBook.DAL.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSDataBook.DAL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class ConsultaLogRepository : IConsultaLogRepository
    {
        private readonly IConfiguration _configuration;
        private readonly OracleContext _oracleContext;
        private readonly ILogger<ConsultaLogRepository> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="oracleContext"></param>
        /// <param name="logger"></param>
        public ConsultaLogRepository(
                        IConfiguration configuration,
                        OracleContext oracleContext,
                        ILogger<ConsultaLogRepository> logger)
        {
            _oracleContext = oracleContext;
            _logger = logger;
            _configuration = configuration;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>

        public bool CrearLog(BaseCambios info)
        {
            try
            {
                var connection = _configuration.GetSection("Global:Services:Siaerp:ConnectionString").Value;
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
                    RegistroCivil = 0,
                    NombreCompletoUsuario = info.MTR_NOMBRE_COMPLETO,
                    CedulaConsultada = info.MTR_IDENTIFICACION,
                    DataBook = 1
                };
                var flag = AddLog(model, connection);
                _oracleContext.ConsultaPersonaLogs.Add(model);
                //_oracleContext.SaveChanges();
                return flag;
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

                    //OracleGlobalization info = cnn.GetSessionInfo();
                    //info.TimeZone = "America/Guayaquil";
                    //cnn.SetSessionInfo(info);

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
            catch (Exception)
            {
                return false;
            }
        }


    }
}
