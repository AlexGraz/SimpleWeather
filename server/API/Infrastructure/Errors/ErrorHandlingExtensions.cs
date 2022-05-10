using System.Net.Mime;
using System.Text.Json;
using Core.Infrastructure.Util;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;

namespace API.Infrastructure.Errors;

public static class ErrorHandlingExtensions
{
    public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder app, bool isDevelopment)
    {
        return app.UseExceptionHandler(builder =>
        {
            builder.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = MediaTypeNames.Application.Json;

                var message = "A server error occurred";
                if (isDevelopment)
                {
                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    message = exceptionHandlerPathFeature?.Error.Message ?? "An unknown error occured";
                }

                var result = Result<Unit>.Fail(message, StatusCodes.Status500InternalServerError);
                var serializedResponse = JsonSerializer.Serialize(result, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                });
                await context.Response.WriteAsync(serializedResponse);
            });
        });
    }
}