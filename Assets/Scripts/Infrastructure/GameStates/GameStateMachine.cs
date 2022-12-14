using System;
using System.Collections.Generic;
using Zenject;


namespace Infrastructure
{
    internal sealed class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IGameState> _states;
        private IGameState _currentState;


        [Inject]
        public GameStateMachine(IGameBootstrapState gameBootstrapState, IShipSetupState shipSetupState
            , ILoadBattleState loadBattleState, IRunBattleState runBattleState, ILeaveBattleState leaveBattleState)
        {
            _states = new Dictionary<Type, IGameState>
            {
                [typeof(GameBootstrapState)] = gameBootstrapState,
                [typeof(ShipSetupState)] = shipSetupState,
                [typeof(LoadBattleState)] = loadBattleState,
                [typeof(RunBattleState)] = runBattleState,
                [typeof(LeaveBattleState)] = leaveBattleState
            };
        }

        public IGameState GetState(Type stateType)
        {
            return _states.TryGetValue(stateType, out var state) ? state : null;
        }

        public void Enter<TState>() where TState : class, IGameState
        {
            var newState = SwitchCurrentState<TState>();
            newState.Enter();
        }

        public void CleanUp()
        {
            _currentState = null;
            _states.Clear();
        }

        private TState SwitchCurrentState<TState>() where TState : class, IGameState
        {
            _currentState?.Exit();

            var newState = _states[typeof(TState)] as TState;
            _currentState = newState;

            return newState;
        }
    }
}