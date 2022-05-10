using System.Net.Mime;
using Core.Infrastructure.Util;
using MediatR;

namespace API.Infrastructure.Authentication;

public static class AuthResponseMiddleware
{
    public static IApplicationBuilder UseUnauthorizedResponse(this WebApplication app)
    {
        return app.Use(async (context, next) => { Use(context, next); });
    }

    public static async void Use(HttpContext context, Func<Task> next)
    {
        await next();

        if (context.Response.StatusCode != StatusCodes.Status401Unauthorized) return;
        context.Response.ContentType = MediaTypeNames.Application.Json;

        var result = Result<Unit>.Fail("Unauthorised", StatusCodes.Status401Unauthorized);
        var response = result.Serialize();
        await context.Response.WriteAsync(response);
    }
}