﻿using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.Commands.Delete;
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
    private static readonly PostShareMapper Mapper = new();

    public ShareService(IShareRepository shareRepository, IUserCantSharePolicy userCantSharePolicy,
        IPostRepository postRepository)
    {
        _shareRepository = shareRepository;
        _userCantSharePolicy = userCantSharePolicy;
        _postRepository = postRepository;
    }

    public async Task<Guid> AddAsync(CreatePostShareCommand command, Guid id, CancellationToken ct = default)
    {
        var creatorId = id;
        var share = Mapper.MapCreatePostShareCommandToShare(command with
        {
            Id = Guid.NewGuid()
        });
        share.CreatorId = creatorId;

        if (await _postRepository.GetRecordByFilterAsync(s => s.Id == command.PostId, ct) == null)
        {
            throw new PostNotFoundException(command.PostId);
        }

        if (!await _userCantSharePolicy.CheckShareAsync(share.CreatorId, share.PostId, ct))
        {
            throw new UserCannotSharePostException(share.CreatorId, share.PostId);
        }

        return await _shareRepository.AddAsync(share, ct);
    }

    public async Task DeleteAsync(DeletePostShareCommand command, Guid id, CancellationToken ct = default)
    {
        var creatorId = id;
        var share = await _shareRepository.GetRecordByFilterAsync(s => s.CreatorId == creatorId,
            ct);
        if (share == null)
        {
            throw new ShareNotFoundException(command.Id);
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

    public async Task<IReadOnlyList<ShareDto>> BrowseSharesPerPostAsync(Guid postId, CancellationToken ct = default)
    {
        if (await _postRepository.GetByIdAsync(postId, ct) == null)
        {
            throw new PostNotFoundException(postId);
        }

        var shares = await _shareRepository.GetAllForConditionAsync(p => p.PostId == postId, ct);
        return Mapper.MapShareToShareDto(shares);
    }

    public async Task<IReadOnlyList<ShareDetailsDto>> BrowseSharesPerUserAsync(Guid creatorId,
        CancellationToken ct = default)
    {
        var shares = await _shareRepository.GetAllForConditionAsync(p => p.CreatorId == creatorId, ct);
        return Mapper.MapShareToShareDetailsDto(shares);
    }
}