using ProfesiNet.Posts.Core.Dto;
using ProfesiNet.Posts.Core.Entities;

namespace ProfesiNet.Posts.Core.Mappings;

using Riok.Mapperly.Abstractions;

[Mapper]
internal partial class PostMapper
{
    public partial Post MapPostDtoToPost(PostDto postDto);
    public partial PostDto MapPostToPostDto(Post post);

    public partial IReadOnlyList<PostDto> MapPostsToPostDtos(IEnumerable<Post> posts);

    public Post MapAndUpdatePostDtoToPost(Post post, UpdatePostDto updatePostDto)
    {
        post.Description = updatePostDto.Description;
        post.Media = updatePostDto.Media;
        post.PublishedAt = updatePostDto.CreatedAt;

        return post;
    }
}