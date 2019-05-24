using ADSRegistroCivil.Domain.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSRegistroCivil.Mapping
{
    public class ObjectMap : Profile
    {
        public ObjectMap()
        {
            CreateMap<WSConsultaCiudadano.ciudadano, ResponsePersonaModel>().ReverseMap();
            CreateMap<WSRegistroCivil.persona, ResponsePersonaModel>();
        }
    }
}
