namespace ProfesiNet.Users.Domain.Entities;

public class Experience
{
    public Guid Id { get; init; }
    public string? Company { get; set; }
    public string? Position { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid ProfileId { get; set; }
    public Profile Profile { get; set; }
    
}