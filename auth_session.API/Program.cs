using auth_session.API.Middlewares;
using auth_session.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();

var app = builder.Build();

// For API document
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowFrontend");
// Middleware
app.UseSession();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<RememberMeMiddleware>();
app.UseMiddleware<AuthMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();