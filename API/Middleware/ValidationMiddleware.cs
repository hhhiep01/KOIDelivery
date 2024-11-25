using FluentValidation;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ValidationMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            // Set the response status code to 400 (Bad Request)
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            // Create a custom error response object
            var response = new
            {
                statusCode = 400,
                isSuccess = false,
                errorMessage = "Validation failed", // You can modify this message or customize it further
                result = (object)null // You can customize the result field as needed
            };

            // Serialize and return the custom error response
            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
