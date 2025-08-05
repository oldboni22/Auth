namespace RepositoryAbstractions;

public interface IRepositoryManager
{
    Task SaveChangesAsync();
    IUserRepository User { get; }
}