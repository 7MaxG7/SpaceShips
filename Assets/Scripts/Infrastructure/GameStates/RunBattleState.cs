using System.Threading.Tasks;
using Abstractions;
using Abstractions.Services;
using Abstractions.Ships;
using Ui.Battle;
using Zenject;


namespace Infrastructure
{
    internal sealed class RunBattleState : IGameState
    {
        private readonly ICurtain _curtain;
        private readonly IBattleObserver _battleObserver;
        private readonly IShipsInitializer _shipsInitializer;
        private readonly IUpdater _updater;
        private readonly IUiFactory _uiFactory;
        private IGameStateMachine _stateMachine;
        
        private BattleUiController _battleUi;


        [Inject]
        public RunBattleState(ICurtain curtain, IBattleObserver battleObserver, IShipsInitializer shipsInitializer
            , IUpdater updater, IUiFactory uiFactory)
        {
            _curtain = curtain;
            _battleObserver = battleObserver;
            _shipsInitializer = shipsInitializer;
            _updater = updater;
            _uiFactory = uiFactory;
        }
        
        public async void Enter()
        {
            await SetupUi();
            _battleObserver.OnWinnerDefined += HandleBattleStop;
            _curtain.HideCurtain(callback: StartBattle);
        }

        public void Exit()
        {
            _battleUi.CleanUp();
            _battleObserver.OnWinnerDefined -= HandleBattleStop;
            _battleUi.OnBattleLeft -= LeaveBattle;
        }

        public void Init(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private async Task SetupUi()
        {
            _battleUi = await _uiFactory.CreateBattleUiAsync();
            _battleUi.SetupUi(_shipsInitializer.Ships);
            _battleUi.OnBattleLeft += LeaveBattle;
        }

        private void StartBattle()
        {
            foreach (var ship in _battleObserver.Ships)
            {
                _updater.AddUpdatable(ship.Health);
                _updater.AddUpdatable(ship.WeaponBattery);
                ship.WeaponBattery.ToggleShooting(true);
            }
        }

        private void HandleBattleStop(IShip winner)
        {
            foreach (var ship in _battleObserver.Ships)
            {
                ship.WeaponBattery.ToggleShooting(false);
                _updater.RemoveController(ship.Health);
                _updater.RemoveController(ship.WeaponBattery);
            }

            _battleUi.ShowBattleEnd(winner);
        }

        private void LeaveBattle()
            => _curtain.ShowCurtain(callback: _stateMachine.Enter<LeaveBattleState>);
    }
}