using MediatR;
using ProfesiNet.Shared.UserContext;
using ProfesiNet.Users.Application.Users.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Commands.Update;

internal class UpdateUserFullNameCommandHandler : IRequestHandler<UpdateUserFullNameCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserContextService _currentUserContextService;
    private static readonly UserMapper Mapper = new();

    public UpdateUserFullNameCommandHandler(IUserRepository userRepository,
        ICurrentUserContextService currentUserContextService)
    {
        _userRepository = userRepository;
        _currentUserContextService = currentUserContextService;
    }

    public async Task Handle(UpdateUserFullNameCommand request, CancellationToken cancellationToken)
    {
        var token = Guid.Parse(_currentUserContextService.GetCurrentUser()!.Id!);
        var user = await _userRepository.GetByIdAsync(token, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(token);
        }
        // var updatedUser = Mapper.MapUpdateUserFullNameCommandToUser(request with
        // {
        //     Id = token
        // });
        

        user.Name = request.Name ?? user.Name;
        user.Surname = request.Surname ?? user.Surname;

        await _userRepository.UpdateAsync(user, cancellationToken);
    }
}