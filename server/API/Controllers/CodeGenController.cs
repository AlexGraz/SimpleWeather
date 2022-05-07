using API.Infrastructure.CodeGen;
using Core.Infrastructure.CodeGen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/v1/code-gen"), AllowAnonymous, NoCodeGeneration]
public class CodeGenController : ApiControllerBase
{
    [HttpGet("api-definitions.ts")]
    public IActionResult Definitions()
    {
        var generator = new CodeGenerator();
        var ms = generator.Generate();
        return File(ms, "text/x.typescript");
    }
}