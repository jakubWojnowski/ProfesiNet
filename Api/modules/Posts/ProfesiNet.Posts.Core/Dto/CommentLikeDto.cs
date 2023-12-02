namespace ProfesiNet.Posts.Core.Dto;

public class CommentLikeDto
{
    public Guid Id { get; set; }
}

public class CommentLikeDetailsDto : CommentLikeDto
{
    public Guid CreatorId { get; set; }
    public Guid CommentId { get; set; }
}