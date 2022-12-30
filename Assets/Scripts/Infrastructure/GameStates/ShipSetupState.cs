using Abstractions;
using Abstractions.Services;
using Configs;
using Sounds;
using Ui.ShipSetup.Controllers;
using Zenject;


namespace Infrastructure
{
    internal sealed class ShipSetupState : IGameState
    {
        private const float CURTAIN_HIDE_DELAY = 0.35f;

        private readonly ICurtain _curtain;
        private readonly IShipsInitializer _shipsInitializer;
        private readonly IAssetsProvider _assetsProvider;
        private readonly ISoundPlayer _soundPlayer;
        private readonly IStaticDataService _staticDataService;
        private readonly UiConfig _uiConfig;
        private IGameStateMachine _stateMachine;
        private ShipSetupMenuController _shipSetupMenuController;


        [Inject]
        public ShipSetupState(ICurtain curtain, IShipsInitializer shipsInitializer, IAssetsProvider assetsProvider
            , ISoundPlayer soundPlayer, IStaticDataService staticDataService, UiConfig uiConfig)
        {
            _curtain = curtain;
            _shipsInitializer = shipsInitializer;
            _assetsProvider = assetsProvider;
            _soundPlayer = soundPlayer;
            _staticDataService = staticDataService;
            _uiConfig = uiConfig;
        }

        public void Enter()
        {
            PrepareSetupScene();
            _soundPlayer.PlayMusic();
            // Delay prevents lagging animation on load after bootstrap
            _curtain.HideCurtain(CURTAIN_HIDE_DELAY);
        }

        public void Exit()
        {
            _shipSetupMenuController.OnSetupComplete -= SwitchState;
            _shipSetupMenuController.CleanUp();
        }

        public void Init(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void PrepareSetupScene()
        {
            _assetsProvider.PrepareSetupShipRoots();
            _shipsInitializer.PrepareShips();
            _shipSetupMenuController =
                new ShipSetupMenuController(_assetsProvider, _staticDataService, _uiConfig, _stateMachine.CoroutineRunner);
            _shipSetupMenuController.PrepareUi(_shipsInitializer.Ships);
            _shipSetupMenuController.OnSetupComplete += SwitchState;
        }

        private void SwitchState() 
            => _curtain.ShowCurtain(callback: _stateMachine.Enter<LoadBattleState>);
    }
}