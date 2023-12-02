﻿using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.Commands.Delete;
using ProfesiNet.Posts.Core.Dto;
using ProfesiNet.Posts.Core.Exceptions;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Posts.Core.Mappings;
using ProfesiNet.Posts.Core.Policies;
using ProfesiNet.Posts.Core.Repositories;
using ProfesiNet.Shared.UserContext;

namespace ProfesiNet.Posts.Core.Services;

internal class PostLikeService : IPostLikeService
{
    private readonly IPostLikeRepository _postLikeRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserCantAddLikeToPostPolicy _userCantAddLikeToPostPolicy;
    private readonly ICurrentUserContextService _currentUserContextService;
    private static readonly PostLikeMapper Mapper = new();

    public PostLikeService(IPostLikeRepository postLikeRepository, IPostRepository postRepository,
        IUserCantAddLikeToPostPolicy userCantAddLikeToPostPolicy, ICurrentUserContextService currentUserContextService)
    {
        _postLikeRepository = postLikeRepository;
        _postRepository = postRepository;
        _userCantAddLikeToPostPolicy = userCantAddLikeToPostPolicy;
        _currentUserContextService = currentUserContextService;
    }

    public async Task<Guid> AddAsync(CreatePostLikeCommand command, CancellationToken cancellationToken = default)
    {
        var creatorId = Guid.Parse(_currentUserContextService.GetCurrentUser()!.Id!);
        var post = await _postRepository.GetByIdAsync(command.PostId, cancellationToken);
        var postLike = Mapper.MapPostLikeCreatePostLikeCommandToPostLike(command with
        {
            Id = Guid.NewGuid(),
        });
        postLike.CreatorId = creatorId;
        if (post is null)
        {
            throw new PostNotFoundException(command.PostId);
        }

        if (!await _userCantAddLikeToPostPolicy.CheckPostLikeAsync(creatorId,
                command.PostId, cancellationToken))
        {
            throw new UserCannotLikeException(postLike.CreatorId, postLike.PostId);
        }

        postLike.Id = Guid.NewGuid();


        return await _postLikeRepository.AddAsync(postLike, cancellationToken);
    }

    public async Task DeleteAsync(DeletePostLikeCommand command, CancellationToken cancellationToken = default)
    {
        var creatorId = Guid.Parse(_currentUserContextService.GetCurrentUser()!.Id!);
        var postLike =
            await _postLikeRepository.GetRecordByFilterAsync(l => l.Id == command.Id && l.CreatorId == creatorId,
                cancellationToken);
        if (postLike is null)
        {
            throw new PostLikeNotFoundException(command.Id);
        }

        await _postLikeRepository.DeleteAsync(postLike, cancellationToken);
    }

    public async Task<IReadOnlyList<PostLikeDto>> BrowseAsync(Guid postId,
        CancellationToken cancellationToken = default)
    {
        var postLikes = await _postLikeRepository.GetAllForConditionAsync(p => p.PostId == postId, cancellationToken);
        return Mapper.MapPostLikeToPostLikeDto(postLikes);
    }

    public async Task<PostLikeDetailsDto> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var postLike = await _postLikeRepository.GetByIdAsync(id, cancellationToken);
        if (postLike is null)
        {
            throw new PostLikeNotFoundException(id);
        }

        var dto = Mapper.MapPostLikeToPostLikeDetailsDto(postLike);
        return dto;
    }
}