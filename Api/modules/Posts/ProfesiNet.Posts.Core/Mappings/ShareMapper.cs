using ProfesiNet.Posts.Core.Dto;
using ProfesiNet.Posts.Core.Entities;
using Riok.Mapperly.Abstractions;

namespace ProfesiNet.Posts.Core.Mappings;
[Mapper]
internal partial class ShareMapper
{
    public partial Share MapShareDetailsDtoToShare(ShareDetailsDto shareDetailsDto);
    
    public partial ShareDetailsDto MapShareToShareDetailsDto(Share share);
    
    public partial IReadOnlyList<ShareDto> MapShareToShareDto(IEnumerable<Share?> share);    
    
}