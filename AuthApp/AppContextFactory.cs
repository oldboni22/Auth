using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;

namespace AuthApp;

public class AppContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json").
            Build();

        var connectionString = configuration.GetConnectionString("sql");

        var optionsBuilder = new DbContextOptionsBuilder().
            UseSqlServer
            (
                connectionString,
                builder => builder.MigrationsAssembly(typeof(Program).Assembly)
            );

        return new AppDbContext(optionsBuilder.Options);
    }
}