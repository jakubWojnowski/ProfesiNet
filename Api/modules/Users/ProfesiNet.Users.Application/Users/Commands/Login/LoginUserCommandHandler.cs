using MediatR;
using Microsoft.AspNetCore.Identity;
using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Application.Users.Mappings;
using ProfesiNet.Users.Domain.Entities;
using ProfesiNet.Users.Domain.Enums;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Commands.Login;

internal class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, UserLoggedInDto>
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

    public async Task<UserLoggedInDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Email == request.Email,
                       cancellationToken) ??
                   throw new InvalidEmailException(request.Email);
        var result = _passwordHasher.VerifyHashedPassword(user, user.EncodedPassword, request.Password);

        if (result == PasswordVerificationResult.Failed) throw new InvalidPasswordException();
        var dto = Mapper.MapUserToUserLoggedInDto(user);
        dto.Token = _jwtProvider.GenerateJwtToken(user);
         dto.ProfilePicture = user.Photos.FirstOrDefault(x => x.PictureType == PictureType.ProfilePicture)?.Url;
         dto.ProfilePictureId = user.Photos.FirstOrDefault(x => x.PictureType == PictureType.ProfilePicture)?.Id;

        return dto;
    }
}