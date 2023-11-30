using System.Net;

namespace ProfesiNet.Shared.Interfaces;

public record ExceptionResponse(object Response, HttpStatusCode StatusCode);