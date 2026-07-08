using DocumentFormat.OpenXml.InkML;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Application.Extension;

public static class ServiceCollectionExtension
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediator();
        services.AddAutoMapping();

    }
    public static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(con =>con.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped<IMediator,Mediator>();

    }
    public static void AddAutoMapping(this IServiceCollection services)
    {
        //services.AddAutoMapper(typeof(Mapping<>));
        //services.AddAutoMapper(Mapping<>);
        services.AddAutoMapper(cfg => { }, typeof(Mapping));
    }
}
