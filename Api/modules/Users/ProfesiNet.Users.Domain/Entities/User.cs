namespace ProfesiNet.Users.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string EncodedPassword { get; set; } = null!;
    public string? Name { get; set; }
    public string? Surname { get; set; }

    public string? Address { get; set; }
    public string? Bio { get; set; }
    
    public virtual ICollection<NetworkConnection>? NetworkConnectionsReceived { get; set; } = new List<NetworkConnection>();
    public virtual ICollection<NetworkConnection>?  NetworkConnectionsSend { get; set; } = new List<NetworkConnection>();
    public virtual ICollection<Experience> Experiences { get; set; } = new List<Experience>();
    public virtual ICollection<Education> Educations { get; set; } = new List<Education>();
   
    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}