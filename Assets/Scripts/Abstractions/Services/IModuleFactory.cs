using Abstractions.Services;
using Abstractions.Ships;
using Enums;

namespace Services
{
    public interface IModuleFactory : IEquipmentFactory<IModule, ModuleType>
    {
    }
}