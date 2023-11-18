using MediatR;
using Microsoft.AspNetCore.Identity;
using ProfesiNet.Users.Application.Users.Mappings;
using ProfesiNet.Users.Domain.Entities;
using ProfesiNet.Users.Domain.Interfaces;
using ProfesiNet.Users.Infrastructure.Repositories;

namespace ProfesiNet.Users.Application.Users.Commands.Login;

public class LoginCommandHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher,
    IJwtProvider jwtProvider) : IRequestHandler<LoginCommand, string>
{
    private static readonly UserMapper Mapper = new();

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetRecordByFilterAsync(u => u.Email == request.LoginDto.Email,
                       cancellationToken) ??
                   throw new Exception("Invalid email or password");
        var result = passwordHasher.VerifyHashedPassword(user, user.EncodedPassword, request.LoginDto.Password);

        if (result == PasswordVerificationResult.Failed) throw new Exception("Invalid email or password");

        return jwtProvider.GenerateJwtToken(user);
    }
}