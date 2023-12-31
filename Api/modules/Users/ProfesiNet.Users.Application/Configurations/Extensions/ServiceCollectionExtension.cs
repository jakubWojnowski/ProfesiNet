﻿using System.Runtime.CompilerServices;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Users.Application.Policy;
using ProfesiNet.Users.Application.Users.Validations.Validators;
using ProfesiNet.Users.Domain.Entities;
[assembly: InternalsVisibleTo("ProfesiNet.Users.Api")]
namespace ProfesiNet.Users.Application.Configurations.Extensions;

internal static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        services.AddValidatorsFromAssemblyContaining(typeof(RegisterUserCommandValidator));

        services.AddScoped<ICannotSetDatePolicy, CannotSetDatePolicy>();
        services.AddScoped<ICannotAddSkillPolicy, CannotAddSkillPolicy>();
        
        return services;
    }
}