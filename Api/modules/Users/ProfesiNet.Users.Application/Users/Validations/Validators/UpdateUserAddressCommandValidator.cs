using FluentValidation;
using ProfesiNet.Users.Application.Users.Commands.Update;

namespace ProfesiNet.Users.Application.Users.Validations.Validators;

internal class UpdateUserAddressCommandValidator : AbstractValidator<UpdateUserAddressCommand>
{
    public UpdateUserAddressCommandValidator()
    {
        RuleFor(x => x.Address)
            .MaximumLength(100);
    }
    
}