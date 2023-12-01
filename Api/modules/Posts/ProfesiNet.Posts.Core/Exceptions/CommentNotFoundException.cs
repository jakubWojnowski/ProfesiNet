using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Posts.Core.Exceptions;

internal class CommentNotFoundException(Guid id) : ProfesiNetException($"Comment with id {id} not found");
