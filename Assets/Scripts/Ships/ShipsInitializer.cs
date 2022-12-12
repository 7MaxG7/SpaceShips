using System.Collections.Generic;
using System.Linq;
using Abstractions;
using Abstractions.Services;
using Abstractions.Ships;
using Configs;
using Configs.Data;
using Enums;
using Services;
using Ships.Views;
using UnityEngine;
using Zenject;

namespace Ships
{
    internal class ShipsInitializer : IShipsInitializer
    {
        public Dictionary<OpponentId, IShip> Ships { get; } = new();

        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;
        private readonly IShipsFactory _shipsFactory;
        private readonly RulesConfig _rulesConfig;


        [Inject]
        public ShipsInitializer(ISceneLoader sceneLoader, IStaticDataService staticDataService, IShipsFactory shipsFactory, RulesConfig rulesConfig)
        {
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
            _shipsFactory = shipsFactory;
            _rulesConfig = rulesConfig;
        }
        
        public void PrepareShips()
        {
            var sceneName = _sceneLoader.GetCurrentSceneName();
            foreach (var opponent in _rulesConfig.Opponents)
            {
                if (Ships.ContainsKey(opponent.OpponentId))
                    continue;
                
                var sceneRules = opponent.SpawnPositions.FirstOrDefault(data => data.SceneName == sceneName);
                if (sceneRules == null)
                {
                    Debug.LogError($"{this}: No spawn position in rules config for opponent {opponent.OpponentId.ToString()} on scene {sceneName}");
                    var shipSpawnerMarker = Object.FindObjectsOfType<ShipSpawnerMarker>()
                        .FirstOrDefault(data => data.OpponentId == opponent.OpponentId);
                    if (shipSpawnerMarker == null)
                        continue;
                    
                    sceneRules = new SpawnPosition(sceneName);
                    sceneRules.UpdatePosition(shipSpawnerMarker.transform);
                }

                var shipData = _staticDataService.GetShipData(opponent.ShipType);
                var ship = _shipsFactory.CreateShip(shipData, sceneRules.Position, sceneRules.Rotation);
                Ships.Add(opponent.OpponentId, ship);
            }
        }
    }
}