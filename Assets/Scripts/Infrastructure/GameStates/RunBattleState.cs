using Abstractions;
using Abstractions.Services;
using Abstractions.Ships;
using Ui.Battle;
using Zenject;


namespace Infrastructure
{
    internal sealed class RunBattleState : IGameState
    {
        private readonly IAssetsProvider _assetsProvider;
        private readonly ICurtain _curtain;
        private readonly IShipsInteractor _shipsInteractor;
        private readonly IShipsInitializer _shipsInitializer;
        private readonly IUpdater _controllers;
        private BattleUi _battleUi;
        private IGameStateMachine _stateMachine;


        [Inject]
        public RunBattleState(IAssetsProvider assetsProvider, ICurtain curtain, IShipsInteractor shipsInteractor
            , IShipsInitializer shipsInitializer, IUpdater controllers)
        {
            _assetsProvider = assetsProvider;
            _curtain = curtain;
            _shipsInteractor = shipsInteractor;
            _shipsInitializer = shipsInitializer;
            _controllers = controllers;
        }
        
        public void Enter()
        {
            _battleUi = new BattleUi(_assetsProvider);
            _battleUi.PrepareUi(_shipsInitializer.Ships);
            _curtain.HideCurtain(callback: StartBattle);
            _shipsInteractor.OnWinnerDefined += HandleBattleStop;
            _battleUi.OnBattleLeaved += LeaveBattle;
        }

        public void Exit()
        {
            _battleUi.CleanUp();
            _shipsInteractor.OnWinnerDefined -= HandleBattleStop;
            _battleUi.OnBattleLeaved -= LeaveBattle;
        }

        public void Init(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void StartBattle()
        {
            foreach (var ship in _shipsInteractor.Ships.Values)
            {
                _controllers.AddUpdatable(ship.Health);
                _controllers.AddUpdatable(ship.WeaponBattery);
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
            => _curtain.ShowCurtain(callback: _stateMachine.Enter<LeaveBattleState>);
    }
}