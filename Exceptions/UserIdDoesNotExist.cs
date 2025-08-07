namespace Exceptions;

public class UserIdDoesNotExist(int id) : Exception($"User with id {id} does not exist.")
{
    
}