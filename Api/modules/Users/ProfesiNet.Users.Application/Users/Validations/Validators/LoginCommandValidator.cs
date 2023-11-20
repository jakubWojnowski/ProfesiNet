using FluentValidation;
using Microsoft.AspNetCore.Identity;
using ProfesiNet.Users.Application.Users.Commands.Login;
using ProfesiNet.Users.Domain.Entities;
using ProfesiNet.Users.Infrastructure.Repositories;

namespace ProfesiNet.Users.Application.Users.Validations.Validators;

public class LoginCommandValidator : AbstractValidator<LoginUserCommand>, IUserValidatorMarker
{
    public LoginCommandValidator(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .CustomAsync(async (email, context, ct) =>
            {
               
                var user = await userRepository.GetRecordByFilterAsync(u => u.Email == email, ct);
                if (user == null)
                {
                    context.AddFailure("Email", "There is no account for that email");
                }
            });

        RuleFor(x => x.Password)
            .NotEmpty()
            .CustomAsync(async (password, context, ct) =>
            {
                var email = context.InstanceToValidate.Email;
                // Pass the cancellation token to the repository call
                var user = await userRepository.GetRecordByFilterAsync(u => u.Email == email, ct);
                if (user != null)
                {
                    // The VerifyHashedPassword method is not async and does not accept a CancellationToken.
                    // If this method were long-running, ideally it would be async and accept a CancellationToken.
                    var result = passwordHasher.VerifyHashedPassword(user, user.EncodedPassword, password);
                    if (result == PasswordVerificationResult.Failed)
                    {
                        context.AddFailure("Password", "Invalid Password");
                    }
                }
            });
    }
}