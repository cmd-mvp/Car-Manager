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
public class UserController : ControllerBase
{
    private IMapper _mapper;
    private UserContext _context;
    private UserServices _userServices;
    public UserController(IMapper mapper, UserContext context, UserServices userServices)
    {
        _mapper = mapper;
        _context = context;
        _userServices = userServices;
    }
    [HttpPost]
    public async Task<IActionResult> PostUser([FromBody] CreateUserDto dto)
    {
        var check = await _context.Users.FirstOrDefaultAsync(u => u.CPF == dto.CPF);
        if (check is not null) return BadRequest($"CPF: {dto.CPF} Already registered");

        if (!ModelState.IsValid) return BadRequest(ModelState);
        var map = _mapper.Map<User>(dto);
        await _userServices.Post(map);
        return CreatedAtAction(nameof(GetByCPF), new { Id = map.Id, CPF = map.CPF, DateOfBirth = map.DateOfBirth }, map);
    }
    [HttpGet("{CPF}")]
    public async Task<IActionResult> GetByCPF(string CPF)
    {
        var user = _context.Users.FirstOrDefault(c => c.CPF == CPF);
        if (user is null) return NotFound($"User with CPF: {CPF} does not exist!");
        var returnedUser = _userServices.Get(user.UserName);
        if (returnedUser == null) return null;
        var map = _mapper.Map<UserDto>(returnedUser.Result);
        return Ok(map);
    }
    [HttpPut("{CPF}")]
    public async Task<IActionResult> PutCar(string CPF, UserDto dto)
    {
        if (CPF != dto.CPF)
        {
            return BadRequest();
        }
        var user = _context.Users.Where(u => u.CPF == CPF).FirstOrDefault();
        await _userServices.ChangeUser(user!,dto);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(CPF))
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
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string CPF)
    {
        var user = _context.Users.FirstOrDefault(a=>a.CPF == CPF);
        if (user == null) return NotFound();

        await _userServices.DeleteUser(user);
        return NoContent();
    }
    private bool UserExists(string CPF)
    {
         return _context.Users.Any(a => a.CPF == CPF);
    }
}