using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryAbstractions;

namespace Repository;

public class UserRepository(AppDbContext context) : RepositoryBase<User>(context), IUserRepository
{
    public async Task<User?> FindUserByNameAsync(string name, bool trackChanges) => await 
            FindByCondition(u => string.Equals(name, u.Name), trackChanges).
            FirstOrDefaultAsync();
    
    public void CreateUser(User user) => Create(user);
    public void DeleteUser(User user) => Delete(user);
}