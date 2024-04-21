using LOTApp.Core.Models;

namespace LOTApp.Core.ViewModels
{
    public class FlightViewModel
    {
        public int Id { get; set; }

        public string FlightNumber { get; set; } = null!;
        public DateTime DepartTime { get; set; }
        public string DepartLocation { get; set; } = null!;
        public string ArrivalLocation { get; set; } = null!;
        public PlaneType PlaneType { get; set; }
    }
}
