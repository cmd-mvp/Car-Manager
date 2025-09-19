using Car_Manager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Car_Manager.Data
{
    public class UserContext : IdentityDbContext<User>
    {
        public UserContext(DbContextOptions<UserContext> opts) : base(opts)
        {
            
        }
        public DbSet<User> Users {  get; set; }
    }
}