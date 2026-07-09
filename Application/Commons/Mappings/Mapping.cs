using AutoMapper;
using System.Reflection;

namespace Application.Commons.Mappings;

public class Mapping : Profile
{
    public Mapping()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var profileType = typeof(Profile);

        var types = assembly.GetExportedTypes();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            if (instance == null)
                continue;

            var methodInfo = type.GetMethod("createMappings");

            methodInfo?.Invoke(instance, new object[] { this });
        }
    }
}