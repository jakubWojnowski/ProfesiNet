using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Posts.Core.Exceptions;

internal class PhotoUploadException(string message) : ProfesiNetException(message);
