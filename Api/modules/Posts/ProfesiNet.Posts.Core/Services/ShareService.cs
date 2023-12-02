using ProfesiNet.Posts.Core.Dto;
using ProfesiNet.Posts.Core.Exceptions;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Posts.Core.Mappings;
using ProfesiNet.Posts.Core.Policies;

namespace ProfesiNet.Posts.Core.Services;

internal class ShareService : IShareService
{
    private readonly IShareRepository _shareRepository;
    private readonly IUserCantSharePolicy _userCantSharePolicy;
    private readonly IPostRepository _postRepository;
    private static readonly ShareMapper Mapper = new();

    public ShareService(IShareRepository shareRepository, IUserCantSharePolicy userCantSharePolicy,
        IPostRepository postRepository)
    {
        _shareRepository = shareRepository;
        _userCantSharePolicy = userCantSharePolicy;
        _postRepository = postRepository;
    }

    public async Task AddAsync(ShareDetailsDto shareDetailsDto, CancellationToken ct = default)
    {
        var shareDto = Mapper.MapShareDetailsDtoToShare(shareDetailsDto);
        shareDto.Id = Guid.NewGuid();
        if (await _postRepository.GetByIdAsync(shareDto.PostId, ct) == null)
        {
            throw new PostNotFoundException(shareDto.PostId);
        }

        if (!await _userCantSharePolicy.CheckShareAsync(shareDto.CreatorId, shareDto.PostId, ct))
        {
            throw new UserCannotSharePostException(shareDto.CreatorId, shareDto.PostId);
        }

        await _shareRepository.AddAsync(shareDto, ct);
    }
    
    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var share = await _shareRepository.GetByIdAsync(id, ct);
        if (share == null)
        {
            throw new ShareNotFoundException(id);
        }

        await _shareRepository.DeleteAsync(share, ct);
    }
    
    public async Task<ShareDetailsDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var share = await _shareRepository.GetByIdAsync(id, ct);
        if (share == null)
        {
            throw new ShareNotFoundException(id);
        }

        return Mapper.MapShareToShareDetailsDto(share);
    }
    
    public async Task<IReadOnlyList<ShareDto>> BrowseAsync(Guid postId, CancellationToken ct = default)
    {
        if (await _postRepository.GetByIdAsync(postId, ct) == null)
        {
            throw new PostNotFoundException(postId);
        }
        var shares = await _shareRepository.GetAllForConditionAsync(p => p.PostId == postId, ct);
        return Mapper.MapShareToShareDto(shares);
    }
}