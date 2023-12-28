using Microsoft.AspNetCore.Http;

namespace ProfesiNet.Posts.Core.Commands.Create;

internal record CreatePostCommand(IFormFile? File, string? Description);