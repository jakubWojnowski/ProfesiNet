using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.DAL.Entities;
using ProfesiNet.Posts.Core.Dto;
using Riok.Mapperly.Abstractions;

namespace ProfesiNet.Posts.Core.Mappings;
[Mapper]
internal partial class CommentLikeMapper
{
    public partial CommentLike MapCreatePostLikeCommandToComment(CreateCommentLikeCommand command);
    public partial CommentLikeDetailsDto MapCommentLikeToCommentLikeDetailsDto(CommentLike entity);
    
    public partial IReadOnlyList<CommentLikeDto> MapCommentLikeToCommentLikeDto(IEnumerable<CommentLike?> entity);
    

}