using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Users.Domain.Exceptions;

public class CannotSetDateException(DateTime? date1, DateTime? date2) : ProfesiNetException($"cannot set dates {date1} and {date2}")
{
    
}