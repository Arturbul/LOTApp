using Core.Authentication;
using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Ef.Data
{
    public class MyDbContext : IdentityDbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
