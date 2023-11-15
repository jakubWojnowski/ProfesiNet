using MediatR;
using ProfesiNet.Posts.Application.Posts.Mappings;
using ProfesiNet.Posts.Domain.Interfaces;

namespace ProfesiNet.Posts.Application.Posts.Commands.Create;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
{
    private readonly IPostRepository _postRepository;
    private static readonly PostMapper Mapper = new();

    public CreatePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = Mapper.MapAddPostDtoToPost(request.AddPostDto);
        post.CreatedAt = DateTime.Now;
        await _postRepository.AddAsync(post, cancellationToken);
        return post.Id;
        
    }
}