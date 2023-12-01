using Confab.Shared.Abstractions.Interfaces;

namespace ProfesiNet.Users.Application.Policy;

internal class CannotSetDatePolicy : ICannotSetDatePolicy
{
    private readonly IClock _clock;

    public CannotSetDatePolicy(IClock clock)
    {
        _clock = clock;
    }
    
    public bool IsSatisfiedBy(DateTime? startDate, DateTime? endDate)
    {
        if (!startDate.HasValue || !endDate.HasValue)
        {
            return false;
        }
        
        var start = startDate.Value.Date;
        var end = endDate.Value.Date;
        var today = _clock.CurrentDate().Date; 
        
        return start <= end && start <= today;
    }

}