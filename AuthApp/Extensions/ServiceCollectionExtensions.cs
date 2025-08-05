using Jwt;
using Jwt.Abstractions;
using Microsoft.EntityFrameworkCore;
using Repository;
using RepositoryAbstractions;
using RSA;
using RSA.Abstractions;
using Service;
using Service.Contracts;

namespace AuthApp.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAppDbContext(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddDbContext<AppDbContext>(builder => 
            builder.UseSqlServer(configuration.GetConnectionString("sql")));
    }

    public static void AddRepository(this IServiceCollection collection)
    {
        collection.AddScoped<IRepositoryManager, RepositoryManager>();
    }

    public static void AddService(this IServiceCollection collection)
    {
        collection.AddScoped<IServiceManager, ServiceManager>();
    }

    public static void AddRsa(this IServiceCollection collection, IConfiguration configuration)
    {
        var path = configuration.GetSection("Rsa").Value;

        collection.AddSingleton<ICertificate>(new Certificate(path!));
    }

    public static void AddJwtManager(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddSingleton<IJwtParameters>(new JwtParameters(configuration));

        collection.AddSingleton<IJwtManager, JwtManager>();
    }
}