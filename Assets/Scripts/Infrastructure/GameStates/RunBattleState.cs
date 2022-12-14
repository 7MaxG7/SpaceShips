using System;
using Abstractions.Services;
using Abstractions.Ships;
using Abstractions.Ui;
using Services;
using Zenject;


namespace Infrastructure
{
    internal sealed class RunBattleState : IRunBattleState
    {
        public event Action OnStateChange;

        private readonly ICurtain _curtain;
        private readonly IShipsInteractor _shipsInteractor;
        private readonly IControllersHolder _controllers;
        private readonly ISceneLoader _sceneLoader;
        private readonly IBattleUi _battleUi;


        [Inject]
        public RunBattleState(ICurtain curtain, IShipsInteractor shipsInteractor, IControllersHolder controllers
            , ISceneLoader sceneLoader, IBattleUi battleUi)
        {
            _curtain = curtain;
            _shipsInteractor = shipsInteractor;
            _controllers = controllers;
            _sceneLoader = sceneLoader;
            _battleUi = battleUi;
        }
        
        public void Enter()
        {
            _curtain.HideCurtain(callback: StartBattle);
            _shipsInteractor.OnWinnerDefined += HandleBattleStop;
            _battleUi.OnBattleLeaved += LeaveBattle;
        }

        public void Exit()
        {
            _shipsInteractor.OnWinnerDefined -= HandleBattleStop;
            _battleUi.OnBattleLeaved -= LeaveBattle;
        }

        private void StartBattle()
        {
            foreach (var ship in _shipsInteractor.Ships.Values)
            {
                _controllers.AddController(ship.Health);
                _controllers.AddController(ship.WeaponBattery);
                ship.WeaponBattery.ToggleShooting(true);
            }
        }

        private void HandleBattleStop(IShip winner)
        {
            foreach (var ship in _shipsInteractor.Ships.Values)
            {
                ship.WeaponBattery.ToggleShooting(false);
                _controllers.RemoveController(ship.Health);
                _controllers.RemoveController(ship.WeaponBattery);
            }

            _battleUi.ShowBattleEnd(winner);
        }

        private void LeaveBattle()
            => _curtain.ShowCurtain(callback: () => OnStateChange?.Invoke());
    }
}