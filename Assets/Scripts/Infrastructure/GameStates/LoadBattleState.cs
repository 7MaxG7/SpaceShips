using System;
using Services;
using Utils;
using Zenject;


namespace Infrastructure
{
    internal sealed class LoadBattleState : ILoadBattleState
    {
        public event Action OnStateChange;

        private readonly ISceneLoader _sceneLoader;


        [Inject]
        public LoadBattleState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;

        }

        public void Enter()
        {
            _sceneLoader.LoadScene(Constants.BATTLE_SCENE_NAME, PrepareSceneAsync);


            async void PrepareSceneAsync()
            {
                OnStateChange?.Invoke();
            }
        }

        public void Exit()
        {
        }
    }
}