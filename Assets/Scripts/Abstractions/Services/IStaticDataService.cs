using Configs.Data;
using Enums;

namespace Abstractions.Services
{
    internal interface IStaticDataService
    {
        void Init();
        ShipData GetShipData(ShipType shipType);
        WeaponData GetWeaponData(WeaponType weapon);
        ModuleData GetModuleData(ModuleType module);
    }
}