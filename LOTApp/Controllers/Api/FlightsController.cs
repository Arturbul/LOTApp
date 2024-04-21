using FluentValidation;
using LOTApp.Attribiutes;
using LOTApp.Business.Services;
using LOTApp.Core.Authentication;
using LOTApp.Core.Extensions;
using LOTApp.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LOTApp.Controllers.Api
{
    [Authorize]
    [ApiController]
    [Route("api/flights")]
    public class FlightsController : Controller
    {
        private readonly IFlightService _flightService;
        private readonly IValidator<FlightViewModel> _validator;
        public FlightsController(IFlightService flightManager, IValidator<FlightViewModel> validator)
        {
            _flightService = flightManager;
            _validator = validator;
        }

        //GET
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FlightViewModel>))]
        public IActionResult Get(int? id, string? flightNumber, DateTime? departTimeFrom, DateTime? departTimeTo, string? departLocation, string? arrivalLocation, int? planeType)
        {
            var flights = _flightService.Get(id, flightNumber, departTimeFrom, departTimeTo, departLocation, arrivalLocation, planeType);
            if (flights == null)
            {
                return NotFound();
            }
            return Ok(flights.OrderBy(o => o.ArrivalLocation));
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var flight = _flightService.GetSingle(id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }

        //POST
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(FlightViewModel flightVM)
        {
            var validationResult = await _validator.ValidateAsync(flightVM);
            if (!ModelState.IsValid || !validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }

            var result = await _flightService.Create(flightVM);
            if (ModelState.IsValid)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FlightViewModel flightVM)
        {
            if (id != flightVM.Id)
            {
                return BadRequest("Id mismatch");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _flightService.Update(flightVM);
            if (ModelState.IsValid)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        //DELETE
        [Authorize]
        [RequiresClaim(IdentityData.AdminUserClaimName, "true")]
        [HttpDelete("{id}")]
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
