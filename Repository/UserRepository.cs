using System.Linq.Expressions;
using Domain.Entities;
using RepositoryAbstractions;

namespace Repository;

public class UserRepository(AppDbContext context) : RepositoryBase<User>(context), IUserRepository
{
    public IEnumerable<User> FindUsers(Expression<Func<User, bool>> expression, bool trackChanges) =>
        FindByCondition(expression, trackChanges);
    
    public void CreateUser(User user) => Create(user);
    public void DeleteUser(User user) => Delete(user);
}