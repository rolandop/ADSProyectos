using ADSConsultaCliente.DAL.Entidades;
using ADSConsultaCliente.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConsultaCliente
{
    /// <summary>
    /// Clase para definir los objetos a mapear
    /// </summary>
    public class MappingObjects : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public MappingObjects()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Persona, PersonaModel>();
            CreateMap<PersonaUniverso, PersonaModel>();
        }
    }
}
