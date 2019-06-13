using ADSConsultaCliente.DAL.Entidades;
using ADSConsultaCliente.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConsultaCliente.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class SiaerpRepositorio : ISiaerpRepositorio
    {
        private readonly ILogger<SiaerpRepositorio> _logger;
        private readonly IMapper _mapper;
        private readonly SiaerpContext _contexto = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="settings"></param>
        public SiaerpRepositorio(ILogger<SiaerpRepositorio> logger,
            IMapper mapper,
            SiaerpContext settings)
        {
            _logger = logger;
            _mapper = mapper;
            _contexto = settings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Usuario MapPersonaModelToUsuario(PersonaModel model)
        {
            try
            {
                var usuario = new Usuario();
                if (model != null)
                {
                    usuario.IdCompania = 23;
                    usuario.Identificacion = model.MTR_IDENTIFICACION;
                    usuario.PrimerNombre = model.MTR_PRIMER_NOMBRE;
                    usuario.SegundoNombre = model.MTR_SEGUNDO_NOMBRE;
                    usuario.PrimerApellido = model.MTR_PRIMER_APELLIDO;
                    usuario.SegundoApellido = model.MTR_SEGUNDO_APELLIDO;
                    usuario.FechaNacimiento = model.MTR_FEC_NACIMIENTO;
                    usuario.Nacionalidad = model.MTR_NACIONALIDAD;
                    usuario.Sexo = "";
                    usuario.Estado = "A";
                    usuario.Hijos = model.MTR_NUM_HIJOS == null ? 0 : Int32.Parse(model.MTR_NUM_HIJOS.ToString());
                    usuario.TipoIdentificacion = model.MTR_TIPO_IDENTIFICACION;
                    usuario.Email1 = model.MTR_EMAIL_PERSONAL;
                    usuario.Web = model.MTR_WEB;
                    usuario.CodigoPostal = model.MTR_COD_POSTAL;
                    usuario.Descripcion = model.MTR_NOMBRE_COMPLETO;
                }
                return usuario;
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
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool SaveSiaerpNuevoUsuario(Usuario usuario)
        {
            try
            {
                if (usuario != null)
                {
                    var user = _contexto.Usuarios.FirstOrDefault(x => x.Identificacion == usuario.Identificacion);
                    if (user == null)
                    {
                        _contexto.Usuarios.Add(usuario);
                        _contexto.SaveChanges();
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado.");
                throw ex;
            }
        }
    }
}
