using Confab.Shared.Abstractions.Interfaces;

namespace ProfesiNet.Shared.Time;

internal class UtcClock : IClock
{
    public DateTime CurrentDate() => DateTime.Now;
}