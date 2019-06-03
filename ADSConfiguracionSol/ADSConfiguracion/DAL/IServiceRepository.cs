using ADSConfiguracion.DAL.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConfiguracion.DAL
{
    public interface IServiceRepository: IDisposable
    {        
        Task<Service> GetServiceAsync(string serviceId, string environment, string version);               
        Task AddServiceAsync(Service servicio);
        Task<bool> UpdateService(ObjectId id, string urlActualizacion, string urlVerificacion);

        Task<ICollection<Service>> GetServicesAsync(bool active);
        Task<bool> UnsubscribeServiceAsync(Service servicio);
        Task<bool> UpdateAttemptsAsync(Service service, int attempts);
    }
}
