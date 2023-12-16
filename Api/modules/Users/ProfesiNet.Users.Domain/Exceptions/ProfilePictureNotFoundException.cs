using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Users.Domain.Enums;

namespace ProfesiNet.Users.Domain.Exceptions;

public class ProfilePictureNotFoundException(Guid id, Guid userId) : ProfesiNetException($"Profile Picture with id {id} not found for user with id {userId}");