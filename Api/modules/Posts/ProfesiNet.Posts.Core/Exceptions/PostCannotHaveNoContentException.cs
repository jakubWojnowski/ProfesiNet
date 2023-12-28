using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Posts.Core.Exceptions;

internal class PostCannotHaveNoContentException() : ProfesiNetException ("Post has no content");