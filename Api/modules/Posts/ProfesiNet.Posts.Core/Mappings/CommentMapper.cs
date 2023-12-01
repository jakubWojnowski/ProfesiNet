using ProfesiNet.Posts.Core.Dto;
using ProfesiNet.Posts.Core.Entities;
using Riok.Mapperly.Abstractions;

namespace ProfesiNet.Posts.Core.Mappings;
[Mapper]
internal partial class CommentMapper
{
    public partial Comment MapCommentDtoToComment(CommentDto commentDto);
    
    public partial IReadOnlyList<CommentDto> MapCommentToCommentDto(IEnumerable<Comment> comment);
    
    public partial CommentDetailsDto MapCommentToCommentDetailsDto(Comment comment);
    
    public Comment MapAndUpdateCommentDtoToComment(Comment comment, UpdateCommentDto updateCommentDto)
    {
        comment.Content = updateCommentDto.Content;
        comment.PublishedAt = updateCommentDto.PublishedAt;
    
        return comment;
    }
}