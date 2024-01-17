using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Shared.Interfaces;

namespace ProfesiNet.Shared.Middlewares;

internal static class Extensions
{
    public static IServiceCollection AddErrorHandling(this IServiceCollection services)
        => services.AddScoped<ErrorHandlerMiddleware>()
            .AddSingleton<IExceptionResponseMapper, ExceptionToResponseMapper>()
            .AddSingleton<IExceptionCompositionRoot, ExceptionCompositionRoot>();
        
    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        => app.UseMiddleware<ErrorHandlerMiddleware>();
}