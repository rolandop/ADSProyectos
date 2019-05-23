using ADSConfiguracion.DAL.Entidades;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConfiguracion.Servicios
{
    public interface IServicioServicio
    {
        Task<ICollection<Servicio>> ObtenerServiciosAsync(bool activo);
        Task<Servicio> ObtenerServicioAsync(string id, string ambiente, string version);
    }


    
}
