using System.ComponentModel.DataAnnotations;

namespace Shared.Models;

public record UserLoginDto
{
    [MaxLength(20,ErrorMessage = "A user name must be less than 21 characters.")] 
    public required string Name { get; init; } 
    
    [MaxLength(50,ErrorMessage = "A password must be less than 50 characters.")]
    [DataType(DataType.Password)]
    public required string Password { get; init; } 
}