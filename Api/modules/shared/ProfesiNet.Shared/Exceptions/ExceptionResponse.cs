using System.Net;

namespace ProfesiNet.Shared.Exceptions;

public record ExceptionResponse(object Response, HttpStatusCode StatusCode);