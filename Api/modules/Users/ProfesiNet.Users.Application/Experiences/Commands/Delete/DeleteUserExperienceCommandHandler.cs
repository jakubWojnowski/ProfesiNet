using MediatR;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Experiences.Commands.Delete;

internal class DeleteUserExperienceCommandHandler : IRequestHandler<DeleteUserExperienceCommand>
{
    private readonly IExperienceRepository _experienceRepository;
    private readonly IUserRepository _userRepository;

    public DeleteUserExperienceCommandHandler(IExperienceRepository experienceRepository,
        IUserRepository userRepository)
    {
        _experienceRepository = experienceRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(DeleteUserExperienceCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == request.UserId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        var experience =
            await _experienceRepository.GetRecordByFilterAsync(e => e.Id == request.Id && e.UserId == user.Id,
                cancellationToken);
        if (experience == null)
        {
            throw new ExperienceNotFoundException(request.Id);
        }

        await _experienceRepository.DeleteAsync(experience, cancellationToken);
    }
}