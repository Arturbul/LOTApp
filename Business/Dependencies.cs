using Business.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Business
{
    public class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IFlightManager, FlightManager>();

            //DI for DataAccess
            DataAccess.Dependencies.Register(services);
        }
    }
}
