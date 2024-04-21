using LOTApp.Core.Models;
using LOTApp.DataAccess.Data;

namespace LOTApp.DataAccess.Repositories
{
    public class FlightRepository : EFTRepository<Flight>, IFlightRepository
    {
        public FlightRepository(LOTContext context) : base(context) { }
    }
}
