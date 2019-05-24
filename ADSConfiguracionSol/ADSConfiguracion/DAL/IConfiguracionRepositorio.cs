using ADSConfiguracion.DAL.Entidades;
using ADSConfiguracion.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConfiguracion.DAL
{
    public interface IConfiguracionRepositorio
    {
        Task<IEnumerable<Configuracion>> ObtenerTodasConfiguraciones();
        Task<Configuracion> ObtenerConfiguracionAsync(string id);
        Task<Configuracion> ObtenerConfiguracionAsync(string id, string ambiente, string version, string seccion, string clave);
        Task<ICollection<Configuracion>> ObtenerConfiguracionesAsync(string id, string ambiente, string version);
        Task<ICollection<Configuracion>> ObtenerConfiguracionesGlobalesAsync(string ambiente);
        // add new Configuracion document
        Task AgregarConfiguracionAsync(Configuracion elemento);

        // remove a single document / Configuracion
        Task<bool> EliminarConfiguracion(string id);

        // update just a single document / Configuracion
        Task<bool> ActualizarConfiguracion(string id, string seccion, string clave,
                                                string valor, string descripcion);        

        // should be used with high cautious, only in relation with demo setup
        Task<bool> EliminarTodasConfiguracionesAsync();
        
    }
}
