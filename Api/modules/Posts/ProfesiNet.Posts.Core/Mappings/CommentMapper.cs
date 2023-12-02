﻿using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.Commands.Update;
using ProfesiNet.Posts.Core.Dto;
using ProfesiNet.Posts.Core.Entities;
using Riok.Mapperly.Abstractions;

namespace ProfesiNet.Posts.Core.Mappings;
[Mapper]
internal partial class CommentMapper
{
    public partial Comment MapCreateCommentCommandToComment(CreateCommentCommand command);
    
    public partial IReadOnlyList<CommentDto> MapCommentToCommentDto(IEnumerable<Comment?> comment);
    
    public partial CommentDetailsDto MapCommentToCommentDetailsDto(Comment comment);
    
    public Comment MapAndUpdateCommentCommandToComment(Comment comment, UpdateCommentCommand command)
    {
        comment.Content = command.Content;
    
        return comment;
    }
}