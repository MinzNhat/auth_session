namespace auth_session.API.Middlewares
{
    public class RememberMeMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            if (context.Session.GetString("UserId") != null)
            {
                bool rememberMe = context.Session.GetString("RememberMe") == "true";

                if (rememberMe)
                {
                    context.Session.SetString("UserId", context.Session.GetString("UserId") ?? "");
                    context.Session.SetString("RememberMe", "true");
                }
            }

            await _next(context);
        }
    }
}