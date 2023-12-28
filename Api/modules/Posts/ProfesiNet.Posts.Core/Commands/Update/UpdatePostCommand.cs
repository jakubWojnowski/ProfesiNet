using Microsoft.AspNetCore.Http;

namespace ProfesiNet.Posts.Core.Commands.Update;

internal record UpdatePostCommand(Guid Id, IFormFile? File, string? Description);

