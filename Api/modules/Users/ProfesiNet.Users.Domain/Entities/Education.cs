namespace ProfesiNet.Users.Domain.Entities;

public class Education
{
    public Guid Id { get; init; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime StarDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid ProfileId { get; set; }
    public Profile Profile { get; set; }
}