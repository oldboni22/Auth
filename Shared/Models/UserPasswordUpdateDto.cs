using System.ComponentModel.DataAnnotations;

namespace Shared.Models;

public record UserPasswordUpdateDto
{
    public required string UserIdAsString { get; init; }
    
    [MaxLength(50,ErrorMessage = "A password must be less than 50 characters.")]
    [DataType(DataType.Password)]
    public required string NewPassword { get; init; }
};