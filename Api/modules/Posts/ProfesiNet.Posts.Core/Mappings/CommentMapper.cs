using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.Commands.Update;
using ProfesiNet.Posts.Core.DAL.Dao;
using ProfesiNet.Posts.Core.DAL.Entities;
using ProfesiNet.Posts.Core.Dto;
using Riok.Mapperly.Abstractions;

namespace ProfesiNet.Posts.Core.Mappings;

[Mapper]
internal partial class CommentMapper
{
    public partial Comment MapCreateCommentCommandToComment(CreateCommentCommand command);


    public partial IReadOnlyList<CommentDetailsDto> MapCommentToCommentDto(IQueryable<CommentDao>? comment);


    public partial CommentDetailsDto MapCommentToCommentDetailsDto(CommentDao commentDao);

    [MapperIgnoreSource(nameof(UpdateCommentCommand.Id))]
    public partial Comment MapAndUpdateCommentCommandToComment(UpdateCommentCommand command);

}