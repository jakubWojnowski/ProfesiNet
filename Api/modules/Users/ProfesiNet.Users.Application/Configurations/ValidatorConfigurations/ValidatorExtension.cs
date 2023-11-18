using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Application.Users.Validations.Validators;

namespace ProfesiNet.Users.Application.Configurations.ValidatorConfigurations;

public static class ValidatorExtension
{
    public static IServiceCollection RegisterValidators(this IServiceCollection services)
        => services.RegisterUserValidators();

    private static IServiceCollection RegisterUserValidators(this IServiceCollection services)
        => services.AddScoped<IValidator<RegisterDto>, RegisterUserDtoValidator>();

}