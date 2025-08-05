using AuthApp.ActionFilters;

namespace AuthApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddAuthorization();



        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ValidationFilter>();
        });
        
        var app = builder.Build();

       

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}