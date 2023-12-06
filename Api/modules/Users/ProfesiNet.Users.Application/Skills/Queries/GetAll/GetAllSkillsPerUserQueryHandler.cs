using MediatR;
using ProfesiNet.Shared.UserContext;
using ProfesiNet.Users.Application.Skills.Dtos;
using ProfesiNet.Users.Application.Skills.Mappings;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Skills.Queries.GetAll;

internal class GetAllSkillsPerUserQueryHandler : IRequestHandler<GetAllSkillsPerUserQuery, IReadOnlyCollection<SkillDto>>
{
    private readonly ISkillRepository _skillRepository;
    private readonly ICurrentUserContextService _currentUserContextService;
    private static readonly SkillMapper Mapper = new();

    public GetAllSkillsPerUserQueryHandler(ISkillRepository skillRepository, ICurrentUserContextService currentUserContextService)
    {
        _skillRepository = skillRepository;
        _currentUserContextService = currentUserContextService;
    }
    public async Task<IReadOnlyCollection<SkillDto>> Handle(GetAllSkillsPerUserQuery request, CancellationToken cancellationToken)
    {
        var skills = await _skillRepository.GetAllForConditionAsync(u => u.UserID == request.Id, cancellationToken);
        
        return Mapper.MapSkillToSkillDtos(skills);
    }
}