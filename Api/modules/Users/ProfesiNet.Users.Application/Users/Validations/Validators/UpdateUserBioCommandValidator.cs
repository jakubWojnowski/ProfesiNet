using FluentValidation;
using ProfesiNet.Users.Application.Users.Commands.Update;

namespace ProfesiNet.Users.Application.Users.Validations.Validators;

internal class UpdateUserBioCommandValidator : AbstractValidator<UpdateUserBioCommand>
{
    public UpdateUserBioCommandValidator()
    {
        RuleFor(x => x.Bio)
            .MaximumLength(100);
    }
}