using System.Threading.Tasks;
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
        private readonly ISoundPlayer _soundPlayer;
        private readonly IStaticDataService _staticDataService;
        private readonly IAssetsProvider _assetsProvider;
        private readonly IUiFactory _uiFactory;
        private readonly ICleaner _cleaner;
        private readonly UiConfig _uiConfig;
        
        private IGameStateMachine _stateMachine;
        private ShipSetupMenuController _shipSetupMenu;


        [Inject]
        public ShipSetupState(ICurtain curtain, IShipsInitializer shipsInitializer, IStaticDataService staticDataService
            , ISoundPlayer soundPlayer, IAssetsProvider assetsProvider, IUiFactory uiFactory, ICleaner cleaner, UiConfig uiConfig)
        {
            _curtain = curtain;
            _shipsInitializer = shipsInitializer;
            _soundPlayer = soundPlayer;
            _staticDataService = staticDataService;
            _assetsProvider = assetsProvider;
            _uiFactory = uiFactory;
            _cleaner = cleaner;
            _uiConfig = uiConfig;
        }

        public async void Enter()
        {
            await _assetsProvider.WarmUpCurrentSceneAsync();
            await PrepareSetupSceneAsync();
            _soundPlayer.PlayMusic();
            // Delay prevents lagging animation on load after bootstrap
            _curtain.HideCurtain(CURTAIN_HIDE_DELAY);
        }

        public void Exit()
        {
            _shipSetupMenu.OnSetupComplete -= SwitchState;
            _shipSetupMenu.CleanUp();
        }

        public void Init(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private async Task PrepareSetupSceneAsync()
        {
            await _uiFactory.PrepareCanvasAsync();
            await _shipsInitializer.PrepareShipsAsync();
            await SetupUiAsync();
        }

        private async Task SetupUiAsync()
        {
            _shipSetupMenu = await _uiFactory.CreateShipSetupMenuAsync();
            _shipSetupMenu.Init(_staticDataService, _uiFactory, _stateMachine.CoroutineRunner, _uiConfig);
            await _shipSetupMenu.SetupUiAsync(_shipsInitializer.Ships.Keys);
            _shipSetupMenu.OnSetupComplete += SwitchState;
            _cleaner.AddCleanable(_shipSetupMenu);
        }

        private void SwitchState() 
            => _curtain.ShowCurtain(callback: _stateMachine.Enter<LoadBattleState>);
    }
}