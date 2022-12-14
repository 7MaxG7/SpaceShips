using Abstractions.Services;
using Abstractions.Ships;
using Configs.Data;
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


        [Inject]
        public ShipsFactory(IAssetsProvider assetsProvider, IWeaponFactory weaponFactory, IModuleFactory moduleFactory
            , IShipUpgrader shipUpgrader)
        {
            _assetsProvider = assetsProvider;
            _weaponFactory = weaponFactory;
            _moduleFactory = moduleFactory;
            _shipUpgrader = shipUpgrader;
        }
        
        public IShip CreateShip(ShipData shipData, Vector3 position, Quaternion rotation)
        {
            CreateShipEquipments(shipData, out var health, out var weapons, out var modules);
            var ship = new Ship(shipData.ShipType, health, weapons, modules, _shipUpgrader);
            GenerateView(ship, position, rotation);
            return ship;
        }

        public IDamagableView GenerateView(IShip ship, Vector3 position, Quaternion rotation)
        {
            var shipView = _assetsProvider.CreateShip(ship.ShipType, position, rotation);
            ship.SetView(shipView);
            return shipView;
        }

        private void CreateShipEquipments(ShipData shipData, out IHealth health, out IWeaponBattery weaponBattery, out IShipModules modules)
        {
            health = new Health(shipData.MaxHp, shipData.MaxShied, shipData.ShieldRecovery, shipData.ShieldRecoveryInterval);
            weaponBattery = new WeaponBattery(shipData.WeaponSlotsAmount, _weaponFactory);
            modules = new ShipModules(shipData.ModuleSlotsAmount, _moduleFactory);
        }
    }
}