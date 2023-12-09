namespace ProfesiNet.Shared.Contexts
{
    public interface IIdentityContext
    {
        public Guid Id { get; }
        Dictionary<string, IEnumerable<string>> Claims { get; }
    }
}