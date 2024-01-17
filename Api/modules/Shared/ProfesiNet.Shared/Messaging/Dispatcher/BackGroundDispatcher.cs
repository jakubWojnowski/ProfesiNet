using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProfesiNet.Shared.Modules;

namespace ProfesiNet.Shared.Messaging.Dispatcher;

internal sealed class BackGroundDispatcher : BackgroundService
{
    private readonly IMessageChannel _messageChannel;
    private readonly IModuleClient _moduleClient;
    private readonly ILogger<BackGroundDispatcher> _logger;

    public BackGroundDispatcher(IMessageChannel messageChannel, IModuleClient moduleClient, ILogger<BackGroundDispatcher> logger)
    {
        _messageChannel = messageChannel;
        _moduleClient = moduleClient;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Running the Background Dispatcher.");
        await foreach (var message in _messageChannel.Reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                await _moduleClient.PublishAsync(message);
            }
            catch(Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }
        }
        _logger.LogInformation("Finished the Background Dispatcher.");   
    }
}