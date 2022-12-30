namespace Infrastructure
{
    internal interface IGameStateMachine : ICleanable
    {
        void Enter<TState>() where TState : class, IGameState;
        void Init(ICoroutineRunner coroutineRunner);
        ICoroutineRunner CoroutineRunner { get; }
    }
}