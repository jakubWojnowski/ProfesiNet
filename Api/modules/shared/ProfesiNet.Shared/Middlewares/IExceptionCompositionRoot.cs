using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Shared.Interfaces;

namespace ProfesiNet.Shared.Middlewares;

internal interface IExceptionCompositionRoot
{
    ExceptionResponse? Map(Exception exception);
}