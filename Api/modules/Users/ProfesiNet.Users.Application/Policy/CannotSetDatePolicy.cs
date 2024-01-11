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
        if (!startDate.HasValue)
        {
            return false;
        }
    
        var startDateTime = startDate.Value.Date;
        var todayDateTime = _clock.CurrentDate().Date;

 
        if (!endDate.HasValue)
        {
            return startDateTime <= todayDateTime;
        }

        
        var endDateTime = endDate.Value.Date;
        return startDateTime <= endDateTime && startDateTime <= todayDateTime;
    }

}