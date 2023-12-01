using ProfesiNet.Posts.Core.Dto;
using ProfesiNet.Posts.Core.Entities;
using Riok.Mapperly.Abstractions;

namespace ProfesiNet.Posts.Core.Mappings;
[Mapper]
internal partial class CommentLikeMapper
{
    public partial CommentLike MapCommentLikeDetailsDtoToComment(CommentLikeDetailsDto commentLikeDetailsDto);
    public partial CommentLikeDetailsDto MapCommentLikeToCommentLikeDetailsDto(CommentLike entity);
    
    public partial IReadOnlyList<CommentLikeDto> MapCommentLikeToCommentLikeDto(IEnumerable<CommentLike?> entity);
    

}