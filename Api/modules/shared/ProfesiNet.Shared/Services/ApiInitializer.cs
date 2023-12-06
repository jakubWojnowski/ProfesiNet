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
        try
        {
            var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(DbContext).IsAssignableFrom(x) && !x.IsAbstract && x != typeof(DbContext));

            using var scope = _serviceProvider.CreateScope();
            foreach (var dbContextType in dbContextTypes)
            {
                if (scope.ServiceProvider.GetRequiredService(dbContextType) is not DbContext dbContext) continue;
                _logger.LogInformation($"Starting database migration for {dbContextType.Name}...");
                await dbContext.Database.MigrateAsync(cancellationToken);
                _logger.LogInformation($"Database migration for {dbContextType.Name} completed.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while migrating the database.");
            // Rethrow the exception if you want to stop the app startup
            throw;
        }
    }


    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}