namespace Infrastructure
{
    public interface IGameStateMachine : ICleanable
    {
        ICoroutineRunner CoroutineRunner { get; }
        
        void Enter<TState>() where TState : class, IGameState;
        void Init(ICoroutineRunner coroutineRunner);
    }
}