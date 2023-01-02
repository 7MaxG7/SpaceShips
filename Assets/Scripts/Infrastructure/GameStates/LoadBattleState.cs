using Abstractions;
using Abstractions.Services;
using Services;
using Utils;
using Zenject;


namespace Infrastructure
{
    internal sealed class LoadBattleState : IGameState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IBattleObserver _battleObserver;
        private readonly IAssetsProvider _assetsProvider;
        private readonly IShipsInitializer _shipsInitializer;
        private readonly IAmmoFactory _ammoFactory;
        private readonly IUiFactory _uiFactory;
        private readonly IDamageHandler _damageHandler;
        private IGameStateMachine _stateMachine;


        [Inject]
        public LoadBattleState(ISceneLoader sceneLoader, IBattleObserver battleObserver, IAssetsProvider assetsProvider
            , IShipsInitializer shipsInitializer, IAmmoFactory ammoFactory, IUiFactory uiFactory, IDamageHandler damageHandler)
        {
            _sceneLoader = sceneLoader;
            _battleObserver = battleObserver;
            _assetsProvider = assetsProvider;
            _shipsInitializer = shipsInitializer;
            _ammoFactory = ammoFactory;
            _uiFactory = uiFactory;
            _damageHandler = damageHandler;
        }

        public void Enter()
            => _sceneLoader.LoadScene(Constants.BATTLE_SCENE_NAME, PrepareSceneAsync);

        public void Exit()
        {
        }

        public void Init(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private async void PrepareSceneAsync()
        {
            await _assetsProvider.WarmUpCurrentSceneAsync();
            await _uiFactory.PrepareCanvasAsync();
            _ammoFactory.PrepareRoot();
            PrepareOpponents();
            
            _stateMachine.Enter<RunBattleState>();
        }

        private void PrepareOpponents()
        {
            _shipsInitializer.PrepareShipsAsync();
            
            foreach (var ship in _shipsInitializer.Ships.Values)
            {
                ship.PrepareToBattle();
                _battleObserver.AddShip(ship);
                _damageHandler.AddShip(ship);
            }
        }
    }
}