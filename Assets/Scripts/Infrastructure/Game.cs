using Services;
using Zenject;


namespace Infrastructure
{
    internal sealed class Game
    {
        public IUpdater Updater { get; }
        public ICleaner Cleaner { get; }

        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        
        
        [Inject]
        public Game(IUpdater updater, ICleaner cleaner, IGameStateMachine gameStateMachine
            , ISceneLoader sceneLoader)
        {
            Updater = updater;
            Cleaner = cleaner;
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            Cleaner.AddCleanable(Updater);
        }

        public void Init(ICoroutineRunner coroutineRunner)
        {
            _sceneLoader.Init(coroutineRunner);
            _gameStateMachine.Init(coroutineRunner);
            _gameStateMachine.Enter<GameBootstrapState>();
        }
    }
}