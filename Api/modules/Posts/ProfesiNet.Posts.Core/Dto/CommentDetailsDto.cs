namespace ProfesiNet.Posts.Core.Dto;

internal class CommentDetailsDto : CommentDto
{
    public virtual ICollection<CommentLikeDto> Likes { get; set; }
}