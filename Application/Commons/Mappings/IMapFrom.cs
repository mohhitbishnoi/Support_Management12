using AutoMapper;

namespace Application.Commons.Mappings;

public interface IMapFrom<T>
{
    void CreateMappings(Profile profile)
    {
        profile.CreateMap(typeof(T), GetType());
    }
}