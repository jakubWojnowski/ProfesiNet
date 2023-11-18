using MediatR;
using ProfesiNet.Posts.Application.Posts.Dtos;

namespace ProfesiNet.Posts.Application.Posts.Commands.Create;

public record CreatePostCommand(AddPostDto AddPostDto) : IRequest<Guid>;