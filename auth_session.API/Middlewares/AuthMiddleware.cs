namespace auth_session.API.Middlewares
{
    public class AuthMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger")
            || context.Request.Path.StartsWithSegments("/api/auth/login")
            || context.Request.Path.StartsWithSegments("/api/auth/register"))
            {
                await _next(context);
                return;
            }

            if (context.Session.GetString("UserId") == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                throw new UnauthorizedAccessException();
            }

            await _next(context);
        }
    }
}