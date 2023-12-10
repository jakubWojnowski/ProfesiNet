using ProfesiNet.Posts.Core.Commands;
using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.Commands.Update;
using ProfesiNet.Posts.Core.DAL.Entities;
using ProfesiNet.Posts.Core.Dto;

namespace ProfesiNet.Posts.Core.Mappings;

using Riok.Mapperly.Abstractions;

[Mapper]
internal partial class PostMapper
{
   
    public partial PostDto MapPostToPostDto(Post post);
    
    public partial Post MapCreatePostCommandToPost(CreatePostCommand createPostCommand);

    public partial IReadOnlyList<PostDto> MapPostsToPostDtos(IEnumerable<Post?> posts);
    

    [MapperIgnoreSource(nameof(UpdatePostCommand.Id))]
    public partial Post MapAndUpdateUpdatePostCommandToPost(UpdatePostCommand command);


}