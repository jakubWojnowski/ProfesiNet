using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Posts.Core.Exceptions;

public class LikeNotFoundException(Guid id) : ProfesiNetException($"like {id} not found");