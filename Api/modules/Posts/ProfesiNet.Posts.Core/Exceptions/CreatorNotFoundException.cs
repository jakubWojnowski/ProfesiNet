using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Posts.Core.Exceptions;

internal class CreatorNotFoundException(Guid id) : ProfesiNetException($"Creator {id} not found");
