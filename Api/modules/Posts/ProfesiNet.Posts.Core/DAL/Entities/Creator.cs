namespace ProfesiNet.Posts.Core.DAL.Entities;

public class Creator
{
    public Guid Id { get; init; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Org { get; set; }
}