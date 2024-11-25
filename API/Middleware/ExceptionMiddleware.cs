
using Application.CustomExceptions;
using Application.Response;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;

namespace API.Middleware
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

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (DbUpdateException ex)
            {
                // Check if the exception message indicates a foreign key violation
                if (ex.InnerException.Message.Contains("23503: insert or update on table") &&
                    ex.InnerException.Message.Contains("violates foreign key constraint"))
                {
                    // Handle the foreign key violation error
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Response.ContentType = "application/json";
                    ApiResponse apiResponse = new ApiResponse().SetApiResponse(
                        statusCode: HttpStatusCode.BadRequest,
                        isSuccess: false,
                        message: "Foreign key constraint violation: " + ex.Message + "\n The entity id you inserted with may be not in db "
                        );

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(apiResponse));
                    _logger.LogError("Foreign key constraint violation: " + ex.Message);
                }
                else if (ex.InnerException.Message.Contains("23505") &&
                    ex.InnerException.Message.Contains("already exists"))
                {
                    // Handle the foreign key violation error
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Response.ContentType = "application/json";
                    ApiResponse apiResponse = new ApiResponse().SetApiResponse(
                        statusCode: HttpStatusCode.BadRequest,
                        isSuccess: false,
                        message: "Foreign key constraint violation: " + ex.Message + "\n The entity id you inserted with already exists in database "
                        );

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(apiResponse));
                    _logger.LogError("duplicate key value violates unique constraint: " + ex.Message + ex.InnerException.Message);
                }
                else if (ex.InnerException.Message.Contains("23503") &&
                    ex.InnerException.Message.Contains("still referenced"))
                {
                    // Handle the foreign key violation error
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Response.ContentType = "application/json";
                    ApiResponse apiResponse = new ApiResponse().SetApiResponse(
                        statusCode: HttpStatusCode.BadRequest,
                        isSuccess: false,
                        message: ex.InnerException.Message
                        );

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(apiResponse));
                    _logger.LogError("duplicate key value violates unique constraint: " + ex.Message + ex.InnerException.Message);
                }
                else
                {
                    // Handle other PostgreSQL exceptions as needed
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync("Database error: " + ex.Message);
                    _logger.LogError("Database error: " + ex.Message);
                }
            }
            catch (NotFoundException ex)
            {
                ApiResponse apiResponse = new ApiResponse().SetApiResponse(
                       statusCode: HttpStatusCode.NotFound,
                       isSuccess: false,
                       message: $"message: {ex.Message}  detail:  {ex.InnerException?.Message}"
                       );

                await context.Response.WriteAsync(JsonConvert.SerializeObject(apiResponse));
                _logger.LogError("NotFound error: " + ex.Message);
            }
            catch (NotMatchException ex)
            {
                ApiResponse apiResponse = new ApiResponse().SetApiResponse(
                       statusCode: HttpStatusCode.Conflict,
                       isSuccess: false,
                       message: $"message: {ex.Message}  detail:  {ex.InnerException?.Message}"
                       );

                await context.Response.WriteAsync(JsonConvert.SerializeObject(apiResponse));
                _logger.LogError("NotMatch error: " + ex.Message);
            }
            catch (ConflictException ex)
            {
                ApiResponse apiResponse = new ApiResponse().SetApiResponse(
                       statusCode: HttpStatusCode.NotFound,
                       isSuccess: false,
                       message: $"message: {ex.Message}  detail:  {ex.InnerException?.Message}"
                       );

                await context.Response.WriteAsync(JsonConvert.SerializeObject(apiResponse));
                _logger.LogError("Conflict error: " + ex.Message);
            }
            catch (Exception ex)
            {
                ApiResponse apiResponse = new ApiResponse().SetApiResponse(
                       statusCode: HttpStatusCode.BadRequest,
                       isSuccess: false,
                       message: $"message: {ex.Message}  detail:  {ex.InnerException?.Message}"
                       );

                await context.Response.WriteAsync(JsonConvert.SerializeObject(apiResponse));
            }
        }
    }
}
