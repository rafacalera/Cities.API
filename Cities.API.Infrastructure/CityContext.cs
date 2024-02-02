using Cities.API.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Cities.API.Infrastructure
{
    public class CityContext : DbContext
    {
        public CityContext(DbContextOptions<CityContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(this.Database.GetConnectionString());
            }
        }

        public DbSet<City> City { get; set; }
    }
}
