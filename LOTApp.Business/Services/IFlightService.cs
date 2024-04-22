using LOTApp.Business.Common;
using LOTApp.Core.DTOs;
using LOTApp.Core.Models;
using LOTApp.Core.ViewModels;

namespace LOTApp.Business.Services
{
    public interface IFlightService : ITManager<Flight, FlightViewModel, CreateFlightDTO, int>
    {
        IEnumerable<FlightViewModel> Get(int? id, string? flightNumber,
            DateTime? departTimeFrom, DateTime? departTimeTo,
            string? departLocation,
            string? arrivalLocation,
            PlaneType? planeType);
    }
}
