namespace ProfesiNet.Posts.Core.Dto;

public class CommentDto
{
    public Guid Id { get; set; }
    public Guid CreatorId { get; set; }
    public Guid PostId { get; set; }
    public string? Content { get; set; }
    public DateTime? PublishedAt { get; set; }
    
}