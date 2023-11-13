using Serilog.Events;
using Serilog;

namespace BookingSystem.Api.DependencyInjection
{
    public static class SerilogExtension
    {
        public static IServiceCollection AddSerilog(this IServiceCollection services)
        {
            // Initialize Serilog logger
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            return services;
        }
    }
}
