using Abstractions.Services;

namespace Ships
{
    internal class WeaponBattery : AbstractWeaponBattery
    {
        public WeaponBattery(int amount, IWeaponFactory weaponFactory) : base(amount, weaponFactory)
        {
            ReloadRate = 1;
        }
    }
}