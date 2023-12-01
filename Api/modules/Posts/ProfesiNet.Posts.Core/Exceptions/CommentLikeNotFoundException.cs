using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Posts.Core.Exceptions;

public class CommentLikeNotFoundException(Guid id) :  ProfesiNetException($"comment like {id} not found");