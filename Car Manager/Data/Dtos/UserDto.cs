 using System.ComponentModel.DataAnnotations;

namespace Car_Manager.Data.Dtos
{
    public class UserDto
    {
        [Key]
        public string CPF { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
