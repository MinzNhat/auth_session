using System.Net;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using auth_session.API.Filters.Response;
using System.ComponentModel.DataAnnotations;

namespace auth_session.API.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex switch
            {
                DbUpdateException => (int)HttpStatusCode.Conflict,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                ValidationException => (int)HttpStatusCode.BadRequest,
                FormatException => (int)HttpStatusCode.BadRequest,
                BadHttpRequestException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var errorResponse = new Response<string>(
                error: ex switch
                {
                    DbUpdateException => "Database Error",
                    KeyNotFoundException => "Resource Not Found",
                    UnauthorizedAccessException => "Unauthorized",
                    ValidationException => "Validation Error",
                    FormatException => "Invalid Format",
                    BadHttpRequestException => "Invalid Request Data",
                    _ => "Internal Server Error"
                },
                message: ex.InnerException?.Message ?? ex.Message
            );

            return context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }
}