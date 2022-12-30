using System.Collections.Generic;
using Abstractions;
using Abstractions.Services;
using Abstractions.Ships;
using Configs;
using Enums;
using Infrastructure;
using Services;
using Zenject;

namespace Ships
{
    internal class ShipsInitializer : IShipsInitializer
    {
        public Dictionary<OpponentId, IShip> Ships { get; } = new();

        private readonly IStaticDataService _staticDataService;
        private readonly IShipsFactory _shipsFactory;
        private readonly ILocationFinder _locationFinder;
        private readonly IWeaponFactory _weaponFactory;
        private readonly IModuleFactory _moduleFactory;
        private readonly RulesConfig _rulesConfig;


        [Inject]
        public ShipsInitializer(IStaticDataService staticDataService, IShipsFactory shipsFactory
            , ILocationFinder locationFinder, IWeaponFactory weaponFactory, IModuleFactory moduleFactory
            , ICleaner cleaner, RulesConfig rulesConfig)
        {
            _staticDataService = staticDataService;
            _shipsFactory = shipsFactory;
            _locationFinder = locationFinder;
            _weaponFactory = weaponFactory;
            _moduleFactory = moduleFactory;
            _rulesConfig = rulesConfig;
            cleaner.AddCleanable(this);
        }
        
        public void PrepareShips()
        {
            foreach (var opponent in _rulesConfig.Opponents)
            {
                var position = _locationFinder.GetOpponentLocation(opponent.OpponentId, out var rotation);
                if (!position.HasValue)
                    continue;
                
                if (Ships.ContainsKey(opponent.OpponentId))
                {
                    var ship = Ships[opponent.OpponentId];
                    ship.Health.RestoreHp();
                    ship.Health.RestoreShield();
                    _shipsFactory.GenerateView(ship, position.Value, rotation);
                    var weapons = ship.WeaponBattery;
                    for (var i = 0; i < weapons.MaxEquipmentsAmount; i++)
                    {
                        var weapon = weapons.GetEquipment(i);
                        if (weapon != null)
                            _weaponFactory.GenerateView(weapon, weapons.GetSlotTransform(i));
                    }
                    var modules = ship.ShipModules;
                    for (var i = 0; i < modules.MaxEquipmentsAmount; i++)
                    {
                        var module = modules.GetEquipment(i);
                        if (module != null)
                            _moduleFactory.GenerateView(module, modules.GetSlotTransform(i));
                    }
                    continue;
                }
                
                var shipData = _staticDataService.GetShipData(opponent.ShipType);
                var newShip = _shipsFactory.CreateShip(shipData, position.Value, rotation);
                Ships.Add(opponent.OpponentId, newShip);
            }
        }


        public void CleanUp()
        {
            foreach (var ship in Ships.Values) 
                ship.CleanUp();
            Ships.Clear();
        }
    }
}