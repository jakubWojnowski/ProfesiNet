using FluentValidation;
using Microsoft.AspNetCore.Identity;
using ProfesiNet.Users.Application.Users.Commands.Login;
using ProfesiNet.Users.Domain.Entities;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Validations.Validators;

internal class LoginCommandValidator : AbstractValidator<LoginUserCommand>, IUserValidatorMarker
{
    public LoginCommandValidator(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .Custom( (email, context) =>
            {
               
                var user =  userRepository.GetRecordByFilterAsync(u => u.Email == email);
                if (user.Result == null)
                {
                    context.AddFailure("Email", "There is no account for that email");
                }
            });

        RuleFor(x => x.Password)
            .NotEmpty()
            .Custom( (password, context) =>
            {
                var email = context.InstanceToValidate.Email;
                // Pass the cancellation token to the repository call
                var user =  userRepository.GetRecordByFilterAsync(u => u.Email == email);
                if (user.Result != null)
                {
                    // The VerifyHashedPassword method is not async and does not accept a CancellationToken.
                    // If this method were long-running, ideally it would be async and accept a CancellationToken.
                    var result = passwordHasher.VerifyHashedPassword(user.Result, user.Result.EncodedPassword, password);
                    if (result == PasswordVerificationResult.Failed)
                    {
                        context.AddFailure("Password", "Invalid Password");
                    }
                }
            });
    }
}