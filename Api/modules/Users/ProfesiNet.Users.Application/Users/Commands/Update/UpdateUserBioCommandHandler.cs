using MediatR;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Application.Users.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Commands.Update;

internal class UpdateUserBioCommandHandler : IRequestHandler<UpdateUserBioCommand>
{
    private readonly IUserRepository _userRepository;
    private static readonly UserMapper Mapper = new();

    public UpdateUserBioCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(UpdateUserBioCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
        {
            throw new UserNotFoundException(request.UserId);
        }
        var bioDto = new UserBioDto
        {
            Bio = request.Bio ?? user.Bio,
        };
        var updatedUserBio = Mapper.MapUpdateUserBioDtoToUser(user, bioDto);
        await _userRepository.UpdateAsync(updatedUserBio, cancellationToken);
    }
}