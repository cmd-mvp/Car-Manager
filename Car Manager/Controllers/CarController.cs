using AutoMapper;
using Car_Manager.Data;
using Car_Manager.Data.Dtos;
using Car_Manager.Models;
using Car_Manager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Car_Manager.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class CarController : ControllerBase
{
    private IMapper _mapper;
    private CarContext _context;
    private CarServices _carServices;
    public CarController(IMapper mapper, CarContext context, CarServices carServices)
    {
        _mapper = mapper;
        _context = context;
        _carServices = carServices;
    }
    [HttpPost]
    public async Task<IActionResult> CreateCar([FromBody]CreateCarDto dto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var map = _mapper.Map<Car>(dto);
        await _carServices.Create(map);
        return CreatedAtAction(nameof(GetById), new {Id = map.Id}, map);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var car = await _carServices.GetCarById(id);
        if (car is null) return NotFound("This car does not exist");
        return Ok(car);
    }
    [HttpGet("Avalible")]
    public async Task<IEnumerable<Car>> GetCarAvalible()
    {
        return _context.Cars.Where(c=>c.Available == true).ToList();
    }
    [HttpGet]
    public async Task<IEnumerable<Car>> GetEveryCar()
    {
        return _context.Cars.Take(10).ToList();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCar(long id, CarDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest("The Id number must be the same");
        }
        await _carServices.ChangeCar(dto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
    }
 
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCar(int id)
    {
        var car = _context.Cars.FirstOrDefault(c => c.Id == id);
        if (car is null) return NotFound();
        await _carServices.DropCar(car);
        return NoContent();
     }
    private bool CarExists(long id)
    {
        return _context.Cars.Any(e=>e.Id == id);
    }
}