using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.SignalR;
using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Shared.Contexts;
[assembly: InternalsVisibleTo("ProfesiNetApi")]

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

    public async Task SendComment(CreateCommentCommand command)
    {
        var comment = await _commentService.AddAsync(command);
        await Clients.Group(command.PostId.ToString()).SendAsync("ReceiveComment", comment);
    }
    
    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var postId = httpContext?.Request.Query["postId"];
        if (postId is not null) await Groups.AddToGroupAsync(Context.ConnectionId, postId!);
        var result = await _commentService.BrowseAsync(Guid.Parse(postId!), CancellationToken.None);
        
        await Clients.Caller.SendAsync("LoadComments", result, cancellationToken: CancellationToken.None);
    }
    

}