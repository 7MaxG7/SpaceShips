using System;
using Abstractions;
using Abstractions.Ui;
using Services;
using Utils;
using Zenject;


namespace Infrastructure
{
    internal sealed class ShipSetupState : IShipSetupState
    {
        public event Action OnStateChange;

        private readonly ISceneLoader _sceneLoader;
        private readonly IShipSetupMenuController _shipSetupMenuController;
        private readonly IShipsInitializer _shipsInitializer;


        [Inject]
        public ShipSetupState(ISceneLoader sceneLoader, IShipSetupMenuController shipSetupMenuController
            , IShipsInitializer shipsInitializer)
        {
            _shipsInitializer = shipsInitializer;
            _sceneLoader = sceneLoader;
            _shipSetupMenuController = shipSetupMenuController;
        }

        public void Enter()
        {
            _sceneLoader.LoadScene(Constants.SETUP_SCENE_NAME, PrepareSetupScene);
        }

        public void Exit()
        {
            _shipSetupMenuController.OnSetupComplete -= SwitchState;
        }

        private void PrepareSetupScene()
        {
            _shipsInitializer.PrepareShips();
            _shipSetupMenuController.PrepareUi(_shipsInitializer.Ships);
            _shipSetupMenuController.OnSetupComplete += SwitchState;
        }

        private void SwitchState()
        {
            _shipSetupMenuController.OnSetupComplete -= SwitchState;
            _shipSetupMenuController.CleanUp();
            OnStateChange?.Invoke();
        }
    }
}