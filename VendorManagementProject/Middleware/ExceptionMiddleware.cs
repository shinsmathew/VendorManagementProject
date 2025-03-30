using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using VendorManagementProject.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger; //instance for logging exceptions.

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
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred. Stack Trace: {StackTrace}", ex.StackTrace);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new
        {
            StatusCode = exception switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                ValidationException => (int)HttpStatusCode.BadRequest,
                System.UnauthorizedAccessException => (int)HttpStatusCode.Forbidden,
                UserAlreadyExistsException => (int)HttpStatusCode.Conflict,
                InvalidCredentialsException => (int)HttpStatusCode.Unauthorized,
                _ => (int)HttpStatusCode.InternalServerError
            },
            Error = new { Message = exception.Message, Type = exception.GetType().Name }
        };

        context.Response.StatusCode = response.StatusCode;
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

}
