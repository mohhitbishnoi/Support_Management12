using Application.Interfaces.Services.MailServices;
using Application.Interfaces.Services.TokenServices;
using Infrastructure.Extension.Service.MailServices;
using Infrastructure.Extension.Service.TokenSerivces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Extension;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(IServiceCollection services)
    {
        services.AddService();
    }
    public static void AddService(this IServiceCollection services)
    {
        services
            .AddScoped<IMailService, MailService>()
            .AddScoped<ITokenService, TokenSerivce>();
            

    }

}
