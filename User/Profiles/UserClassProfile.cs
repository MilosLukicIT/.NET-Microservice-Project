using AutoMapper;
using UserMicroservice.Entites;
using UserMicroservice.Entites.Confirmation;
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
            CreateMap<UserClassConfirmation, UserClass>().ReverseMap();
        }
    }
}
