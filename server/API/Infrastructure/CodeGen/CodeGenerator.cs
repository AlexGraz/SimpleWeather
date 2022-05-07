using System.Reflection;
using Core.Infrastructure.CodeGen;
using TeeSquare.Configuration;
using TeeSquare.Reflection;
using TeeSquare.UnionTypes;
using TeeSquare.WebApi;
using TeeSquare.WebApi.Net50;
using TeeSquare.WebApi.Reflection;

namespace API.Infrastructure.CodeGen;

public class CodeGenerator
{
    private Type[] GetControllers()
    {
        return GetType().Assembly.GetExportedTypes()
            .Where(t => t.IsAssignableTo(StaticConfig.Instance.ControllerType)
                        && !t.IsAbstract
                        && !t.GetCustomAttributes<NoCodeGenerationAttribute>().Any())
            .OrderBy(c => $"{c.Namespace}.{c.Name}")
            .ToArray();
    }

    public MemoryStream Generate()
    {
        var memoryStream = new MemoryStream();
        Net50Configurator.Configure();
        TeeSquareWebApi.GenerateForControllers(GetControllers())
            .Configure(TeeSquareUnionTypes.Configure)
            .Configure(options =>
                {
                    options.WriteHeader = writer =>
                    {
                        writer.WriteLine("// CodeGen - do not edit or delete");
                        writer.WriteLine("// eslint-disable");
                    };
                    options.FactoryNameStrategy = (controller, _, _) =>
                        $"{controller.Name.Replace("Controller", "")}RequestFactory";
                    options.NameRouteStrategy = (_, action, _, _) => action.Name;
                    options.TypeConverter.TypeName = options.TypeConverter.TypeName.ExtendStrategy(original =>
                        type => TypeName(type, original));
                }
            ).WriteToStream(memoryStream);
        memoryStream.Position = 0;
        return memoryStream;
    }

    private static string TypeName(Type type, NameStrategy<Type> original)
    {
        var typeName = original(type);
        return typeName.EndsWith("Dto") ? typeName[..^3] : typeName;
    }
}