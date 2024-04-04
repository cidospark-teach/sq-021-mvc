using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SQ021_First_Web_App.Models.Entity;

namespace SQ021_First_Web_App.Data
{
    public class MyDbContext(DbContextOptions<MyDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<MyClaim> MyClaims { get; set; }

    }
}
