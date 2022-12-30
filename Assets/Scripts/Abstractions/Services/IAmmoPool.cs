using Abstractions.Ships;
using Enums;
using Infrastructure;

namespace Abstractions.Services
{
    internal interface IAmmoPool : ICleanable
    {
        void Init();
        IAmmo SpawnAmmo(WeaponType weaponWeaponType);
    }
}