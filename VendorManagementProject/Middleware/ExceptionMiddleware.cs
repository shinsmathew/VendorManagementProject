using System.Net;
using System.Text.Json;

namespace VendorManagementProject.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                
                await _next(context);
            }
            catch (Exception ex)
            {
                
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = exception switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                ValidationException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

           
            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "An error occurred while processing your request.",
                Detail = exception.Message
            };

            
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        public class NotFoundException : Exception
        {
            public NotFoundException(string message) : base(message) { }
        }

        public class ValidationException : Exception
        {
            public ValidationException(string message) : base(message) { }
        }
    }
}
