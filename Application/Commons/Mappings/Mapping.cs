using AutoMapper;
using System.Reflection;

namespace Application.Commons.Mappings;

public class Mapping : Profile
{
    public Mapping()
    {
        ApplyMappingsFromAssembly(GetType().Assembly);
    }
    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes();
        foreach (var type in types)
        {
            var profileType = typeof(Profile);
            var mapFromType = typeof(IMapFrom<>);
            var createMapFromType = typeof(ICreateMapFrom<>);

            bool HasInterface(Type t, Type interfaceType) =>
                t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType);

            var types1 = assembly.GetExportedTypes().Where(t => HasInterface(t, mapFromType) || HasInterface(t, createMapFromType)).ToList();

            foreach (var type1 in types)
            {
                var instance = Activator.CreateInstance(type);

                var interfaces = type.GetInterfaces().Where(i => i.IsGenericType).ToList();

                foreach (var @interface in interfaces)
                {
                    var genericType = @interface.GetGenericTypeDefinition();

                    if(genericType == mapFromType || genericType == createMapFromType)
                    {
                        var mappingMethod = @interface.GetMethod(
                            genericType == mapFromType ? nameof(IMapFrom<object>.createMappings) : nameof(ICreateMapFrom<object>.createMappings),
                            new[] { profileType }
                            );

                        mappingMethod?.Invoke(instance, new object[] {this});
                    }
                    
                }
            }
        }

    }
}
