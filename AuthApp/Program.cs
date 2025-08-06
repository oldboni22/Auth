using AuthApp.ActionFilters;
using AuthApp.Extensions;

namespace AuthApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddJwtBearer(builder.Configuration);

        builder.Services.AddAutoMapper
        (
            cfg => { },
            typeof(Program)
        );
        
        
        builder.Services.AddAppDbContext(builder.Configuration);
        builder.Services.AddRepository();
        
        builder.Services.AddRsa(builder.Configuration);
        builder.Services.AddJwtManager(builder.Configuration);
        builder.Services.AddService();

        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ValidationFilter>();
        });
        
        var app = builder.Build();
        
        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.MapControllers();
        app.Run();
    }
}