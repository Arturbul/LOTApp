﻿using AutoMapper;
using LOTApp.Core.Models;
using LOTApp.Core.ViewModels;
using LOTApp.DataAccess.Repositories;

namespace LOTApp.Business.Services
{
    public class FlightService : IFlightService
    {
        private readonly IMapper _mapper;
        private readonly IFlightRepository _repository;
        public FlightService(IMapper mapper, IFlightRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public IEnumerable<FlightViewModel> Get(
            int? id = null,
            string? flightNumber = null,
            DateTime? departTimeFrom = null,
            DateTime? departTimeTo = null,
            string? departLocation = null,
            string? arrivalLocation = null,
            string? planeType = null
            )
        {
            var query = _repository.AllEntities;

            if (id != null)
            {
                query = query.Where(x => x.Id.Equals(id));
            }

            if (departTimeFrom != null)
            {
                query = query.Where(x => x.DepartTime >= departTimeFrom);
            }

            if (departTimeTo != null)
            {
                query = query.Where(x => x.DepartTime <= departTimeTo);
            }

            if (!string.IsNullOrEmpty(flightNumber))
            {
                query = query.Where(x => x.FlightNumber.Equals(flightNumber));
            }

            if (!string.IsNullOrEmpty(departLocation))
            {
                query = query.Where(x => x.DepartLocation.Equals(departLocation));
            }

            if (!string.IsNullOrEmpty(arrivalLocation))
            {
                query = query.Where(x => x.ArrivalLocation.Equals(arrivalLocation));
            }

            if (planeType != null)
            {
                Enum.TryParse(planeType, true, out PlaneType resultType);
                query = query.Where(x => x.PlaneType.Equals(resultType));
            }

            return _mapper.Map<IEnumerable<FlightViewModel>>(query.ToList());
        }

        public FlightViewModel? GetSingle(int id)
        {
            return _mapper.Map<FlightViewModel>(_repository.AllEntities.SingleOrDefault(x => x.Id == id));
        }
        public async Task<FlightViewModel> Create(Flight flight)
        {
            var result = await _repository.Create(flight);

            return _mapper.Map<FlightViewModel>(result);
        }
        public async Task<FlightViewModel> Update(Flight flight)
        {
            var result = await _repository.Update(flight);

            return _mapper.Map<FlightViewModel>(result);
        }
        public async Task<int> Delete(int id)
        {
            var entity = _repository.AllEntities.SingleOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return 0;
            }
            return await _repository.Delete(entity);
        }
    }
}
