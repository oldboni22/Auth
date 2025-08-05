using RepositoryAbstractions;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly AppDbContext _context;
    
    public IUserRepository User { get; }
    
    public RepositoryManager(AppDbContext context)
    {
        _context = context;
        var factory = new RepositoryFactory(context);

        User = factory.CreateUserRepository();
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    
}