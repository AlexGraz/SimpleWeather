using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected ApiControllerBase()
    {
    }
}