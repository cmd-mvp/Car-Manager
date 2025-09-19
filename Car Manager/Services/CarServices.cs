using Car_Manager.Data;
using Car_Manager.Data.Dtos;
using Car_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Car_Manager.Services;
public class CarServices
{
    private CarContext _context;
    public CarServices(CarContext dbContext)
    {
        _context = dbContext;
    }
    public async Task<ActionResult<Car>> Create(Car car)
    {
        _context.Cars.Add(car);
        await _context.SaveChangesAsync();
        return car;
    }
    public async Task<Car> GetCarById(int id)
    {
        var car = _context.Cars.Where(c=>c.Id==id).FirstOrDefault()!;
        if (car is null) return null;
        return car;
    }
    public async Task<ActionResult<Car>> ChangeCar(CarDto dto)
    {
        var car = _context.Cars.Where(c=>c.Id == dto.Id).FirstOrDefault();
        car.Make = dto.Make;
        car.Model = dto.Model;
        car.LicensePlate = dto.LicensePlate;
        car.Year = dto.Year;
        //car.Available = car.Available;
        return car!;
    }
    public async Task<Car> DropCar(Car car)
    {
        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();
        return car;
    }
}