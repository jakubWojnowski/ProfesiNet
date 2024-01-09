namespace ProfesiNet.Users.Domain.Entities;

public class Education
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Degree { get; set; }
    public string? FieldOfStudy { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}