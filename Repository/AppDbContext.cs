using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SetUserTriggers(modelBuilder);
        CreateUserIndex(modelBuilder);
    }

    private void CreateUserIndex(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().
            HasIndex(u => u.Name).
            IsUnique();
    }

    private void SetUserTriggers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().
            ToTable(builder =>
            {
                builder.HasTrigger(User.LastUpdatedTrigger);
                builder.HasTrigger(User.SetCreatedTrigger);
            });
    }
}