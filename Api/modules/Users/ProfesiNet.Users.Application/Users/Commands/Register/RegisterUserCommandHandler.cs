using MediatR;
using Microsoft.AspNetCore.Identity;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Application.Users.Mappings;
using ProfesiNet.Users.Domain.Entities;
using ProfesiNet.Users.Infrastructure.Repositories;

namespace ProfesiNet.Users.Application.Users.Commands.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private static readonly UserMapper Mapper = new();

    public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
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
        var user = Mapper.MapRegistrationDtoToUser(dto);
        
        var encoded = _passwordHasher.HashPassword(user, dto.Password);
        user.EncodedPassword = encoded;
        
        await _userRepository.AddAsync(user, cancellationToken);
    }
}