namespace ProfesiNet.Users.Domain.Entities;

public class Experience
{
    public Guid Id { get; init; }
    public string? Company { get; set; }
    public string? Position { get; set; }
    public string? Description { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    
}