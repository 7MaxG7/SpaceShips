using Abstractions.Ships;
using Enums;

namespace Abstractions.Services
{
    internal interface IWeaponFactory : IEquipmentFactory<IWeapon, WeaponType>
    {
    }
}