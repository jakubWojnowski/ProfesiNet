using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Posts.Core.Exceptions;

public class ShareNotFoundException(Guid id) : ProfesiNetException($"Share {id} not found");