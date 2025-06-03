using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace WaterTimeServer.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Call the next delegate/middleware in the pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                // Handle the exception here
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var errorResponse = new
            {
                Message = "An unexpected error occurred on the server.",
                Detail = ex.Message
            };

            // Set the HTTP Status Code (e.g., 500 for server errors).
            switch (ex)
            {
                case InvalidOperationException _:
                    context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
            }
            context.Response.ContentType = "application/json";
            Console.WriteLine(errorResponse);
        }
    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
