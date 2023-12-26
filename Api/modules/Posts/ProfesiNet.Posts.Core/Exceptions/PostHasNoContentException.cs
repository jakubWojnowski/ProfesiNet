using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Posts.Core.Exceptions;

internal class PostHasNoContentException() : ProfesiNetException ("Post has no content");