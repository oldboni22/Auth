using Shared.Models;

namespace Exceptions;

public class IncorrectUserLoginCredentialsException(UserLoginDto dto) :
    Exception($"Wrong signing credentials : name - {dto.Name}, password - {dto.Password}") { }