using Abstractions.Ships;
using Enums;

namespace Abstractions.Services
{
    public interface IWeaponFactory : IEquipmentFactory<IWeapon, WeaponType>
    {
    }
}