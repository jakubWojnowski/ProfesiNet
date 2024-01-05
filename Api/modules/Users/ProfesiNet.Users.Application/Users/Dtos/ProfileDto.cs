using ProfesiNet.Users.Application.Certificates.Dtos;
using ProfesiNet.Users.Application.Educations.Dtos;
using ProfesiNet.Users.Application.Experiences.Dtos;
using ProfesiNet.Users.Application.Photos.Dtos;
using ProfesiNet.Users.Application.Skills.Dtos;

namespace ProfesiNet.Users.Application.Users.Dtos;

public class ProfileDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Title { get; set; }

    public string? Address { get; set; }
    public string? Bio { get; set; }
    public string? ProfilePicture { get; set; }
    public IList<Guid> Followings { get; set; } = new List<Guid>();
    public int FollowingsCount { get; set; }
    public IList<Guid> Followers { get; set; } = new List<Guid>();
    public int FollowersCount { get; set; }
    public IList<Guid> NetworkConnections { get; set; } = new List<Guid>();
    public int NetworkConnectionsCount { get; set; }
    public IList<Guid> NetworkConnectionInvitationsReceived { get; set; } = new List<Guid>();
    public int NetworkConnectionInvitationsReceivedCount { get; set; }
    public IList<Guid> NetworkConnectionInvitationsSent { get; set; } = new List<Guid>();
    public int NetworkConnectionInvitationsSentCount { get; set; }
    
    public virtual ICollection<ExperienceDto> Experiences { get; set; } = new List<ExperienceDto>();
    public virtual ICollection<EducationDto> Educations { get; set; } = new List<EducationDto>();
 
    public virtual ICollection<CertificateDto> Certificates { get; set; } = new List<CertificateDto>();
    public virtual ICollection<SkillDto> Skills { get; set; } = new List<SkillDto>();
}