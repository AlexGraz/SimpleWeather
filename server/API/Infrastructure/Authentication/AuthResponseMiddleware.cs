using System.Net.Mime;
using Core.Infrastructure.Util;
using MediatR;

namespace API.Infrastructure.Authentication;

public static class AuthResponseMiddleware
{
    public static IApplicationBuilder UseUnauthorizedResponse(this WebApplication app)
    {
        return app.Use(async (context, next) =>
        {
            await next();
            WriteResponse(context);
        });
    }

    public static async void WriteResponse(HttpContext context)
    {
        if (context.Response.StatusCode != StatusCodes.Status401Unauthorized) return;
        context.Response.ContentType = MediaTypeNames.Application.Json;

        var result = Result.Fail("Unauthorised", StatusCodes.Status401Unauthorized);
        var response = result.Serialize();
        await context.Response.WriteAsync(response);
    }
}