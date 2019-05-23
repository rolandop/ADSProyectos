using ADSConfiguracion.DAL.Entidades;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConfiguracion.DAL
{
    public interface IServicioRepositorio: IDisposable
    {        
        Task<Servicio> ObtenerServicioAsync(string id, string ambiente, string version);               
        Task AgregarServicioAsync(Servicio servicio);
        Task<bool> ActualizarServicio(ObjectId id, string urlActualizacion, string urlVerificacion);

        Task<ICollection<Servicio>> ObtenerServiciosAsync(bool activo);
        Task<bool> DesactivarServicioAsync(Servicio servicio);
        Task<bool> ActualizarIntentosAsync(Servicio servicio, int intentos);
    }
}
