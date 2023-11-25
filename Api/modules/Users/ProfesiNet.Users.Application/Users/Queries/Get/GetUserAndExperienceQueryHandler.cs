using MediatR;
using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Application.Users.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Infrastructure.Repositories;

namespace ProfesiNet.Users.Application.Users.Queries.Get;

public class GetUserAndExperienceQueryHandler : IRequestHandler<GetUserAndExperienceQuery, UserAndExperienceDto> 
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
            throw new NotFoundException("User not found");
        }
        
        return (Mapper.UserAndExperienceDtosToUsers(user));
        
    }
}