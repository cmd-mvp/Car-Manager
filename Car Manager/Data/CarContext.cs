using Car_Manager.Models;
using Microsoft.EntityFrameworkCore;

namespace Car_Manager.Data
{
    public class CarContext : DbContext
    {
        public CarContext(DbContextOptions<CarContext> opts) : base(opts)
        {
           

        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Rent> Rents { get; set; }

    }
}