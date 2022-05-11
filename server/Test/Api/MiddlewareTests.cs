using System.IO;
using System.Threading.Tasks;
using API.Infrastructure.Authentication;
using Core.Infrastructure.Util;
using MediatR;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;

namespace Test.Api;

public class MiddlewareTests
{
    [Test]
    public async Task ControllerCorrectAttributes()
    {
        var context = new DefaultHttpContext
        {
            Response =
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                Body = new MemoryStream()
            }
        };

        AuthResponseMiddleware.WriteResponse(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var reader = new StreamReader(context.Response.Body);
        var text = await reader.ReadToEndAsync();

        Assert.IsTrue(text == Result.Fail("Unauthorised", StatusCodes.Status401Unauthorized).Serialize(),
            "Return from middleware did not match expected");
    }
}