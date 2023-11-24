namespace ProfesiNet.Users.Application.Educations.Dtos;

public class EducationDto
{
    public Guid Id { get; init; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Grade { get; set; }
    public string? FieldOfStudy { get; set; }

    public DateTime StarDate { get; set; }
    public DateTime? EndDate { get; set; }
}