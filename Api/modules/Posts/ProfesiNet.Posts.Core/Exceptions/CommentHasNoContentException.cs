using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Posts.Core.Exceptions;

internal class CommentHasNoContentException() : ProfesiNetException("Comment has no content");