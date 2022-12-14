using Abstractions.Ships;
using Enums;
using Infrastructure;

namespace Abstractions.Services
{
    internal interface IAmmoPool : ICleaner
    {
        void Init();
        IAmmo SpawnAmmo(WeaponType weaponWeaponType);
    }
}