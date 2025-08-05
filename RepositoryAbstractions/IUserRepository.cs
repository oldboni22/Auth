using System.Linq.Expressions;
using Domain.Entities;

namespace RepositoryAbstractions;

public interface IUserRepository
{
    Task<User?> FindUserByName(string name, bool trackChanges);
    void CreateUser(User user);
    void DeleteUser(User user);
}