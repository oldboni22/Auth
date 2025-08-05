using System.Linq.Expressions;
using Domain.Entities;

namespace RepositoryAbstractions;

public interface IUserRepository
{
    IEnumerable<User> FindUsers(Expression<Func<User, bool>> expression, bool trackChanges);
    void CreateUser(User user);
    void DeleteUser(User user);
}