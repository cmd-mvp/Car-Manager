using Car_Manager.Models;
using System.ComponentModel.DataAnnotations;

namespace Car_Manager.Data.Dtos
{
    public class RentDto
    {
        [Key]
        public int Id { get; set; }
        public int CarId { get; set; }
        public string UserName { get; set; }
        //public string UsersCPF { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Status { get; set; }
    }
}
