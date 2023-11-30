using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Shared.Interfaces;

namespace ProfesiNet.Shared.Middlewares;

class ExceptionCompositionRoot : IExceptionCompositionRoot
{
    private readonly IServiceProvider _serviceProvider;

    public ExceptionCompositionRoot(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public ExceptionResponse? Map(Exception exception)
    {
        using var scope = _serviceProvider.CreateScope();
        var mappers = scope.ServiceProvider.GetServices<IExceptionResponseMapper>().ToArray();
        var nondefaultMappers = mappers.Where(x => x is not ExceptionToResponseMapper);
       var result = nondefaultMappers.Select(x => x.Map(exception))
           .SingleOrDefault(x => true);
       if (result is not null)
       {
           return result;
       }
       var defaultMapper = mappers.SingleOrDefault(x => x is ExceptionToResponseMapper);
       
       return defaultMapper?.Map(exception);
    }
}