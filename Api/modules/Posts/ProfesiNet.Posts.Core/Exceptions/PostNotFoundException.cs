using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Posts.Core.Exceptions;

public class PostNotFoundException(Guid id) : ProfesiNetException($"Post with id {id} not found");
