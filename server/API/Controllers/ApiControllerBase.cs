using API.Infrastructure.Authorisation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController, KeyAuthorize]
public abstract class ApiControllerBase : ControllerBase
{
    protected ApiControllerBase()
    {
    }
}