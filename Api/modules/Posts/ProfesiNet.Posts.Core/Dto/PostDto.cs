namespace ProfesiNet.Posts.Core.Dto;

internal class PostDto
{
    public Guid Id { get; set; }
    public Guid CreatorId { get; set; }
    public string? CreatorName { get; set; }
    public string? CreatorSurname { get; set; }
    public string? Media { get; set; }
    public string? Description { get; set; }
    public DateTime PublishedAt { get; set; }
    public int LikesCount { get; set; }
    public int CommentsCount { get; set; }
    public int SharesCount { get; set; }
    
    
}