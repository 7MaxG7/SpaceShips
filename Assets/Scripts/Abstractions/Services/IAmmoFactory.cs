using Abstractions.Ships;
using Enums;

namespace Abstractions.Services
{
    internal interface IAmmoFactory
    {
        IAmmo CreateAmmo(WeaponType weaponType);
    }
}