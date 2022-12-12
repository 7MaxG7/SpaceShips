using Abstractions.Ships;
using Enums;
using Ships;
using Ships.Data;
using UnityEngine;

namespace Services
{
    internal class ShipUpgrader : IShipUpgrader
    {
        public void Upgrade(Ship ship, IModule module)
        {
            switch (module.EffectType)
            {
                case EffectType.Shield:
                case EffectType.Hp:
                case EffectType.ShieldRecovery:
                    var health = UpgradeHealth(ship.Health, module);
                    ship.SetHealth(health);
                    break;
                case EffectType.ShootCooldown:
                    var weapons = UpgradeWeapons(ship.WeaponBattery, module);
                    ship.SetWeapons(weapons);
                    break;
                default:
                    Debug.LogError($"{this}: Unknown module effect type {module.EffectType.ToString()}");
                    break;
            }
        }

        public void Downgrade(Ship ship, IModule module)
        {
            switch (module.EffectType)
            {
                case EffectType.Shield:
                case EffectType.Hp:
                case EffectType.ShieldRecovery:
                    var health = DowngradeHealth(ship.Health, module);
                    ship.SetHealth(health);
                    break;
                case EffectType.ShootCooldown:
                    var weapons = DowngradeWeapons(ship.WeaponBattery, module);
                    ship.SetWeapons(weapons);
                    break;
                default:
                    Debug.LogError($"{this}: Unknown module effect type {module.EffectType.ToString()}");
                    break;
            }
        }

        private IHealth UpgradeHealth(IHealth currentHealth, IModule module)
        {
            var health = new UpgradedHealth(currentHealth, module);
            health.RestoreHp();
            health.RestoreShield();
            return health;
        }

        private IWeaponBattery UpgradeWeapons(IWeaponBattery currentWeaponBattery, IModule module)
            => new UpgradedWeaponsBattery(currentWeaponBattery, module);

        private IHealth DowngradeHealth(IHealth health, IModule module)
        {
            if (health is not IDowngradable<IHealth> upgradedHealth)
            {
                Debug.LogError($"{this}: health cannot be downgraded");
                return health;
            }

            var downgradedHealth = upgradedHealth.Downgrade(module);
            downgradedHealth.RestoreHp();
            downgradedHealth.RestoreShield();
            return downgradedHealth;
        }

        private IWeaponBattery DowngradeWeapons(IWeaponBattery weaponBattery, IModule module)
        {
            if (weaponBattery is not IDowngradable<IWeaponBattery> upgradedWeaponBattery)
            {
                Debug.LogError($"{this}: weapons cannot be downgraded");
                return weaponBattery;
            }

            return upgradedWeaponBattery.Downgrade(module);
        }
    }
}