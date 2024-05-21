using System.Net;
using System.Text.Json;


namespace API.Utility
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
            var statusCode = HttpStatusCode.InternalServerError;

            var response = new ErrorResponse
            {
                StatusCode = (int)statusCode,
                Message = "An error occurred on the server. Please try again later."
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);

            return context.Response.WriteAsync(json);
        }
    }

    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

}
