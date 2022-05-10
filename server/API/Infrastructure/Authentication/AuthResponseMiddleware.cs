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
    
            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                context.Response.ContentType = MediaTypeNames.Application.Json;
                
                var result = Result<Unit>.Fail("Unauthorised", StatusCodes.Status401Unauthorized);
                var response = result.Serialize();
                await context.Response.WriteAsync(response);
            }
        });
    }
}