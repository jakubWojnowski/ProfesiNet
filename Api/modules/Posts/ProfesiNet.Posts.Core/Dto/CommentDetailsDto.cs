namespace ProfesiNet.Posts.Core.Dto;

public class CommentDetailsDto : CommentDto
{
    public virtual ICollection<CommentLikeDto> Likes { get; set; }
}