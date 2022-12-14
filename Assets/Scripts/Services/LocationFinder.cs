using System.Linq;
using Abstractions.Services;
using Configs;
using Configs.Data;
using Enums;
using Ships.Views;
using UnityEngine;
using Zenject;

namespace Services
{
    internal class LocationFinder : ILocationFinder
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly RulesConfig _rulesConfig;


        [Inject]
        public LocationFinder(ISceneLoader sceneLoader, RulesConfig rulesConfig)
        {
            _sceneLoader = sceneLoader;
            _rulesConfig = rulesConfig;
        }
        
        public Vector3? GetOpponentLocation(OpponentId opponentId, out Quaternion rotation)
        {
            var sceneName = _sceneLoader.GetCurrentSceneName();
            rotation = default;
            var sceneRule = _rulesConfig
                .Opponents.FirstOrDefault(data => data.OpponentId == opponentId)?
                .SpawnPositions.FirstOrDefault(data => data.SceneName == sceneName);
            if (sceneRule == null)
            {
                Debug.LogError($"{this}: No spawn position in rules config for opponent {opponentId.ToString()} in scene {sceneName}");
                var shipSpawnerMarker = Object.FindObjectsOfType<ShipSpawnerMarker>()
                    .FirstOrDefault(data => data.OpponentId == opponentId);
                if (shipSpawnerMarker == null)
                    return null;

                sceneRule = new SpawnPosition(sceneName);
                sceneRule.UpdatePosition(shipSpawnerMarker.transform);
            }

            rotation = sceneRule.Rotation;
            return sceneRule.Position;
        }
    }
}