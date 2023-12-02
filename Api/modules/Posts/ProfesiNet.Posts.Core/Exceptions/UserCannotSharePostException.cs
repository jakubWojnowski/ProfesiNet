using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Posts.Core.Exceptions;

internal class UserCannotSharePostException(Guid userId, Guid postId) : ProfesiNetException($"User {userId} cannot share post {postId}")
{
    
}