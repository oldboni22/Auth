using Shared.Models;

namespace Service.Contracts;

public interface IUserService
{
    Task CreateUserAsync(UserCreateDto dto);
    Task<string> LoginAsync(UserLoginDto dto);
}