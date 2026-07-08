using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commons.Mappings;

public interface IMapFrom<T>
{
    void createMappings(Profile profile) => profile.CreateMap(GetType(),typeof(T));
}
