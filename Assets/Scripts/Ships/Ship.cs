using System;
using Abstractions.Ships;
using Enums;
using Services;
using Object = UnityEngine.Object;

namespace Ships
{
    internal class Ship : IShip
    {
        public event Action<IShip> OnDied;

        public ShipType ShipType { get; }
        public IHealth Health { get; private set; }
        public IWeaponBattery WeaponBattery { get; private set; }
        public IShipModules ShipModules { get; }
        public string Name { get; }
        public bool IsDead => Health.CurrentHp <= 0;

        private readonly IShipUpgrader _shipUpgrader;
        private ShipView _shipView;

        
        public Ship(ShipType shipType, IHealth health, IWeaponBattery weaponBattery, IShipModules shipModules,
            IShipUpgrader shipUpgrader)
        {
            _shipUpgrader = shipUpgrader;
            ShipType = shipType;
            SetHealth(health);
            SetWeapons(weaponBattery);
            ShipModules = shipModules;
            ShipModules.OnModuleEquiped += UpgradeShip;
            ShipModules.OnModuleUnequip += DowngradeShip;
            Name = ShipType.ToString();
        }

        public void SetView(ShipView shipView)
        {
            _shipView = shipView;
            WeaponBattery.SetSlots(_shipView.WeaponSlots);
            ShipModules.SetSlots(_shipView.ModuleSlots);
        }

        public void TakeDamage(int damage)
        {
            Health.TakeDamage(damage);
            if (Health.CurrentHp <= 0)
                OnDied?.Invoke(this);
        }

        public void Kill()
        {
            Object.Destroy(_shipView.gameObject);
        }

        public void SetHealth(IHealth health)
            => Health = health;

        public void SetWeapons(IWeaponBattery weaponBattery)
        {
            WeaponBattery = weaponBattery;
            WeaponBattery.Init(this);
        }

        public void CleanUp()
        {
            ShipModules.OnModuleEquiped -= UpgradeShip;
            ShipModules.OnModuleUnequip -= DowngradeShip;
        }

        private void UpgradeShip(IModule module)
            => _shipUpgrader.Upgrade(this, module);

        private void DowngradeShip(IModule module)
            => _shipUpgrader.Downgrade(this, module);
    }
}