using AutoMapper;
using Car_Manager.Data;
using Car_Manager.Data.Dtos;
using Car_Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Car_Manager.Services;
public class UserServices
{
    private readonly UserContext _userContext;
    private readonly IMapper _mapper;
    public UserServices(UserContext userContext)
    {
        _userContext = userContext;
    }
    public async Task<ActionResult<User>> Post(User user)
    {
        _userContext.Add(user);
        _userContext.SaveChanges();
        return user;
    }
    public async Task<User> Get(string userName)
    {
        var user = _userContext.Users.FirstOrDefault(c => c.UserName == userName);
        if (user is null) return null!;
        return user;
    }
    public async Task<ActionResult<User>> ChangeUser(User user, UserDto dto)
    {
        user.UserName = dto.UserName;
        user.DateOfBirth = dto.DateOfBirth;
        _userContext.Update(user);
        return user;
    }
    public async Task<User> DeleteUser(User user)
    {
        _userContext.Remove(user);
        await _userContext.SaveChangesAsync();
        return user;
    }
}
