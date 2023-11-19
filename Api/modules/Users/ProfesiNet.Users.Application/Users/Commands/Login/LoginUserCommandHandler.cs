using MediatR;
using Microsoft.AspNetCore.Identity;
using ProfesiNet.Users.Application.Users.Mappings;
using ProfesiNet.Users.Domain.Entities;
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
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Email == request.LoginUserDto.Email,
                       cancellationToken) ??
                   throw new Exception("Invalid email or password");
        var result = _passwordHasher.VerifyHashedPassword(user, user.EncodedPassword, request.LoginUserDto.Password);

        if (result == PasswordVerificationResult.Failed) throw new Exception("Invalid email or password");

        return _jwtProvider.GenerateJwtToken(user);
    }
}