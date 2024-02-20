using AutoMapper;
using Generalizacija.Dto;
using Generalizacija.Entity;

namespace Generalizacija.Profiles
{
    public class SastanakProfile : Profile
    {
      public SastanakProfile() 
        {
            CreateMap<Sastanak, SastanakDto>().ReverseMap();
            CreateMap<IndividualniSastanak, SastanakDto>().ReverseMap();
            CreateMap<Sastanak, IndividualniSastanak>().ReverseMap();
            CreateMap<TimskiSastanak, Sastanak>().ReverseMap();
            CreateMap<TimskiSastanakDto, SastanakDto>().ReverseMap();
            CreateMap<TimskiSastanak, TimskiSastanakDto>().ReverseMap();
            CreateMap<TimskiSastanak, SastanakInfoDto>().ReverseMap();
            CreateMap<Sastanak, SastanakInfoDto>().ReverseMap();
            CreateMap<IndividualniSastanak, SastanakInfoDto>().ReverseMap();


        }
    }
}
