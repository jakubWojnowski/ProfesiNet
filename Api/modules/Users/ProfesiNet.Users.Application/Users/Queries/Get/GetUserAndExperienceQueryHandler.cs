using MediatR;
using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Application.Users.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Queries.Get;

internal class GetUserAndExperienceQueryHandler : IRequestHandler<GetUserAndExperienceQuery, UserAndExperienceDto> 
{
    private readonly IUserRepository _userRepository;
    private static readonly UserMapper Mapper = new();

    public GetUserAndExperienceQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<UserAndExperienceDto> Handle(GetUserAndExperienceQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user == null)
        {
            throw new UserNotFoundException(request.Id);
        }
        
        return (Mapper.UserAndExperienceDtosToUsers(user));
        
    }
}