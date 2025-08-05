using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("users")]
public class User
{
    [Column("user_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required int Id { get; init; }

    [MaxLength(20)]
    [Column("user_name")]
    public required string Name { get; init; }
    
    [Column("password_hash")]
    public string PasswordHash { get; init; } = string.Empty;
    
    [Column("password_salt")]
    public string PasswordSalt { get; init; } = string.Empty;

    [Column("last_updated")] 
    public DateTime LastUpdated { get; init; }
}