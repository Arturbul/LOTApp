using FluentValidation;
using LOTApp.Attribiutes;
using LOTApp.Business.Services;
using LOTApp.Core.Authentication;
using LOTApp.Core.DTOs;
using LOTApp.Core.Extensions;
using LOTApp.Core.Models;
using LOTApp.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LOTApp.Controllers
{
    [ApiController]
    [Route("api/flights")]
    public class FlightsController : Controller
    {
        private readonly IFlightService _flightService;
        private readonly IValidator<FlightViewModel> _viewModelValidator;
        private readonly IValidator<CreateFlightDTO> _createFlightValidator;
        public FlightsController(IFlightService flightManager, IValidator<FlightViewModel> viewModelValidator, IValidator<CreateFlightDTO> createFlightValidator)
        {
            _flightService = flightManager;
            _viewModelValidator = viewModelValidator;
            _createFlightValidator = createFlightValidator;
        }

        /// <summary>
        /// This method retrieves flight information based on various search criteria. It allows for anonymous access
        /// </summary>
        /// <param name="id">(int?): An optional integer representing a specific flight ID to search for.</param>
        /// <param name="flightNumber">(string?): An optional string representing a flight number to search for, required IATA (marketing) code ex. LO123</param>
        /// <param name="departTimeFrom"> (DateTime?): An optional DateTime object representing the earliest departure time for flights in the search. ex. "2024-04-23T12:00:00"</param>
        /// <param name="departTimeTo">(DateTime?): An optional DateTime object representing the latest departure time for flights in the search.</param>
        /// <param name="departLocation">(string?): An optional string representing the departure location to search for required IATA Airport Commercial service mark ex. 'KTW'".</param>
        /// <param name="arrivalLocation">(string?): An optional string representing the arrival location to search for required IATA Airport Commercial service mark ex. 'KTW'".</param>
        /// <param name="planeType"> (string?): An optional string representing a specific plane type to search for. ex. 'Boeing', PlaneTypes are defined in PlaneType enum</param>
        /// <remarks>
        /// This example demonstrates a search for flights departing after 2024-04-23 12:00 PM and arriving in Katowice:
        /// <code>
        /// GET api/flights
        /// {
        ///   "departTimeFrom": "2024-04-23T12:00:00",
        ///   "arrivalLocation": "KTW"
        /// }
        /// </code>
        /// </remarks>
        /// <returns>Status200OK (with data): If flights matching the search criteria are found, the method returns a list of FlightViewModel objects, 
        /// Status404NotFound (no data): If no flights are found that match the search criteria
        /// </returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FlightViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int? id, string? flightNumber, DateTime? departTimeFrom, DateTime? departTimeTo,
                                 string? departLocation, string? arrivalLocation, string? planeType)
        {
            Enum.TryParse(planeType, true, out PlaneType resultType);
            var flights = _flightService.Get(id, flightNumber, departTimeFrom, departTimeTo, departLocation,
                                             arrivalLocation, resultType);
            if (flights == null)
            {
                return NotFound();
            }
            return Ok(flights.OrderBy(o => o.ArrivalLocation));
        }

        /// <summary>
        /// Retrieves a specific flight by its unique identifier. 
        /// This method allows for anonymous access.
        /// </summary>
        /// <param name="id">An integer representing the unique identifier of the flight to retrieve.</param>
        /// <returns>
        ///   * Status200OK (with data): If a flight with the specified ID is found, the method returns a single FlightViewModel object representing that flight.
        ///   * Status404NotFound (no data): If no flight is found with the provided ID, the method returns a 404 Not Found response.
        /// </returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FlightViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var flight = _flightService.GetSingle(id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }

        /// <summary>
        /// Creates a new flight record in the system.
        /// This method requires authorization (valid access token).
        /// </summary>
        /// <param name="flightDTO">A Flight object containing the details of the new flight to be created.</param>
        /// <remarks>
        /// PlaneTypes are defined in PlaneType enum
        /// Sample request:
        /// <code>
        /// POST api/flights
        /// {
        ///   "flightNumber": "LO123",
        ///   "departTime": "2024-04-22T09:01:17.507Z",
        ///   "departLocation": "KTW",
        ///   "arrivalLocation": "KRK",
        ///   "planeType": "Boeing"
        /// }
        /// </code>
        /// </remarks>
        /// <returns>
        ///   * Status201Created (with data): If the flight is successfully created, the method returns a 201 Created response with the newly created FlightViewModel object in the body.
        ///   * Status400BadRequest (no data): If the provided flight data is invalid or fails validation, the method returns a 400 Bad Request response with details about the validation errors in the ModelState.
        ///   * Status404NotFound (no data): If an unexpected error occurs during creation (e.g., resource not found), the method returns a 404 Not Found response (consider revising this based on your specific error handling).
        /// </returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FlightViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create(CreateFlightDTO flightDTO)
        {
            var validationResult = await _createFlightValidator.ValidateAsync(flightDTO);
            if (!ModelState.IsValid || !validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            var result = await _flightService.Create(flightDTO);
            if (ModelState.IsValid)
            {
                return Created(string.Empty, result);
            }
            return NotFound(result);
        }

        /// <summary>
        /// Updates an existing flight record in the system.
        /// This method requires authorization (valid access token).
        /// </summary>
        /// <param name="id">An integer representing the unique identifier of the flight to be updated.</param>
        /// <param name="flightVM">A FlightViewModel object containing the updated details for the flight.</param>
        /// <remarks>
        /// PlaneTypes are defined in PlaneType enum
        /// Sample request:
        /// <code>
        /// POST api/flights
        /// {
        ///     "id": 1,
        ///     "flightNumber": "SD123",
        ///     "departTime": "2024-04-22T08:44:44.587",
        ///     "departLocation": "SDA",
        ///     "arrivalLocation": "DDS",
        ///     "planeType": 0
        /// }
        /// </code>
        /// </remarks>
        /// <returns>
        ///   * Status200OK (with data): If the flight is successfully updated, the method returns a 200 OK response with the updated FlightViewModel object in the body.
        ///   * Status400BadRequest (no data): If the provided flight data is invalid, there's a mismatch between the ID in the URL and the object, or validation fails, the method returns a 400 Bad Request response with details about the errors in the ModelState.
        ///   * Status404NotFound (no data): If no flight is found with the provided ID, the method returns a 404 Not Found response.
        /// </returns>
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FlightViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, FlightViewModel flightVM)
        {
            if (id != flightVM.Id)
            {
                return BadRequest("Id mismatch");
            }
            var validationResult = await _viewModelValidator.ValidateAsync(flightVM);
            if (!ModelState.IsValid || !validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }
            var result = await _flightService.Update(flightVM);
            if (ModelState.IsValid)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        /// <summary>
        /// Deletes a flight record from the system.
        /// This method requires authorization (alid access token) and restricts access to users with the 'admin' claim.
        /// </summary>
        /// <param name="id">An integer representing the unique identifier of the flight to be deleted.</param>
        /// <returns>
        ///   * Status200OK (with data): If the flight is successfully deleted, the method returns a 200 OK response with number of changed records.
        ///   * Status404NotFound (no data): If no flight is found with the provided ID, the method returns a 404 Not Found response.
        /// </returns>
        [Authorize]
        [RequiresClaim(IdentityData.AdminUserClaimName, "true")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _flightService.Delete(id);
            if (result == 0)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
