using Business.Generic.Interface;
using Core.Models;
using Core.ViewModels;

namespace Business.Interface
{
    public interface IFlightManager : ITManager<Flight, FlightViewModel>
    {
    }
}
