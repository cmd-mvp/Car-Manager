using System.ComponentModel.DataAnnotations;

namespace Car_Manager.Data.Dtos
{
    public class CreateRentDto 
    {
        [Key]
        public int Id { get; set; }
        public int CarId { get; set; }
        public string UserName { get; set; }
    }
}