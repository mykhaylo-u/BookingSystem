using BookingSystem.Api.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.DependencyInjection
{
    public static class ErrorHandlerServicesExtension
    {
        public static IServiceCollection AddErrorHandler(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    var response = new ErrorResponse
                    {
                        Message = string.Join(' ', errors),
                        ErrorCode = "BS-400"
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }
    }
}
