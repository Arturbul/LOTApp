using Business.Interface;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Razor.Flight
{
    public class FlightController : Controller
    {
        private readonly IFlightManager _flightManager;
        public FlightController(IFlightManager flightManager)
        {
            _flightManager = flightManager;
        }
        public async Task<IActionResult> Index()
        {
            var flights = await _flightManager.Get();
            return View(flights.OrderBy(o => o.ArrivalLocation));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var flight = await _flightManager.GetSingle((int)id);
            return View(flight);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(FlightViewModel flightVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _flightManager.Create(flightVM); //return result;
                if (ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(flightVM);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var flight = await _flightManager.GetSingle((int)id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, FlightViewModel flightVM)
        {
            if (id != flightVM.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var flight = await _flightManager.Update(flightVM);
                return RedirectToAction(nameof(Index));
            }
            return View(flightVM);
        }

        public IActionResult Remove(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost, ActionName("Remove")]
        public async Task<IActionResult> RemoveConfirmation(int id)
        {
            var result = await _flightManager.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
