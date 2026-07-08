using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commons.Mappings;

public  interface ICreateMapFrom<T>
{
    void createMappings(Profile profile) => profile.CreateMap(GetType(),typeof(T));

}
