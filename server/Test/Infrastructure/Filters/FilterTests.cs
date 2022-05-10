using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    [SetUp]
    public void Init()
    {
        ApiKeyStore.InitKeys(
            Array.Empty<string>(),
            1,
            5
        );
    }

    private static ActionExecutingContext CreateActionExecutingContext(ApiKey key)
    {
        ApiKeyStore.AddKey(key);

        var httpContext = new DefaultHttpContext();
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, key.Key)
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
        const string apiTestKey = "1f2eb11d-6fa8-41de-89f7-f0b99adedba6";
        const int requestLimit = 5;
        const int requestLimitPeriodHours = 1;

        var context = CreateActionExecutingContext(
            new ApiKey(apiTestKey, requestLimit, requestLimitPeriodHours)
        );
        
        var rateLimit = new RateLimit();
        rateLimit.OnActionExecuting(context);

        Assert.IsNull(context.Result);
    }

    [Test]
    public void RateLimitOverLimitTest()
    {
        const string apiTestKey = "51c836f3-467c-40b5-a4bc-7f4d395c1abf";
        const int requestLimit = 5;
        const int requestLimitPeriodHours = 1;

        var context = CreateActionExecutingContext(
            new ApiKey(apiTestKey, requestLimit, requestLimitPeriodHours)
        );

        var rateLimit = new RateLimit();

        for (var i = 0; i < requestLimit + 1; i++)
        {
            rateLimit.OnActionExecuting(context);
        }

        Assert.IsNotNull(context.Result);
    }

    [Test]
    public void RateLimitZeroHourTest()
    {
        const string apiTestKey = "c30e5dfa-f257-476c-b54b-a46f414e4f03";
        const int requestLimit = 1;
        const double requestLimitPeriodHours = 0;

        var context = CreateActionExecutingContext(
            new ApiKey(apiTestKey, requestLimit, requestLimitPeriodHours)
        );

        var rateLimit = new RateLimit();

        for (var i = 0; i < requestLimit + 5; i++)
        {
            rateLimit.OnActionExecuting(context);
        }

        Assert.IsNull(context.Result);
    }
}