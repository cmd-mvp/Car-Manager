using AutoMapper;
using Car_Manager.Data;
using Car_Manager.Data.Dtos;
using Car_Manager.Models;
using Car_Manager.Services;
using Microsoft.AspNetCore.Mvc;

namespace Car_Manager.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class RentalController : ControllerBase
{
    private IMapper _mapper;
    private CarContext _carContext;
    private RentServices _rentServices;
    public RentalController(IMapper mapper, CarContext context, RentServices rentServices)
    {
        _mapper = mapper;
        _carContext = context;
        _rentServices = rentServices;
    }
    [HttpPost]
    public async Task<IActionResult> PostRent([FromBody] CreateRentDto dto)
    {
        var rent = _mapper.Map<Rent>(dto);
        var createdRent = await _rentServices.PostRent(rent);
        if (createdRent == null) return BadRequest("This car is unavailable or the user does not exist");
        await _carContext.SaveChangesAsync();
        var map = _mapper.Map<RentDto>(rent);
        return CreatedAtAction(nameof(GetRent), new { Id = map.Id }, map);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRent(int id)
    {
        var check = _carContext.Rents.FirstOrDefault(c => c.Id == id);
        if (check is null) return NotFound();
        Rent rentCar = await _rentServices.GetRent(id);
        var map = _mapper.Map<RentDto>(rentCar);
        return Ok(map);
    }
    [HttpGet]
    public async Task<IEnumerable<Rent>> GetByStatus()
    {
        _rentServices.UpdateStatus();
        return _carContext.Rents.ToList();
    }
    [HttpGet("Get By Status")]
    public async Task<IEnumerable<Rent>> GetByStatus(string status)
    {   
        _rentServices.CheckAndUpdateStatus(status);
        return _carContext.Rents.Where(c=>c.Status.Equals(status)).ToList();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRent(int id)
    {
        var check = _carContext.Rents.FirstOrDefault(c => c.Id == id);
        if (check is null) return NotFound();
        await _rentServices.ReturnRent(id);
        return NoContent(); 
    }
}