using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Users.Application.Users.Validations.Validators;
using ProfesiNet.Users.Domain.Entities;

namespace ProfesiNet.Users.Application.Configurations.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        //services.AddValidatorsFromAssemblyContaining<IUserValidatorMarker>();
        return services;
    }
}