using Services;
using Utils;
using Zenject;


namespace Infrastructure
{
    internal sealed class LeaveBattleState : IGameState
    {
        private readonly ISceneLoader _sceneLoader;
        private IGameStateMachine _stateMachine;


        [Inject]
        public LeaveBattleState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.LoadScene(Constants.SETUP_SCENE_NAME, _stateMachine.Enter<ShipSetupState>);
        }

        public void Exit()
        {
        }

        public void Init(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
    }
}