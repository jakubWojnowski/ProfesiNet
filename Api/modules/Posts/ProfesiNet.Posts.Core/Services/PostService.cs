﻿using Confab.Shared.Abstractions.Interfaces;
using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.Commands.Delete;
using ProfesiNet.Posts.Core.Commands.Update;
using ProfesiNet.Posts.Core.Dto;
using ProfesiNet.Posts.Core.Exceptions;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Posts.Core.Mappings;
using ProfesiNet.Shared.Photos;

namespace ProfesiNet.Posts.Core.Services;

internal class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IClock _clock;
    private readonly ICreatorRepository _creatorRepository;
    private readonly IPhotoAccessor _photoAccessor;
    private static readonly PostMapper Mapper = new();

    public PostService(IPostRepository postRepository,
        IClock clock, ICreatorRepository creatorRepository, IPhotoAccessor photoAccessor)
    {
        _postRepository = postRepository;
        _clock = clock;
        _creatorRepository = creatorRepository;
        _photoAccessor = photoAccessor;
    }

    public async Task<Guid> AddAsync(CreatePostCommand command, Guid id, CancellationToken cancellationToken = default)
    {
        var post = Mapper.MapCreatePostCommandToPost(command);
        post.PublishedAt = _clock.CurrentDate();
        if (command.File is not null)
        {
            var photoUploadResult = await _photoAccessor.AddPhoto(command.File);
            if (photoUploadResult is null)
            {
                throw new PhotoUploadException("Photo upload failed");
            }

            post.ImageUrl = photoUploadResult.Url;
            post.ImageId = photoUploadResult.PublicId;
        }

        var creator = await _creatorRepository.GetByIdAsync(id, cancellationToken);
        if (creator is null)
        {
            throw new CreatorNotFoundException(id);
        }

        post.CreatorId = creator.Id;

        return await _postRepository.AddAsync(post, cancellationToken);
    }

    public async Task<PostDto?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var post = await _postRepository.GetByIdAsync(id, cancellationToken);
        if (post is null)
        {
            throw new PostNotFoundException(id);
        }

        var dto = Mapper.MapPostToPostDto(post);
        return dto;
    }

    public async Task<IReadOnlyList<PostDto>> BrowseAsync(CancellationToken cancellationToken = default)
    {
        var posts = await _postRepository.GetAllAsync(cancellationToken);
        var mappedPosts = Mapper.MapPostsToPostDtos(posts).OrderByDescending(p => p.PublishedAt).ToList();
        return mappedPosts;
    }

    public async Task<IReadOnlyList<PostDto>> BrowsePerCreatorAsync(Guid creatorId,
        CancellationToken cancellationToken = default)
    {
        var creator = await _creatorRepository.GetByIdAsync(creatorId, cancellationToken);
        if (creator is null)
        {
            throw new CreatorNotFoundException(creatorId);
        }

        var posts = await _postRepository.GetAllForConditionAsync(p => p.CreatorId == creator.Id, cancellationToken);
        return Mapper.MapPostsToPostDtos(posts);
    }

    public async Task<IReadOnlyList<PostDto>> BrowseAllOwnAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var creator = await _creatorRepository.GetByIdAsync(id, cancellationToken);
        if (creator is null)
        {
            throw new CreatorNotFoundException(id);
        }

        var posts = await _postRepository.GetAllForConditionAsync(p => p.CreatorId == creator.Id, cancellationToken);
        return Mapper.MapPostsToPostDtos(posts);
    }


    public async Task UpdateAsync(UpdatePostCommand command, Guid id, CancellationToken cancellationToken = default)
    {
        var creator = await _creatorRepository.GetByIdAsync(id, cancellationToken);
        if (creator is null)
        {
            throw new CreatorNotFoundException(id);
        }

        var post = await _postRepository.GetRecordByFilterAsync(p => p.CreatorId == creator.Id && p.Id == command.Id,
            cancellationToken);

        if (post is null)
        {
            throw new PostNotFoundException(command.Id);
        }

        var updatedPost = Mapper.MapUpdatePostCommandToPost(command, post);
        if (command.File is not null)
        {
            if (post.ImageId != null) await _photoAccessor.DeletePhoto(post.ImageId);

            var photoUploadResult = await _photoAccessor.AddPhoto(command.File);
            if (photoUploadResult is null)
            {
                throw new PhotoUploadException("Photo upload failed");
            }

            updatedPost.ImageUrl = photoUploadResult.Url;
            updatedPost.ImageId = photoUploadResult.PublicId;
        }

        await _postRepository.UpdateAsync(updatedPost, cancellationToken);
    }

    public async Task DeleteAsync(DeletePostCommand command, Guid id, CancellationToken cancellationToken = default)
    {
        var creator = await _creatorRepository.GetByIdAsync(id, cancellationToken);
        if (creator is null)
        {
            throw new CreatorNotFoundException(id);
        }

        var post = await _postRepository.GetRecordByFilterAsync(
            p => p.CreatorId == creator.Id && p.Id == command.PostId,
            cancellationToken);

        if (post is null)
        {
            throw new PostNotFoundException(command.PostId);
        }

        if (post.ImageId != null)
        {
            await _photoAccessor.DeletePhoto(post.ImageId);
        }

        await _postRepository.DeleteAsync(post, cancellationToken);
    }
}