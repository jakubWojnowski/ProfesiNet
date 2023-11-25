using MediatR;
using Microsoft.AspNetCore.Identity;
using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Users.Application.Users.Mappings;
using ProfesiNet.Users.Domain.Entities;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;
using ProfesiNet.Users.Infrastructure.Repositories;

namespace ProfesiNet.Users.Application.Users.Commands.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public LoginUserCommandHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher,
        IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }
    private static readonly UserMapper Mapper = new();

    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Email == request.Email,
                       cancellationToken) ??
                   throw new NotFoundException("Invalid email");
        var result = _passwordHasher.VerifyHashedPassword(user, user.EncodedPassword, request.Password);

        if (result == PasswordVerificationResult.Failed) throw new NotFoundException("Invalid  password");

        return _jwtProvider.GenerateJwtToken(user);
    }
}