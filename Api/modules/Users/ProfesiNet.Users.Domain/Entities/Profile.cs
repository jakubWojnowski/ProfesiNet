using Microsoft.VisualBasic.CompilerServices;

namespace ProfesiNet.Users.Domain.Entities;

public class Profile
{
    public Guid Id { get; init; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? Bio { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public ICollection<Connection> Connections { get; set; } = new List<Connection>();
    public ICollection<ConnectionRequest> ConnectionRequests { get; set; } = new List<ConnectionRequest>();
    public ICollection<Experience> Experiences { get; set; } = new List<Experience>();
    public ICollection<Education> Educations { get; set; } = new List<Education>();
    public ICollection<Following> Followings { get; set; } = new List<Following>();
    
    
}