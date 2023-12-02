using ProfesiNet.Posts.Core.Commands;
using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.Commands.Update;
using ProfesiNet.Posts.Core.Dto;
using ProfesiNet.Posts.Core.Entities;

namespace ProfesiNet.Posts.Core.Mappings;

using Riok.Mapperly.Abstractions;

[Mapper]
internal partial class PostMapper
{
   
    public partial PostDto MapPostToPostDto(Post post);
    
    public partial Post MapCreatePostCommandToPost(CreatePostCommand createPostCommand);

    public partial IReadOnlyList<PostDto> MapPostsToPostDtos(IEnumerable<Post?> posts);

    public Post MapAndUpdateUpdatePostCommandToPost(Post post, UpdatePostCommand command)
    {
        post.Description = command.Description;
        post.Media = command.Media;

        return post;
    }
}