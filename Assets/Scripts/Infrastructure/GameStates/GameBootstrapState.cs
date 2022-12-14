using System;
using Abstractions.Services;
using DG.Tweening;
using Services;
using Sounds;
using Utils;
using Zenject;


namespace Infrastructure
{
    internal sealed class GameBootstrapState : IGameBootstrapState
    {
        public event Action OnStateChange;

        private readonly IStaticDataService _staticDataService;
        private readonly ISceneLoader _sceneLoader;
        private readonly ICurtain _curtain;
        private readonly IAssetsProvider _assetsProvider;
        private readonly ISoundPlayer _soundPlayer;


        [Inject]
        public GameBootstrapState(IStaticDataService staticDataService, ISceneLoader sceneLoader, ICurtain curtain
            , IAssetsProvider assetsProvider, ISoundPlayer soundPlayer)
        {
            _staticDataService = staticDataService;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _assetsProvider = assetsProvider;
            _soundPlayer = soundPlayer;
        }

        public void Enter()
        {
            PrepareServices();
            _sceneLoader.LoadScene(Constants.SETUP_SCENE_NAME, () => OnStateChange?.Invoke());
        }

        public void Exit()
        {
        }

        private void PrepareServices()
        {
            DOTween.Init();
            var curtainView = _assetsProvider.CreateCurtain();
            _curtain.Prepare(curtainView);
            _curtain.ShowCurtain(false);
            _staticDataService.Init();
            _soundPlayer.Init();
        }
    }
}