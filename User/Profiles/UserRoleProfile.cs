using AutoMapper;
using UserMicroservice.Entites;
using UserMicroservice.Models.DTO;

namespace UserMicroservice.Profiles
{
    public class UserRoleProfile : Profile
    {  
        public UserRoleProfile() {
        
            CreateMap<UserRole, UserRoleDto>().ReverseMap();
            CreateMap<UserRoleCreationDto, UserRole>().ReverseMap();
            CreateMap<UserRoleUpdateDto, UserRole>().ReverseMap();

        }
    }
}
