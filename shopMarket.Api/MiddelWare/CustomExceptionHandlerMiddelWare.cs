using Serilog;
using System.Text.Json;

namespace shopMarket.Api.MiddelWare
{
    public class CustomExceptionHandlerMiddelWare
    {
        private readonly RequestDelegate _Next;
        public CustomExceptionHandlerMiddelWare(RequestDelegate next) 
        {
            _Next = next;
        }

        public async Task InvokeAsync (HttpContext context)
        {
            try
            {
                Log.Information($"{context.Request.Method} , {context.Request.RouteValues.First()} Pass to the next middelware");
                await _Next(context);
            }catch (Exception ex)
            {
                Log.Information(ex, $"{context.Request.Method}, {context.Request.RouteValues.First()}An unhandled exception occurred.");
                await HandelExceptionAsync(context, ex);
                
            }
        }

        private static Task HandelExceptionAsync(HttpContext context , Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            var response = new
            {
                Error  = "An unexpected error occurred.",
                Details = exception.Message,
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
