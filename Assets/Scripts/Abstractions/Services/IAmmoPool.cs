using Abstractions.Ships;
using Enums;

namespace Abstractions.Services
{
    internal interface IAmmoPool
    {
        IAmmo SpawnAmmo(WeaponType weaponWeaponType);
    }
}