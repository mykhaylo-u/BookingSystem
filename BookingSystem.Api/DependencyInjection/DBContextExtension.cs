using BookingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Api.DependencyInjection
{
    public static class DBContextExtension
    {
        public static IServiceCollection AddBookingContext(this IServiceCollection services)
        {
            services.AddDbContext<BookingDbContext>(options =>
                options.UseInMemoryDatabase("BookingDatabase"));

            return services;
        }
    }
}
