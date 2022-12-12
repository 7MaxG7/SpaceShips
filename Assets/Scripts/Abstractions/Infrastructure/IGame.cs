namespace Infrastructure
{
    internal interface IGame
    {
        IControllersHolder Controllers { get; }
        void Init(ICoroutineRunner coroutineRunner);
    }
}