namespace Exceptions;

public class IncorrectUserLoginCredentialsException(string name, string password) :
    Exception($"Wrong signing credentials : name - {name}, password - {password}") { }