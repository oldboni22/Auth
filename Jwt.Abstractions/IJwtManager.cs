namespace Jwt.Abstractions;

public interface IJwtManager
{
    string CreateToken(int userId);
}