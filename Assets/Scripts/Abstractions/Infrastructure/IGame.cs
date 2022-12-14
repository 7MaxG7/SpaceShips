namespace Infrastructure
{
    internal interface IGame : ICleaner
    {
        IControllersHolder Controllers { get; }
        void Init(ICoroutineRunner coroutineRunner);
    }
}