using Abstractions.Services;
using Abstractions.Ships;
using Enums;

namespace Services
{
    internal interface IModuleFactory : IEquipmentFactory<IModule, ModuleType>
    {
    }
}