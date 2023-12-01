using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Posts.Core.Exceptions;

internal class PostLikeNotFoundException(Guid id) : ProfesiNetException($"Post like with id {id} not found");