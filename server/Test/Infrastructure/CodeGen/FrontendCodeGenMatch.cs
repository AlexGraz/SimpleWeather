using System.IO;
using API.Infrastructure.CodeGen;
using NUnit.Framework;

namespace Test.Infrastructure.CodeGen;

public class FrontendCodeGenApi
{
    [Test]
    public void OutputMatchesFrontendCodeGen()
    {
        var generator = new CodeGenerator();
        using var ms = generator.Generate();
        using var sr = new StreamReader(ms);
        var latest = sr.ReadToEnd();

        BlurkCompare.Blurk.CompareFile("../../../../../web/src/code-gen/api-definitions.ts")
            .To(latest, true, "actual.ts")
            .AssertAreTheSame(Assert.Fail);
    }
}