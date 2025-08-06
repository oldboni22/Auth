using System.ComponentModel.DataAnnotations;

namespace Shared.Models;

public record UserLoginDto
{
    [MaxLength(20)] 
    public required string Name { get; init; } 
    
    [MaxLength(50)]
    [DataType(DataType.Password)]
    public required string Password { get; init; } 
}