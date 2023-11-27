namespace ProfesiNet.Users.Domain.Entities;

public class Education
{
    public Guid Id { get; init; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Degree { get; set; }
    public string? FieldOfStudy { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}