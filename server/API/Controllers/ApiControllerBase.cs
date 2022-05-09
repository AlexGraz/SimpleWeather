using API.Infrastructure.Authentication.Attributes;
using API.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController, AuthorizeApiKey, RateLimit]
public abstract class ApiControllerBase : ControllerBase
{
    protected ApiControllerBase()
    {
    }
}