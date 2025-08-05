using RepositoryAbstractions;

namespace Repository;

public class RepositoryFactory(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public IUserRepository CreateUserRepository() => new UserRepository(_context);

}