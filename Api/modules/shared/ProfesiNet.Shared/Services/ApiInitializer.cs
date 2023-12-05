using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ProfesiNet.Shared.Services;

public class ApiInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ApiInitializer> _logger;

    public ApiInitializer(IServiceProvider serviceProvider, ILogger<ApiInitializer> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(DbContext).IsAssignableFrom(x) && x != typeof(DbContext));

        using var scope = _serviceProvider.CreateScope();
        foreach (var dbContextType in dbContextTypes)
        {
            if (scope.ServiceProvider.GetRequiredService(dbContextType) is DbContext dbContext) await dbContext.Database.MigrateAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}