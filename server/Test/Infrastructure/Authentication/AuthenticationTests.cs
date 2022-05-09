using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using API.Infrastructure.Authentication.Handlers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace Test.Infrastructure.Authentication;

public class AuthenticationTests
{
    private static ApiKeyAuthHandler CreateHandler()
    {
        var options = new Mock<IOptionsMonitor<ApiKeyAuthHandlerOptions>>();
        
        options
            .Setup(x => x.Get(It.IsAny<string>()))
            .Returns(new ApiKeyAuthHandlerOptions());
        
        options
            .Setup(x => x.CurrentValue)
            .Returns(new ApiKeyAuthHandlerOptions());

        var logger = new Mock<ILogger<ApiKeyAuthHandler>>();
        var loggerFactory = new Mock<ILoggerFactory>();
        loggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(logger.Object);
        
        var encoder = new Mock<UrlEncoder>();
        var clock = new Mock<ISystemClock>();
        
        return new ApiKeyAuthHandler(options.Object, loggerFactory.Object, encoder.Object, clock.Object);
    }
    
    [Test]
    public async Task ValidApiKey()
    {
        var context = new DefaultHttpContext();
        context.HttpContext.Request.Headers.Add("Authorization", $"Bearer {new ApiKeyAuthHandlerOptions().ApiKeys.First()}");

        var handler = CreateHandler();
        await handler.InitializeAsync(new AuthenticationScheme(ApiKeyAuthHandler.SchemeName, null, typeof(ApiKeyAuthHandler)), context);
        var result = await handler.AuthenticateAsync();
        
        Assert.IsTrue(result.Succeeded);
    }
    
    [Test]
    public async Task InvalidApiKey()
    {
        var context = new DefaultHttpContext();
        context.HttpContext.Request.Headers.Add("Authorization", $"Bearer AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

        var handler = CreateHandler();
        await handler.InitializeAsync(new AuthenticationScheme(ApiKeyAuthHandler.SchemeName, null, typeof(ApiKeyAuthHandler)), context);
        var result = await handler.AuthenticateAsync();
        
        Assert.IsFalse(result.Succeeded);
    }
    
    [Test]
    public async Task NoApiKey()
    {
        var context = new DefaultHttpContext();

        var handler = CreateHandler();
        await handler.InitializeAsync(new AuthenticationScheme(ApiKeyAuthHandler.SchemeName, null, typeof(ApiKeyAuthHandler)), context);
        var result = await handler.AuthenticateAsync();
        
        Assert.IsFalse(result.Succeeded);
    }
}