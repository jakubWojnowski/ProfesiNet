namespace ProfesiNet.Users.Application.Experiences.Dtos;

public class ExperienceDto
{
    
    public string? Company { get; set; }
    public string? Position { get; set; }
    public string? Description { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}