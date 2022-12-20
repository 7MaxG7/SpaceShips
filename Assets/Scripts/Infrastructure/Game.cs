using Services;
using Zenject;


namespace Infrastructure
{
    internal sealed class Game : IGame
    {
        public IControllersHolder Controllers { get; private set; }
        public ICoroutineRunner CoroutineRunner { get; private set; }

        private IGameStateMachine _gameStateMachine;
        private ISceneLoader _sceneLoader;


        [Inject]
        private void InjectDependencies(IControllersHolder controllers, IGameStateMachine gameStateMachine
            , ISceneLoader sceneLoader)
        {
            Controllers = controllers;
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            Controllers.AddController(this);
        }

        public void Init(ICoroutineRunner coroutineRunner)
        {
            CoroutineRunner = coroutineRunner;
            _sceneLoader.Init(CoroutineRunner);

            _gameStateMachine.GetState(typeof(LeaveBattleState)).OnStateChange += EnterShipSetupState;
            _gameStateMachine.GetState(typeof(RunBattleState)).OnStateChange += EnterLeaveBattleState;
            _gameStateMachine.GetState(typeof(LoadBattleState)).OnStateChange += EnterRunBattleState;
            _gameStateMachine.GetState(typeof(ShipSetupState)).OnStateChange += EnterLoadBattleState;
            _gameStateMachine.GetState(typeof(GameBootstrapState)).OnStateChange += EnterShipSetupState;
            _gameStateMachine.Enter<GameBootstrapState>();
        }

        public void CleanUp()
        {
            _gameStateMachine.GetState(typeof(GameBootstrapState)).OnStateChange -= EnterShipSetupState;
            _gameStateMachine.GetState(typeof(ShipSetupState)).OnStateChange -= EnterLoadBattleState;
            _gameStateMachine.GetState(typeof(LoadBattleState)).OnStateChange -= EnterRunBattleState;
            _gameStateMachine.GetState(typeof(RunBattleState)).OnStateChange -= EnterLeaveBattleState;
            _gameStateMachine.GetState(typeof(LeaveBattleState)).OnStateChange -= EnterShipSetupState;
            _gameStateMachine.CleanUp();
        }

        private void EnterShipSetupState()
        {
            _gameStateMachine.Enter<ShipSetupState>();
        }

        private void EnterLoadBattleState()
        {
            _gameStateMachine.Enter<LoadBattleState>();
        }

        private void EnterRunBattleState()
        {
            _gameStateMachine.Enter<RunBattleState>();
        }

        private void EnterLeaveBattleState()
        {
            _gameStateMachine.Enter<LeaveBattleState>();
        }
    }
}