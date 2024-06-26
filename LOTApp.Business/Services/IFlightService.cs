﻿using LOTApp.Business.Common;
using LOTApp.Core.Models;
using LOTApp.Core.ViewModels;

namespace LOTApp.Business.Services
{
    public interface IFlightService : ITManager<Flight, FlightViewModel, int>
    {
        IEnumerable<FlightViewModel> Get(int? id, string? flightNumber,
            DateTime? departTimeFrom, DateTime? departTimeTo,
            string? departLocation,
            string? arrivalLocation,
            string? planeType);
    }
}
