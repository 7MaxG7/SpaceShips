﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstractions.Services;
using Abstractions.Ships;
using Enums;
using Ships;
using Ships.Data;
using UnityEngine;
using Utils;
using Zenject;

namespace Services
{
    public sealed class ShipsFactory : IShipsFactory
    {
        private readonly IAssetsProvider _assetsProvider;
        private readonly IWeaponFactory _weaponFactory;
        private readonly IModuleFactory _moduleFactory;
        private readonly IShipUpgrader _shipUpgrader;
        private readonly IStaticDataService _staticDataService;
        
        private Transform _shipsParent;


        [Inject]
        public ShipsFactory(IAssetsProvider assetsProvider, IWeaponFactory weaponFactory, IModuleFactory moduleFactory
            , IShipUpgrader shipUpgrader, IStaticDataService staticDataService)
        {
            _assetsProvider = assetsProvider;
            _weaponFactory = weaponFactory;
            _moduleFactory = moduleFactory;
            _shipUpgrader = shipUpgrader;
            _staticDataService = staticDataService;
        }

        public void PrepareRoot()
        {
            if (_shipsParent == null)
                _shipsParent = new GameObject(Constants.SHIPS_PARENT_NAME).transform;
        }
        
        public async Task<IShip> CreateShipAsync(ShipModel shipModel, Vector3 position, Quaternion rotation)
        {
            var ship = await CreateShipAsync(shipModel.ShipType, position, rotation);
            await SetWeaponsAsync(ship.WeaponBattery, shipModel.WeaponTypes);
            await SetModulesAsync(ship.ShipModules, shipModel.ModuleTypes);

            return ship;
        }

        private async Task<Ship> CreateShipAsync(ShipType shipType, Vector3 position, Quaternion rotation)
        {
            var shipData = _staticDataService.GetShipData(shipType);
            var health = new Health(shipData.MaxHp, shipData.MaxShied, shipData.ShieldRecovery, shipData.ShieldRecoveryInterval);
            var weapons = new WeaponBattery(shipData.WeaponSlotsAmount, _weaponFactory);
            var modules = new ShipModules(shipData.ModuleSlotsAmount, _moduleFactory);
            
            var ship = new Ship(shipData.ShipType, health, weapons, modules, _shipUpgrader);
            var shipView = await _assetsProvider.CreateInstanceAsync<ShipView>(shipData.Prefab, position, rotation, _shipsParent);
            ship.SetView(shipView);
            return ship;
        }

        private async Task SetWeaponsAsync(IWeaponBattery weapons, Dictionary<int, WeaponType> weaponTypes)
        {
            foreach (var slotIndex in weaponTypes.Keys.Where(slotIndex => slotIndex < weapons.MaxEquipmentsAmount))
                await weapons.SetEquipmentAsync(slotIndex, weaponTypes[slotIndex]);
        }

        private async Task SetModulesAsync(IShipModules modules, Dictionary<int, ModuleType> moduleTypes)
        {
            foreach (var slotIndex in moduleTypes.Keys.Where(slotIndex => slotIndex < modules.MaxEquipmentsAmount))
                await modules.SetEquipmentAsync(slotIndex, moduleTypes[slotIndex]);
        }
    }
}