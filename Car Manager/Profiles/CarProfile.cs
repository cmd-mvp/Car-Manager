using AutoMapper;
using Car_Manager.Data.Dtos;
using Car_Manager.Models;

namespace Car_Manager.Profiles;

public class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<CreateCarDto, Car>();
        CreateMap<Car, CarDto>();
        CreateMap<CarDto, Car>();
    }
}
