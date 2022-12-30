using Abstractions.Services;
using Services;
using Sounds;
using Utils;
using Zenject;


namespace Infrastructure
{
    internal sealed class LeaveBattleState : IGameState
    {
        private readonly IShipsInteractor _shipsInteractor;
        private readonly IUpdater _controllers;
        private readonly ISceneLoader _sceneLoader;
        private readonly IAmmoPool _ammoPool;
        private readonly ISoundPlayer _soundPlayer;
        private IGameStateMachine _stateMachine;


        [Inject]
        public LeaveBattleState(IShipsInteractor shipsInteractor, IUpdater controllers,
            ISceneLoader sceneLoader, IAmmoPool ammoPool, ISoundPlayer soundPlayer)
        {
            _shipsInteractor = shipsInteractor;
            _controllers = controllers;
            _sceneLoader = sceneLoader;
            _ammoPool = ammoPool;
            _soundPlayer = soundPlayer;
        }

        public void Enter()
        {
            foreach (var ship in _shipsInteractor.Ships.Values)
            {
                _controllers.RemoveController(ship.Health);
                _controllers.RemoveController(ship.WeaponBattery);
                ship.WeaponBattery.OnShoot -= _soundPlayer.PlayShoot;
                ship.CleanUpView();
            }
            _shipsInteractor.CleanUp();
            _ammoPool.CleanUp();

            _sceneLoader.LoadScene(Constants.SETUP_SCENE_NAME, _stateMachine.Enter<ShipSetupState>);
        }

        public void Exit()
        {
        }

        public void Init(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
    }
}