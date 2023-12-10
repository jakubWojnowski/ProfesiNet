namespace ProfesiNet.Posts.Core.Dto;

internal class CreatorDto
{
    public Guid Id { get; init; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Org { get; set; }
}