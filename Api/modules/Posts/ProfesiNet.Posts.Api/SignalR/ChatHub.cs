using Microsoft.AspNetCore.SignalR;
using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Shared.Contexts;

namespace ProfesiNet.Posts.Api.SignalR;

internal class ChatHub : Hub
{
    private readonly ICommentService _commentService;
    private readonly IContext _context;

    public ChatHub(ICommentService commentService, IContext context)
    {
        _commentService = commentService;
        _context = context;
    }

    public async Task SendComment(CreateCommentCommand command, CancellationToken cancellationToken = default)
    {
        var id = await _commentService.AddAsync(command, _context.Id, cancellationToken);
        await Clients.All.SendAsync("ReceiveComment", id, cancellationToken: cancellationToken);
    }

}