using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Infrastructure.Authentication;
using API.Infrastructure.Authentication.Handlers;
using API.Infrastructure.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;
using NUnit.Framework;

namespace Test.Infrastructure.Filters;

public class FilterTests
{
    private static ActionExecutingContext CreateActionExecutingContext(string apiKey, int requestLimit, double requestLimitPeriodHours)
    {
        var options = new ApiKeyAuthHandlerOptions();
        ApiKeyStore.InitKeys(
            options.ApiKeys,
            requestLimit,
            requestLimitPeriodHours
        );

        var httpContext = new DefaultHttpContext();

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, apiKey)
        };

        var appIdentity = new ClaimsIdentity(claims);
        httpContext.User.AddIdentity(appIdentity);

        var actionContext = new ActionContext(
            httpContext: httpContext,
            routeData: new RouteData(),
            actionDescriptor: new ActionDescriptor(),
            modelState: new ModelStateDictionary()
        );


        return new ActionExecutingContext(
            actionContext,
            new List<IFilterMetadata>(),
            new Dictionary<string, object?>(),
            new Mock<Controller>().Object
        );
    }
    
    [Test]
    public void RateLimitOneRequestTest()
    {
        const int requestLimit = 5;

        var context = CreateActionExecutingContext(new ApiKeyAuthHandlerOptions().ApiKeys.First(), requestLimit, ApiKeyAuthHandlerOptions.DefaultRequestLimitPeriodHours);
        var rateLimit = new RateLimit();
        rateLimit.OnActionExecuting(context);

        Assert.IsNull(context.Result);
    }
    
    [Test]
    public void RateLimitOverLimitTest()
    {
        const int requestLimit = 5;

        var context = CreateActionExecutingContext(new ApiKeyAuthHandlerOptions().ApiKeys.First(), requestLimit, ApiKeyAuthHandlerOptions.DefaultRequestLimitPeriodHours);
        var rateLimit = new RateLimit();
        
        for (var i = 0; i < requestLimit + 1; i++)
        {
            rateLimit.OnActionExecuting(context);
        }

        Assert.IsNotNull(context.Result);
    }
    
    [Test]
    public async Task RateLimitZeroHourTest()
    {
        const int requestLimit = 1;
        const double requestLimitPeriodHours = 0;

        var context = CreateActionExecutingContext(new ApiKeyAuthHandlerOptions().ApiKeys.First(), requestLimit, requestLimitPeriodHours);
        var rateLimit = new RateLimit();
        
        for (var i = 0; i < requestLimit + 5; i++)
        {
            rateLimit.OnActionExecuting(context);
        }

        Assert.IsNull(context.Result);
    }
}