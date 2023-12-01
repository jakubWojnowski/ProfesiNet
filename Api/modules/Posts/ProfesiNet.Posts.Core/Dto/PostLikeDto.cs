namespace ProfesiNet.Posts.Core.Dto;

public class PostLikeDto
{ 
    public Guid Id { get; set; }
}

public class PostLikeDetailsDto : PostLikeDto
{
    public Guid CreatorId { get; set; }
    public Guid PostId { get; set; }
}