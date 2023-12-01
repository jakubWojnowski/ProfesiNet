using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Posts.Core.Exceptions;

public class UserCannotLikeException(Guid userId, Guid commentId) : ProfesiNetException($"user {userId} already liked comment {commentId}");