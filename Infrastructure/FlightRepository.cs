using Core.Models;
using DataAccess.Ef.Data;
using DataAccess.Generic.Ef;
using DataAccess.Interface;

namespace DataAccess
{
    public class FlightRepository : EFTRepository<Flight>, IFlightRepository
    {
        public FlightRepository(MyDbContext context) : base(context) { }
    }
}
