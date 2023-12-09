using MediatR;
using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Shared.UserContext;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Application.Users.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Queries.Get;

internal class GetOwnProfileQueryHandler : IRequestHandler<GetOwnProfileQuery, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserContextService _currentUserContextService;
    private readonly IContext _context;
    private static readonly UserMapper Mapper = new();

    public GetOwnProfileQueryHandler(IUserRepository userRepository, ICurrentUserContextService currentUserContextService, IContext context)
    {
        _userRepository = userRepository;
        _currentUserContextService = currentUserContextService;
        _context = context;
    }
    public async Task<UserDto> Handle(GetOwnProfileQuery request, CancellationToken cancellationToken)
    {
        var tokenId = _context.Id;
        var user = await _userRepository.GetByIdAsync(tokenId, cancellationToken);
        if (user == null)
        {
            throw new UserNotFoundException(tokenId);
        }
        return Mapper.MapUserToUserDto(user);
    }
}