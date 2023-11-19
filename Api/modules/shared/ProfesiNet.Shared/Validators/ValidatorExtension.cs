using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ProfesiNet.Shared.Validators;


internal static class ValidatorExtension
{
    public static IServiceCollection RegisterValidators(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        var validators = assemblies.SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IValidator).IsAssignableFrom(type) && !type.IsInterface);
        
        var distinctValidationsAssemblies = validators.Select(validator => validator.Assembly).Distinct();

        foreach (var assembly in distinctValidationsAssemblies)
        {
            services.AddValidatorsFromAssembly(assembly);
        }

        return services;
    }
       
}