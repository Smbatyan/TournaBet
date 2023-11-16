using TournaBet.Shared.Repository;

namespace TournaBet.Shared.Models;

public class PlayerEntity : BaseEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}