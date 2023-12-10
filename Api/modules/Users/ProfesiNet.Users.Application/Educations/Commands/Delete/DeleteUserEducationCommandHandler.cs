using MediatR;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Educations.Commands.Delete;

internal class DeleteUserEducationCommandHandler : IRequestHandler<DeleteUserEducationCommand>
{
    private readonly IEducationRepository _educationRepository;
    private readonly IUserRepository _userRepository;

    public DeleteUserEducationCommandHandler(IEducationRepository educationRepository, IUserRepository userRepository)
    {
        _educationRepository = educationRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(DeleteUserEducationCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == request.UserId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        var education =
            await _educationRepository.GetRecordByFilterAsync(e => e.Id == request.Id && e.UserId == user.Id,
                cancellationToken);

        if (education == null)
        {
            throw new EducationNotFoundException(request.Id);
        }


        await _educationRepository.DeleteAsync(education, cancellationToken);
    }
}