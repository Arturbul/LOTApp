using LOTApp.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LOTApp.DataAccess
{
    public class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IFlightRepository, FlightRepository>();
        }
    }
}
