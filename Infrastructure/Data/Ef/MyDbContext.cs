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
            modelBuilder
                .Entity<Flight>()
                .Property(b => b.FlightNumber)
                .HasConversion(v => v.ToUpper(), v => v.ToUpper());

            modelBuilder
                .Entity<Flight>()
                .Property(b => b.DepartLocation)
                .HasConversion(v => v.ToUpper(), v => v.ToUpper());

            modelBuilder
                .Entity<Flight>()
                .Property(b => b.ArrivalLocation)
                .HasConversion(v => v.ToUpper(), v => v.ToUpper());
            //relations
        }
    }
}
