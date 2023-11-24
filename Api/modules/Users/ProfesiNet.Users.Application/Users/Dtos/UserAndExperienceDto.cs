using ProfesiNet.Users.Application.Experiences.Dtos;

namespace ProfesiNet.Users.Application.Users.Dtos;

public class UserAndExperienceDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }

    public string? Address { get; set; }
    public string? Bio { get; set; }
    public IReadOnlyCollection<GetExperienceDto>? Experiences { get; set; }
}