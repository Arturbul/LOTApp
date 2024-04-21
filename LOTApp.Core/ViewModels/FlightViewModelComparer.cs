namespace LOTApp.Core.ViewModels
{
    public class FlightViewModelComparer : IEqualityComparer<FlightViewModel>
    {
        public bool Equals(FlightViewModel? x, FlightViewModel? y)
        {
            if (x == null || y == null) return false;
            return
              x.Id == y.Id &&
              x.FlightNumber == y.FlightNumber &&
              x.DepartTime == y.DepartTime &&
              x.DepartLocation == y.DepartLocation &&
              x.ArrivalLocation == y.ArrivalLocation &&
              x.PlaneType == y.PlaneType;
        }

        public int GetHashCode(FlightViewModel obj)
        {
            // Combine hash codes of relevant properties for consistent comparison
            return obj.Id.GetHashCode() ^
                   obj.FlightNumber.GetHashCode() ^
                   obj.DepartTime.GetHashCode() ^
                   obj.DepartLocation.GetHashCode() ^
                   obj.ArrivalLocation.GetHashCode() ^
                   obj.PlaneType.GetHashCode();
        }
    }
}
