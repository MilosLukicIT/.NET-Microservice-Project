using AutoMapper;
using UserMicroservice.Entites;
using UserMicroservice.Models.DTO;

namespace UserMicroservice.Profiles
{
    public class UserClassProfile : Profile
    {
        public UserClassProfile() 
        {
            CreateMap<UserClass, UserClassDto>().ReverseMap();
            CreateMap<UserClassCreationDto, UserClass>().ReverseMap();
            CreateMap<UserClassUpdateDto, UserClass>().ReverseMap();
            CreateMap<UserClass, UserClassUpdateDto>().ReverseMap();
            CreateMap<UserClass, UserClassViewDto>().ReverseMap();
        }
    }
}
