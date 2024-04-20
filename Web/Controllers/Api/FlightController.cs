using Business.Interface;
using Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Api
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FlightController : Controller
    {
        private readonly IFlightManager _flightManager;
        public FlightController(IFlightManager flightManager)
        {
            _flightManager = flightManager;
        }

        //GET
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var flights = await _flightManager.Get();
            if (flights == null)
            {
                return NotFound();
            }
            return Ok(flights.OrderBy(o => o.ArrivalLocation));
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var flight = await _flightManager.GetSingle((int)id);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _flightManager.Create(flightVM);
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
            var result = await _flightManager.Update(flightVM);
            if (ModelState.IsValid)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        //DELETE
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _flightManager.Delete(id);
            if (result == 0)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
