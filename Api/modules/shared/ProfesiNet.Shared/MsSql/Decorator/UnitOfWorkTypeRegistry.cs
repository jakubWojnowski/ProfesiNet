using ProfesiNet.Shared.Configurations;

namespace ProfesiNet.Shared.MsSql.Decorator;

internal class UnitOfWorkTypeRegistry
{
    private readonly Dictionary<string, Type> _unitOfWorkTypes = new();
    public void Register<T>() where T : IUnitOfWork => _unitOfWorkTypes[GetKey<T>()] = typeof(T);
    
    public Type? Resolve<T>() => _unitOfWorkTypes.TryGetValue(GetKey<T>(),out var type ) ? type : null;

    private static string GetKey<T>() => $"{typeof(T).GetModuleName()}";
}