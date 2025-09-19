using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Car_Manager.Models
{
    public class User : IdentityUser
    {
        [Key]
        [MaxLength(11, ErrorMessage = "Only 11 numbers will be accepted")]
        public string CPF { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public User() : base()
        {
            
        }
    }
}
