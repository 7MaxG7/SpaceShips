namespace Infrastructure
{
    internal interface IGame : ICleaner
    {
        IControllersHolder Controllers { get; }
        ICoroutineRunner CoroutineRunner { get; }
        void Init(ICoroutineRunner coroutineRunner);
    }
}