using System;
using Abstractions.Services;
using Abstractions.Ui;
using Services;
using Sounds;
using Utils;
using Zenject;


namespace Infrastructure
{
    internal sealed class LeaveBattleState : ILeaveBattleState
    {
        public event Action OnStateChange;

        private readonly IShipsInteractor _shipsInteractor;
        private readonly IControllersHolder _controllers;
        private readonly ISceneLoader _sceneLoader;
        private readonly IBattleUi _battleUi;
        private readonly IAmmoPool _ammoPool;
        private readonly ISoundPlayer _soundPlayer;


        [Inject]
        public LeaveBattleState(IShipsInteractor shipsInteractor, IControllersHolder controllers,
            ISceneLoader sceneLoader, IBattleUi battleUi, IAmmoPool ammoPool, ISoundPlayer soundPlayer)
        {
            _shipsInteractor = shipsInteractor;
            _controllers = controllers;
            _sceneLoader = sceneLoader;
            _battleUi = battleUi;
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
            }
            _shipsInteractor.CleanUp();
            _battleUi.CleanUp();
            _ammoPool.CleanUp();

            _sceneLoader.LoadScene(Constants.SETUP_SCENE_NAME, () => OnStateChange?.Invoke());
        }

        public void Exit()
        {
        }
    }
}