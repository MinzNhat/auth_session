using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace auth_session.API.Filters.Response
{
    public class ResponseFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                string? message = context.HttpContext.Items["CustomMessage"] as string;

                if (!context.ModelState.IsValid)
                {
                    var errors = context.ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    context.Result = new ObjectResult(new Response<string>(
                        string.Join("; ", errors),
                        message ?? ""
                    ))
                    {
                        StatusCode = 400
                    };
                    return;
                }

                var valueType = objectResult.Value?.GetType();
                bool isResponseType = valueType != null && valueType.IsGenericType &&
                                      valueType.GetGenericTypeDefinition() == typeof(Response<>);

                if (isResponseType)
                {
                    return;
                }

                context.Result = new ObjectResult(new Response<object>(
                    objectResult.Value!,
                    message ?? "Request successful"
                ))
                {
                    StatusCode = objectResult.StatusCode
                };
            }
        }
    }
}