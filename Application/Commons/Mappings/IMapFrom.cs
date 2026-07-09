using AutoMapper;

namespace Application.Commons.Mappings;

public interface IMapFrom<T>
{
    void createMappings(Profile profile)
    {
        profile.CreateMap(typeof(T), GetType());
    }
}