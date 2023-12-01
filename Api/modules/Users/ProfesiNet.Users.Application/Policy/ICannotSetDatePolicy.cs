namespace ProfesiNet.Users.Application.Policy;

internal interface ICannotSetDatePolicy
{
    bool IsSatisfiedBy(DateTime? startDate, DateTime? endDate);
}