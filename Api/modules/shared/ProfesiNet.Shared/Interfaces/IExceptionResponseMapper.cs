using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Shared.Interfaces;

public interface IExceptionResponseMapper
{
    ExceptionResponse? Map(Exception exception);
}