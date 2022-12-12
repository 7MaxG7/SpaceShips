using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private IGame _game;


        [Inject]
        private void InjectDependencies(IGame game)
        {
            _game = game;
        }

        private void Awake()
        {
            _game.Init(this);
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            _game.Controllers?.OnUpdate(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _game.Controllers.ClearControllers();
        }
    }
}