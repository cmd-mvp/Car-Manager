using System.ComponentModel.DataAnnotations;

namespace Car_Manager.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        [Range(2003,2025)]
        public int Year { get; set; }
        public bool Available { get; set; } 
    }
}
