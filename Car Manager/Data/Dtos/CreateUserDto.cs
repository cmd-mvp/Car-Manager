using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Car_Manager.Data.Dtos;

public class CreateUserDto
{
    [MaxLength(14)]
    public string CPF { get; set; }

    [StringLength(30)]
    public string UserName { get; set; }
    public DateTime DateOfBirth { get; set; }
}
