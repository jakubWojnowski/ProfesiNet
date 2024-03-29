﻿using Confab.Shared.Abstractions.Interfaces;
using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.Commands.Delete;
using ProfesiNet.Posts.Core.Commands.Update;
using ProfesiNet.Posts.Core.Dto;
using ProfesiNet.Posts.Core.Exceptions;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Posts.Core.Mappings;
using ProfesiNet.Posts.Core.Policies;
using ProfesiNet.Shared.Contexts;
using ProfesiNet.Shared.Photos;

namespace ProfesiNet.Posts.Core.Services;

internal class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IClock _clock;
    private readonly ICreatorRepository _creatorRepository;
    private readonly IPhotoAccessor _photoAccessor;
    private readonly IContext _context;
    private readonly IPostCannotBeEmptyPolicy _postCannotBeEmptyPolicy;

    private static readonly PostMapper Mapper = new();

    public PostService(IPostRepository postRepository,
        IClock clock, ICreatorRepository creatorRepository, IPhotoAccessor photoAccessor, IContext context, IPostCannotBeEmptyPolicy postCannotBeEmptyPolicy)
    {
        _postRepository = postRepository;
        _clock = clock;
        _creatorRepository = creatorRepository;
        _photoAccessor = photoAccessor;
        _context = context;
        _postCannotBeEmptyPolicy = postCannotBeEmptyPolicy;
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
        
        if(await _postCannotBeEmptyPolicy.CheckPostContentAsync(command, cancellationToken))
        {
            throw new PostCannotHaveNoContentException();
        }   

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

    public async Task<IReadOnlyList<PostDto>> BrowseAsync(Guid creatorId,CancellationToken cancellationToken = default)
    {
        var creator = await _creatorRepository.GetByIdAsync(creatorId, cancellationToken);
        var followingsSet = creator!.Followings.ToHashSet();
        var posts = await _postRepository.GetAllAsync(cancellationToken);
        var newestCreatorPost = posts.Where(p => p.CreatorId == creatorId).OrderByDescending(p => p.PublishedAt)
            .FirstOrDefault();
        
        var sortedPosts = posts
            .OrderByDescending(p => p.Shares != null && (followingsSet.Contains((Guid)p.CreatorId!) || p.Shares.Any(s => followingsSet.Contains(s.CreatorId))))
            .ThenByDescending(p => p.PublishedAt)
            .ToList();
        if (newestCreatorPost == null) return Mapper.MapPostsToPostDtos(sortedPosts);
        sortedPosts.Remove(newestCreatorPost);
        sortedPosts.Insert(0, newestCreatorPost);
       var dtos = Mapper.MapPostsToPostDtos(sortedPosts);
       foreach (var post in dtos)
       {
           post.IsLiked = post.Likes.Any(l => l.CreatorId == creatorId);
           post.IsShared = post.Shares.Any(s => s.CreatorId == creatorId);
           
       }
       return dtos;
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
        var enumerable = posts.ToList();
        var sortedPosts = enumerable.OrderByDescending(p => p.Likes.Count)
            .ThenByDescending(p => p.Shares.Count )
            .ThenByDescending(p => p.PublishedAt).ToList();
        var dtos = Mapper.MapPostsToPostDtos(sortedPosts);
        foreach (var post in dtos)
        {
            post.IsLiked = post.Likes.Any(l => l.CreatorId == creatorId);
            post.IsShared = post.Shares.Any(s => s.CreatorId == creatorId);
           
        }
        return dtos;
        
    }

    public async Task<IReadOnlyList<PostDto>> BrowseAllOwnAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var creator = await _creatorRepository.GetByIdAsync(id, cancellationToken);
        if (creator is null)
        {
            throw new CreatorNotFoundException(id);
        }

        var posts = await _postRepository.GetAllForConditionAsync(p => p.CreatorId == creator.Id, cancellationToken);
        var enumerable = posts.ToList();
        var sortedPosts = enumerable.OrderByDescending(p => p.Likes.Count)
            .ThenByDescending(p => p.Shares.Count )
            .ThenByDescending(p => p.PublishedAt).ToList();
        var dtos = Mapper.MapPostsToPostDtos(sortedPosts);
        foreach (var post in dtos)
        {
            post.IsLiked = post.Likes.Any(l => l.CreatorId == creator.Id);
            post.IsShared = post.Shares.Any(s => s.CreatorId == creator.Id);
           
        }
        return dtos;
    }


    public async Task<Guid> UpdateAsync(UpdatePostCommand command, Guid id, CancellationToken cancellationToken = default)
    {
        var creator = await _creatorRepository.GetByIdAsync(id, cancellationToken);
        if (creator is null)
        {
            throw new CreatorNotFoundException(id);
        }

        var post = await _postRepository.GetRecordByFilterAsync(p => p.CreatorId == creator.Id && p.Id == command.Id, cancellationToken);
        if (post is null)
        {
            throw new PostNotFoundException(command.Id);
        }

        var updatedPost = Mapper.MapUpdatePostCommandToPost(command, post);
        
         if (command?.File is not null)
        {
            if (!string.IsNullOrEmpty(post.ImageId))
            {
                await _photoAccessor.DeletePhoto(post.ImageId);
            }

            var photoUploadResult = await _photoAccessor.AddPhoto(command.File);
            if (photoUploadResult is null)
            {
                throw new PhotoUploadException("Photo upload failed");
            }

            updatedPost.ImageUrl = photoUploadResult.Url;
            updatedPost.ImageId = photoUploadResult.PublicId;
        }
        

        await _postRepository.UpdateAsync(updatedPost, cancellationToken);
        return post.Id;
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