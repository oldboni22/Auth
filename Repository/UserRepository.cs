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

    public async Task<User?> FindUserByIdAsync(int id, bool trackChanges) => await 
            FindByCondition(u => u.Id == id, false).
            FirstOrDefaultAsync();

    public void CreateUser(User user) => Create(user);
    public void DeleteUser(User user) => Delete(user);
    public void UpdateUser(User user) => Update(user);
}