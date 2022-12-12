using Abstractions.Services;
using Abstractions.Ships;
using Configs.Data;
using Infrastructure;
using Ships;
using Ships.Data;
using UnityEngine;
using Zenject;

namespace Services
{
    internal class ShipsFactory : IShipsFactory
    {
        private readonly IAssetsProvider _assetsProvider;
        private readonly IWeaponFactory _weaponFactory;
        private readonly IModuleFactory _moduleFactory;
        private readonly IShipUpgrader _shipUpgrader;
        private readonly IControllersHolder _controllers;


        [Inject]
        public ShipsFactory(IAssetsProvider assetsProvider, IWeaponFactory weaponFactory, IModuleFactory moduleFactory
            , IShipUpgrader shipUpgrader, IControllersHolder controllers)
        {
            _assetsProvider = assetsProvider;
            _weaponFactory = weaponFactory;
            _moduleFactory = moduleFactory;
            _shipUpgrader = shipUpgrader;
            _controllers = controllers;
        }
        
        public IShip CreateShip(ShipData shipData, Vector3 position, Quaternion rotation)
        {
            CreateShipEquipments(shipData, out var health, out var weapons, out var modules);
            var ship = new Ship(shipData.ShipType, health, weapons, modules, _shipUpgrader);
            GenerateView(ship, position, rotation);
            return ship;
        }

        public void GenerateView(IShip ship, Vector3 position, Quaternion rotation)
        {
            var shipView = _assetsProvider.CreateShip(ship.ShipType, position, rotation);
            ship.SetView(shipView);
        }

        private void CreateShipEquipments(ShipData shipData, out IHealth health, out IWeaponBattery weaponBattery, out IShipModules modules)
        {
            health = new Health(shipData.MaxHp, shipData.MaxShied, shipData.ShieldRecovery, shipData.ShieldRecoveryInterval);
            weaponBattery = new WeaponBattery(shipData.WeaponSlotsAmount, _weaponFactory);
            modules = new ShipModules(shipData.ModuleSlotsAmount, _moduleFactory);
        }
    }
}