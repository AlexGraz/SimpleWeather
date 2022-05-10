using API.Infrastructure.Authentication.Handlers;
using API.Infrastructure.Errors;
using Features.Weather.Infrastructure;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication()
    .AddScheme<ApiKeyAuthHandlerOptions, ApiKeyAuthHandler>(ApiKeyAuthHandler.SchemeName, null);

builder.Services.AddControllers();
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient();
builder.Services.AddTransient<IWeatherService, WeatherService>();

var app = builder.Build();

var isDev = app.Environment.IsDevelopment();
app.UseExceptionHandler(isDev);

// Configure the HTTP request pipeline.
if (!isDev)
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();