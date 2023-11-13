namespace BookingSystem.Api.DependencyInjection
{
    public static class MediatRExtension
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(
                cfg => cfg.RegisterServicesFromAssemblies(typeof(Domain.Models.Info).Assembly,
                    typeof(Domain.Services.Info).Assembly));

            return services;
        }
    }
}
