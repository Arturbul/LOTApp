using LOTApp.Core.Authentication;
using LOTApp.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LOTApp.DataAccess.Data
{
    public class LOTContext : IdentityDbContext
    {
        public LOTContext(DbContextOptions<LOTContext> options) : base(options) { }

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
