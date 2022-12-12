using System;


namespace Infrastructure
{
    internal interface IGameStateMachine
    {
        IGameState GetState(Type stateType);
        void Enter<TState>() where TState : class, IGameState;
    }
}