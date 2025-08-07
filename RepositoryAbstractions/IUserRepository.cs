using System.Linq.Expressions;
using Domain.Entities;

namespace RepositoryAbstractions;

public interface IUserRepository
{
    Task<User?> FindUserByNameAsync(string name, bool trackChanges);
    Task<User?> FindUserByIdAsync(int id, bool trackChanges);
    void CreateUser(User user);
    void DeleteUser(User user);
    void UpdateUser(User user);
}