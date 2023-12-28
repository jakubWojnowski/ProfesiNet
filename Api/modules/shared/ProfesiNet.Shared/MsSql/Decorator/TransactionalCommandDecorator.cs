using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Shared.Commands;

namespace ProfesiNet.Shared.MsSql.Decorator;
[Decorator]
internal class TransactionalCommandDecorator<T> : ICommandHandler<T> where T : class, ICommand
{
    private readonly UnitOfWorkTypeRegistry _unitOfWorkTypeRegistry;
    private readonly IServiceProvider _serviceProvider;
    private readonly ICommandHandler<T> _commandHandler;

    public TransactionalCommandDecorator(ICommandHandler<T> commandHandler, UnitOfWorkTypeRegistry unitOfWorkTypeRegistry, IServiceProvider serviceProvider)
    {
        _unitOfWorkTypeRegistry = unitOfWorkTypeRegistry;
        _serviceProvider = serviceProvider;
        _commandHandler = commandHandler;
    }
    public async Task HandleAsync(T command, CancellationToken cancellationToken = default)
    {
        var unitOfWorkType = _unitOfWorkTypeRegistry.Resolve<T>();
        if (unitOfWorkType is null)
        {
            await _commandHandler.HandleAsync(command, cancellationToken);
            return;
        }
        var unitOfWork = (IUnitOfWork)_serviceProvider.GetRequiredService(unitOfWorkType);
        await unitOfWork.ExecuteAsync(async () => await _commandHandler.HandleAsync(command, cancellationToken), cancellationToken);
      
    }
}