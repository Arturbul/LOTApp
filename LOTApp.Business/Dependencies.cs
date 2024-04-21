using LOTApp.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LOTApp.Business
{
    public class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IFlightService, FlightService>();

            //DI for DataAccess
            DataAccess.Dependencies.Register(services);
        }
    }
}
