using ProfesiNet.Posts.Core.Dto;
using ProfesiNet.Posts.Core.Entities;
using Riok.Mapperly.Abstractions;

namespace ProfesiNet.Posts.Core.Mappings;
[Mapper]
internal partial class PostLikeMapper
{
    public partial PostLike MapPostLikeDetailsDtoToPostLike(PostLikeDetailsDto postLikeDetailsDto);
    public partial PostLikeDetailsDto MapPostLikeToPostLikeDetailsDto(PostLike postLike);
    
    public partial IReadOnlyList<PostLikeDto> MapPostLikeToPostLikeDto(IEnumerable<PostLike?> postLikes);
}