using AutoMapper;
using Car_Manager.Data.Dtos;
using Car_Manager.Models;

namespace Car_Manager.Profiles;

public class RentProfile : Profile
{
    public RentProfile()
    {
        CreateMap<CreateRentDto, Rent>();
        CreateMap<Rent, RentDto>();
        CreateMap<RentDto, Rent>();
    }
}
