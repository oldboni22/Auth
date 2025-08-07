using AuthApp.ConfigureOptions;
using Jwt;
using Jwt.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

    public static void AddJwtParameters(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddSingleton<IJwtParameters>(new JwtParameters(configuration));
    }
    
    public static void AddJwtManager(this IServiceCollection collection)
    {
        collection.AddSingleton<IJwtManager, JwtManager>();
    }

    public static void ConfigureAuthentication(this IServiceCollection collection)
    {
        
        collection.AddSingleton<IConfigureNamedOptions<JwtBearerOptions>, JwtBearerOptionsConfigurator>();
        
        collection.
            AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
            AddJwtBearer(JwtBearerDefaults.AuthenticationScheme);
    }
}