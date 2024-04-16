using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Ef.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<Flight> Flights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //relations
        }
    }
}
