using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Domain.Entities;
using Exceptions;
using Jwt.Abstractions;
using RepositoryAbstractions;
using Service.Contracts;
using Shared.Models;

namespace Service;

public class UserService(IRepositoryManager repositoryManager, IMapper mapper, IJwtManager jwtManager) : IUserService
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly IMapper _mapper = mapper;
    private readonly IJwtManager _jwtManager = jwtManager;
    
    private (string hash, string salt) EncryptPassword(string input)
    {
        var saltBytes = RandomNumberGenerator.GetBytes(64);
        var salt = Convert.ToBase64String(saltBytes);
        
        var hashBytes = Encoding.UTF8.GetBytes(input + salt);
        return
        (
            Convert.ToBase64String(SHA256.HashData(hashBytes)),
            salt
        );
    }

    private async Task<User?> FindUserByNameAsync(string name)
    {
        var user = await _repositoryManager.User.FindUserByNameAsync(name,false);

        return user;
    }

    private bool IsUserPasswordCorrect(User user, string input)
    {
        var inputBytes = Encoding.UTF8.GetBytes(input + user.PasswordSalt);
        var inputHash = Convert.ToBase64String(SHA256.HashData(inputBytes));

        return user.PasswordHash == inputHash;
    }
    
    public async Task CreateUserAsync(UserCreateDto dto)
    {
        var user = _mapper.Map<User>(dto);

        var result = EncryptPassword(dto.Password);

        user = user with
        {
            PasswordHash = result.hash, PasswordSalt = result.salt
        };

        _repositoryManager.User.CreateUser(user);
        
        await _repositoryManager.SaveChangesAsync();
    }
    

    public async Task<string> LoginAsync(UserLoginDto dto)
    {
        var user = await FindUserByNameAsync(dto.Name);

        if (user == null)
            throw new IncorrectUserLoginCredentialsException(dto);

        if (IsUserPasswordCorrect(user, dto.Password) is false)
            throw new IncorrectUserLoginCredentialsException(dto);

        return _jwtManager.CreateToken(user.Id);
    }
    
}