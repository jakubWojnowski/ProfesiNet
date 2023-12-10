using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProfesiNet.Shared.Messaging;
using ProfesiNet.Users.Application.Events;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Application.Users.Mappings;
using ProfesiNet.Users.Domain.Entities;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Commands.Register;

internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IValidator<RegisterUserDto> _validator;
    private readonly IMessageBroker _messageBroker;
    private static readonly UserMapper Mapper = new();

    public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IValidator<RegisterUserDto> validator, IMessageBroker messageBroker )
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _validator = validator;
        _messageBroker = messageBroker;
    }
   
    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        
        var dto = new RegisterUserDto
        {
            Email = request.Email,
            Name = request.Name,
            Surname = request.Surname,
            Password = request.Password,
            ConfirmPassword = request.ConfirmPassword
        };
        var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new RegisterIncorrectFormatException(errorMessage);
        }
        var user = Mapper.MapRegistrationDtoToUser(dto);
        
        var encoded = _passwordHasher.HashPassword(user, dto.Password);
        user.EncodedPassword = encoded;
        
        await _userRepository.AddAsync(user, cancellationToken);
        await _messageBroker.PublishAsync(new UserCreated(user.Id, user.Name, user.Surname));
    }
}