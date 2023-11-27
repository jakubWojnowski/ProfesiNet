namespace ProfesiNet.Users.Application.Educations.Dtos;

public class EducationDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Degree { get; set; }
    public string? FieldOfStudy { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    
}