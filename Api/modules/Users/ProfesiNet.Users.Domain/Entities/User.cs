namespace ProfesiNet.Users.Domain.Entities;

public class User
{
    public Guid Id { get; init; }
    public string Email { get; set; } = null!;
    public string EncodedPassword { get; set; } = null!;
    public string? Name { get; set; }
    public string? Surname { get; set; }

    public string? Address { get; set; }
    public string? Bio { get; set; }
    
    public virtual ICollection<Connection> ProfileConnections { get; set; } = new List<Connection>();
    public virtual ICollection<Connection> FriendConnections { get; set; } = new List<Connection>();
    public virtual ICollection<ConnectionRequest> ProfileConnectionRequests { get; set; } = new List<ConnectionRequest>();
    public virtual ICollection<ConnectionRequest> SenderConnectionRequests { get; set; } = new List<ConnectionRequest>();
    public virtual ICollection<Experience> Experiences { get; set; } = new List<Experience>();
    public virtual ICollection<Education> Educations { get; set; } = new List<Education>();
    public virtual ICollection<Following> ObserverFollowings { get; set; } = new List<Following>();
    public virtual ICollection<Following> TargetFollowings { get; set; } = new List<Following>();
    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}