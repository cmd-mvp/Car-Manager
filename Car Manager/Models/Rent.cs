using Car_Manager.Data.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Manager.Models;

public class Rent
{
    [Key]
    public int Id { get; set; }
    public int CarId { get; set; }
    public string UserName { get; set; }
    public DateTime RentDate { get; set; } 
    public DateTime ReturnDate { get; set; }
    public string Status { get; set; }
}
