using ADSRegistroCivil.DAL.Context;
using ADSRegistroCivil.Domain.Enums;
using ADSRegistroCivil.Domain.Model;
using ADSRegistroCivil.Domain.Siaerp;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSRegistroCivil.Service
{

    public class PersonaLogService
    {

        public PersonaLogService()
        {
            Log = new PersonaLogModel();
        }

        private PersonaLogModel Log { get; set; }

        public int IdUsuario
        {
            get { return Log.IdUsuario; }
            set { Log.IdUsuario = value; }
        }
        public string Usuario
        {
            get { return Log.Usuario; }
            set { Log.Usuario = value; }
        }
        public RolBusqueda Rol
        {
            get { return Log.Rol; }
            set { Log.Rol = value; }
        }
        public string Aplicacion
        {
            get { return Log.Aplicacion; }
            set { Log.Aplicacion = value; }
        }

        public string UrlLlamada
        {
            get { return Log.UrlLlamada; }
            set { Log.UrlLlamada = value; }
        }
        public string NombreCompletoUsuario
        {
            get { return Log.NombreCompletoUsuario; }
            set { Log.NombreCompletoUsuario = value; }
        }
        public string CedulaConsultada
        {
            get { return Log.CedulaConsultada; }
            set { Log.CedulaConsultada = value; }
        }

        public void CrearLog(OrigenBusqueda origen, string respuesta, AccionConsulta accion, string con)
        {
            Log.Origen = origen;
            Log.Respuesta = respuesta;
            Log.Accion = accion;
            Log.Aplicacion = "SERV_REST";
            Log.IdUsuario = 15663;
            Log.Usuario = "DATOSPERSONAWS";
            Log.Rol = 0;
            
            CrearLog(con);
        }
        public void CrearLog(string respuesta, AccionConsulta accion, string con)
        {
            Log.Origen = OrigenBusqueda.None;
            Log.Respuesta = respuesta;
            Log.Accion = accion;
            CrearLog(con);
        }

        public void CrearLog(PersonaLogModel log, string con)
        {
            Log = log;
            CrearLog(con);
        }
        public void CrearLog(string con)
        {
            try
            {
                var db = new ApplicationContext();

                Log.BasePropia = Log.Origen == OrigenBusqueda.BasePropia ? 1 : 0;
                Log.RegistroCivil = Log.Origen == OrigenBusqueda.RegistroCivil ? 1 : 0;
                Log.Fecha = DateTime.Now;
                var model = new ConsultaPersonaLog
                {
                    IdConsultaPersonaLog = 0,
                    IdUsuario = Log.IdUsuario,
                    Usuario = Log.Usuario,
                    Rol = (int)Log.Rol,
                    Origen = (int)Log.Origen,
                    Fecha = Log.Fecha,
                    Aplicacion = Log.Aplicacion,
                    UrlLlamada = Log.UrlLlamada,
                    Respuesta = Log.Respuesta,
                    Accion = (int)Log.Accion,
                    BasePropia = Log.BasePropia,
                    RegistroCivil = Log.RegistroCivil,
                    NombreCompletoUsuario = Log.NombreCompletoUsuario,
                    CedulaConsultada = Log.CedulaConsultada
                };

                var flag = AddLog(model, con);
                
                db.ConsultaPersonaLogs.Add(model);
                var x = db.SaveChangesAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        public bool AddLog(ConsultaPersonaLog log, string con)
        {
            try
            {
                using (var cnn = new OracleConnection(con))
                {
                    cnn.Open();

                    var query = @"INSERT INTO 
                                    ""ConsultaPersonaLog"" (""IdUsuario"", ""Usuario"", ""Rol"", ""Origen"", ""Fecha"", ""Aplicacion"", ""UrlLlamada"", ""Respuesta"", ""Accion"", ""BasePropia"", ""RegistroCivil"", ""NombreCompletoUsuario"", ""CedulaConsultada"")
                                                  VALUES (:pIdUsuario, :pUsuario, :pRol, :pOrigen, :pFecha, :pAplicacion, :pUrlLlamada, :pRespuesta, :pAccion, :pBasePropia, :pRegistroCivil, :pNombreCompletoUsuario, :pCedulaConsultada)";

                    var command = new OracleCommand(query, cnn);

                    command.Parameters.Add(new OracleParameter(":pIdUsuario", OracleDbType.Int32) { Value = log.IdUsuario });
                    command.Parameters.Add(new OracleParameter(":pUsuario", OracleDbType.Varchar2) { Value = log.Usuario });
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

                    command.ExecuteNonQuery();
                    command.Dispose();
                    cnn.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
