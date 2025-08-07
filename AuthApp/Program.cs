using AuthApp.ActionFilters;
using AuthApp.Extensions;

namespace AuthApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAutoMapper
        (
            cfg => { },
            typeof(Program)
        );
        
        
        builder.Services.AddAppDbContext(builder.Configuration);
        builder.Services.AddRepository();
        
        builder.Services.AddRsa(builder.Configuration);
        
        builder.Services.AddJwtParameters(builder.Configuration);
        builder.Services.AddJwtManager();
        
        builder.Services.AddService();

        builder.Services.ConfigureAuthentication();
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ValidationFilter>();
        });
        
        var app = builder.Build();
        
        app.ConfigureExceptionHandling();
        
        app.UseHttpsRedirection();

        app.UseAuthentication();
        
        app.MapControllers();
        app.Run();
    }
}