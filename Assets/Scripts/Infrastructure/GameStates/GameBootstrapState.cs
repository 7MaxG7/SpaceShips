using System;
using Abstractions.Services;
using Services;
using Utils;
using Zenject;


namespace Infrastructure
{
    internal sealed class GameBootstrapState : IGameBootstrapState
    {
        public event Action OnStateChange;

        private readonly IStaticDataService _staticDataService;
        private readonly ISceneLoader _sceneLoader;


        [Inject]
        public GameBootstrapState(IStaticDataService staticDataService, ISceneLoader sceneLoader)
        {
            _staticDataService = staticDataService;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            PrepareServices();
            _sceneLoader.LoadScene(Constants.BOOTSTRAP_SCENE_NAME, () => OnStateChange?.Invoke());
        }

        public void Exit()
        {
        }

        private void PrepareServices()
        {
            _staticDataService.Init();
        }
    }
}