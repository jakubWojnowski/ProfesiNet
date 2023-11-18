using MediatR;
using Microsoft.AspNetCore.Identity;
using ProfesiNet.Users.Application.Users.Mappings;
using ProfesiNet.Users.Domain.Entities;
using ProfesiNet.Users.Infrastructure.Repositories;

namespace ProfesiNet.Users.Application.Users.Commands.Register;

public class RegisterCommandHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher ) : IRequestHandler<RegisterCommand>
{
    private static readonly UserMapper Mapper = new();
    
    
    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = Mapper.MapRegistrationDtoToUser(request.RegisterDto);
        
        var userExists = await userRepository.GetRecordByFilterAsync(u => u.Email == user.Email, cancellationToken);
        if (userExists != null)
        {
            throw new Exception("Email already exists");
        }
        
        var encoded = passwordHasher.HashPassword(user, request.RegisterDto.Password);
        user.EncodedPassword = encoded;
        
        await userRepository.AddAsync(user, cancellationToken);
    }
}