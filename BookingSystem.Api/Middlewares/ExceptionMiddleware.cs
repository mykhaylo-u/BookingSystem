﻿using System.Net;
using System.Text.Json;
using BookingSystem.Domain.Models;

namespace BookingSystem.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (DomainValidationException domainValidationException)
            {
                _logger.LogWarning(domainValidationException, "A domain validation error occurred.");
                await HandleDomainValidationExceptionAsync(httpContext, domainValidationException);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ErrorResponse
            {
                Message = "An unexpected error occurred.",
                ErrorCode = "BS-500"
            };

            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }

        private Task HandleDomainValidationExceptionAsync(HttpContext context, DomainValidationException domainValidationException)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var response = new ErrorResponse
            {
                Message = domainValidationException.Message,
                ErrorCode = domainValidationException.ErrorCode
            };

            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    }
}
