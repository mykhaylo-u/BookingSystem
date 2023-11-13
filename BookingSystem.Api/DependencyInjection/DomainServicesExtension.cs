using BookingSystem.Abstractions.Repositories;
using BookingSystem.Repositories;

namespace BookingSystem.Api.DependencyInjection
{
    public static class DomainServicesExtension
    {
        public static IServiceCollection RegisterDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<ITheaterRepository, TheaterRepository>();
            services.AddScoped<IShowTimeRepository, ShowTimeRepository>();
            services.AddScoped<ISeatReservationRepository, SeatReservationRepository>();

            return services;
        }
    }
}
