using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("users")]
public record User
{
    public const string LastUpdatedTrigger = "trg_user_update_last_updated";
    public const string SetCreatedTrigger = "trg_user_set_created";
    
    [Key]
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
    
    [Column("created")]
    public DateTime Created { get; init; }
}