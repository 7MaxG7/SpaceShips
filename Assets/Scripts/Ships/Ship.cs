using System;
using Abstractions.Ships;
using Enums;
using Services;

namespace Ships
{
    internal class Ship : IShip
    {
        public event Action<IShip> OnDied;

        public ShipType ShipType { get; }
        public IHealth Health { get; private set; }
        public IWeaponBattery WeaponBattery { get; private set; }
        public IShipModules ShipModules { get; }
        
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
            ShipModules.OnModuleInstall += UpgradeShip;
            ShipModules.OnModuleUninstall += DowngradeShip;
        }

        public void SetView(ShipView shipView)
        {
            _shipView = shipView;
            WeaponBattery.SetSlots(_shipView.WeaponSlots);
            ShipModules.SetSlots(_shipView.ModuleSlots);
        }

        public void SetHealth(IHealth health)
        {
            Health = health;
        }

        public void SetWeapons(IWeaponBattery weaponBattery)
        {
            WeaponBattery = weaponBattery;
        }

        public void CleanUp()
        {
            ShipModules.OnModuleInstall -= UpgradeShip;
            ShipModules.OnModuleUninstall -= DowngradeShip;
        }

        private void UpgradeShip(IModule module)
        {
            _shipUpgrader.Upgrade(this, module);
        }

        private void DowngradeShip(IModule module)
        {
            _shipUpgrader.Downgrade(this, module);
        }
    }
}