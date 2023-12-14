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
    public string? ProfilePicture { get; set; }

    public IList<Guid> Followings { get; set; } = new List<Guid>();
    public IList<Guid> Followers { get; set; } = new List<Guid>();
    public IList<Guid> NetworkConnections { get; set; } = new List<Guid>();
    public IList<Guid> NetworkConnectionInvitationsReceived { get; set; } = new List<Guid>();
    public IList<Guid> NetworkConnectionInvitationsSent { get; set; } = new List<Guid>();
    
    public virtual ICollection<Experience> Experiences { get; set; } = new List<Experience>();
    public virtual ICollection<Education> Educations { get; set; } = new List<Education>();
 
    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}