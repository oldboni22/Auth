namespace Exceptions;

public class UserAlreadyExistsException(string name) : Exception($"A user with name {name} already exists.")
{
    
}