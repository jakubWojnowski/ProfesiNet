using MediatR;
using ProfesiNet.Users.Application.Skills.Dtos;
using ProfesiNet.Users.Application.Skills.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Skills.Queries.Get;

internal class GetSkillByIdQueryHandler : IRequestHandler<GetSkillByIdQuery, SkillDto>
{
    private readonly ISkillRepository _skillRepository;
    private static readonly SkillMapper Mapper = new();
    public GetSkillByIdQueryHandler(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }
    public async Task<SkillDto> Handle(GetSkillByIdQuery request, CancellationToken cancellationToken)
    {
        var skill = await _skillRepository.GetRecordByFilterAsync(u => u.Id == request.Id, cancellationToken);
        if (skill is null)
        {
            throw new SkillNotFoundException(request.Id);
        }
        return Mapper.MapSkillToSkillDto(skill);
    }
}