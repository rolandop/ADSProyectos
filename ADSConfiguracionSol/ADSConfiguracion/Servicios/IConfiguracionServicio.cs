using ADSConfiguracion.DAL;
using ADSConfiguracion.DAL.Entidades;
using ADSConfiguracion.Modelos;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConfiguracion.Servicios
{
    public interface IConfiguracionServicio
    {
        Task<object> ObtenerConfiguracionServicio(string id, string ambiente, string version);
        Task Subscribe(SubscribeModelo subscribe);
        Task Clonar(ClonarModelo clonar);
        Task<bool> EliminarTodasConfiguracionesAsync();
        Task AgregarConfiguracionAsync(Configuracion elemento);
    }
}
