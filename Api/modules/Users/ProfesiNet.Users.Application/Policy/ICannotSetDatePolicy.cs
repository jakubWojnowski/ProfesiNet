namespace ProfesiNet.Users.Application.Policy;

public interface ICannotSetDatePolicy
{
    bool IsSatisfiedBy(DateTime? startDate, DateTime? endDate);
}