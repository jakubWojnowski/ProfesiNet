using ProfesiNet.Posts.Application.Posts.Dtos;
using ProfesiNet.Posts.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ProfesiNet.Posts.Application.Posts.Mappings;
[Mapper]
public partial class PostMapper
{
    public partial Post MapAddPostDtoToPost(AddPostDto addPostDto);
    public partial AddPostDto MapPostToAddPostDto(Post post);
}