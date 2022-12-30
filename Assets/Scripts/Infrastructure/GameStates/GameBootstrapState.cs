using Abstractions.Services;
using DG.Tweening;
using Services;
using Sounds;
using Utils;
using Zenject;


namespace Infrastructure
{
    internal sealed class GameBootstrapState : IGameState
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ISceneLoader _sceneLoader;
        private readonly ICurtain _curtain;
        private readonly IAssetsProvider _assetsProvider;
        private readonly ISoundPlayer _soundPlayer;
        private IGameStateMachine _stateMachine;


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
            _sceneLoader.LoadScene(Constants.SETUP_SCENE_NAME, _stateMachine.Enter<ShipSetupState>);
        }

        public void Exit()
        {
        }

        public void Init(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
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