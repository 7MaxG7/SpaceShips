using System;


namespace Infrastructure
{
    internal interface IGameStateMachine : ICleaner
    {
        IGameState GetState(Type stateType);
        void Enter<TState>() where TState : class, IGameState;
    }
}