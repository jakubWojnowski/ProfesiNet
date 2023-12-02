using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.Dto;
using ProfesiNet.Posts.Core.Entities;
using Riok.Mapperly.Abstractions;

namespace ProfesiNet.Posts.Core.Mappings;
[Mapper]
internal partial class PostShareMapper
{
    public partial Share MapCreatePostShareCommandToShare(CreatePostShareCommand command);
    
    public partial ShareDetailsDto MapShareToShareDetailsDto(Share share);
    
    public partial IReadOnlyList<ShareDto> MapShareToShareDto(IEnumerable<Share?> share);    
    public partial IReadOnlyList<ShareDetailsDto> MapShareToShareDetailsDto(IEnumerable<Share?> share);    
    
}