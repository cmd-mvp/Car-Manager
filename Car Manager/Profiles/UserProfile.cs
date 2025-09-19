using AutoMapper;
using Car_Manager.Data.Dtos;
using Car_Manager.Models;

namespace Car_Manager.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
