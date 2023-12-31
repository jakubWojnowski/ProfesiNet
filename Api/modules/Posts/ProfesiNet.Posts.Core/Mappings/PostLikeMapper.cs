﻿using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.DAL.Entities;
using ProfesiNet.Posts.Core.Dto;
using Riok.Mapperly.Abstractions;

namespace ProfesiNet.Posts.Core.Mappings;
[Mapper]
internal partial class PostLikeMapper
{
    public partial PostLike MapPostLikeCreatePostLikeCommandToPostLike(CreatePostLikeCommand command);
    public partial PostLikeDetailsDto MapPostLikeToPostLikeDetailsDto(PostLike postLike);
    
    public partial IReadOnlyList<PostLikeDto> MapPostLikeToPostLikeDto(IEnumerable<PostLike?> postLikes);
}