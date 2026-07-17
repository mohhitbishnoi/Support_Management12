using Application.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DataContexts;
using Persistence.Extension.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
namespace Persistence.Extension;

public static class ServiceCollectionExtension
{
    public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddRepository(configuration);
    }

public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    
    {
        //var connectionstring = configuration.GetConnectionString("DefaultConnection");
        //services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionstring));

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
       options.UseNpgsql(connectionString, npgsql =>
       {
           npgsql.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
           npgsql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
       }));
    }
    public static void AddRepository(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

}


