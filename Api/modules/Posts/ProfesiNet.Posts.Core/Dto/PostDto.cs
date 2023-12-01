namespace ProfesiNet.Posts.Core.Dto;

internal class PostDto
{
    public Guid Id { get; set; }
    public Guid CreatorId { get; set; }
    public string? Media { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } 
}