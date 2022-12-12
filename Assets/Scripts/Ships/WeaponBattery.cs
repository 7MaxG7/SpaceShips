using System.Linq;
using Abstractions.Services;
using Abstractions.Ships;
using Enums;

namespace Ships
{
    internal abstract class AbstractWeaponBattery : AbstractEquipments<IWeapon, WeaponType>, IWeaponBattery
    {
        private bool _isActive;

        public void OnUpdate(float deltaTime)
        {
            if (!_isActive)
                return;

            var deltaCooldown = GetDeltaCooldown(deltaTime);
            foreach (var weapon in Equipments.Values.Where(weapon => !weapon.IsReady))
                weapon.ReduceCooldown(deltaTime);

            foreach (var weapon in Equipments.Values.Where(weapon => weapon.IsReady))
                weapon.Shoot();
        }

        protected abstract float GetDeltaCooldown(float deltaTime);

        public void ToggleShooting(bool isActive)
            => _isActive = isActive;
    }

    internal class WeaponBattery : AbstractWeaponBattery
    {
        public WeaponBattery(int amount, IWeaponFactory weaponFactory)
        {
            MaxEquipmentsAmount = amount;
            EquipmentsFactory = weaponFactory;
        }

        protected override float GetDeltaCooldown(float deltaTime)
            => deltaTime;
    }
}