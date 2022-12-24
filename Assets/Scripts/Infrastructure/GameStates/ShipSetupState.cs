using System;
using Abstractions;
using Abstractions.Services;
using Abstractions.Ui;
using Sounds;
using Zenject;


namespace Infrastructure
{
    internal sealed class ShipSetupState : IShipSetupState
    {
        public event Action OnStateChange;
        
        private const float CURTAIN_HIDE_DELAY = 0.35f;

        private readonly IShipSetupMenuController _shipSetupMenuController;
        private readonly ICurtain _curtain;
        private readonly IShipsInitializer _shipsInitializer;
        private readonly IAssetsProvider _assetsProvider;
        private readonly ISoundPlayer _soundPlayer;


        [Inject]
        public ShipSetupState(ICurtain curtain, IShipSetupMenuController shipSetupMenuController
            , IShipsInitializer shipsInitializer, IAssetsProvider assetsProvider, ISoundPlayer soundPlayer)
        {
            _curtain = curtain;
            _shipsInitializer = shipsInitializer;
            _assetsProvider = assetsProvider;
            _soundPlayer = soundPlayer;
            _shipSetupMenuController = shipSetupMenuController;
        }

        public void Enter()
        {
            PrepareSetupScene();
            _soundPlayer.PlayMusic();
            // Delay prevents lagging animation on load after bootstrap
            _curtain.HideCurtain(CURTAIN_HIDE_DELAY);
        }

        public void Exit()
        {
            _shipSetupMenuController.OnSetupComplete -= SwitchState;
            _shipSetupMenuController.CleanUp();
        }

        private void PrepareSetupScene()
        {
            _assetsProvider.PrepareSetupShipRoots();
            _shipsInitializer.PrepareShips();
            _shipSetupMenuController.PrepareUi(_shipsInitializer.Ships);
            _shipSetupMenuController.OnSetupComplete += SwitchState;
        }

        private void SwitchState()
        {
            _curtain.ShowCurtain(callback:() => OnStateChange?.Invoke());
        }
    }
}