using Microsoft.OpenApi.Models;
using System.Reflection;

namespace BookingSystem.Api.DependencyInjection
{
    public static class SwaggerServicesExtension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Booking System Api",
                    Version = "v1",
                    Description = "Booking System Api",
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
