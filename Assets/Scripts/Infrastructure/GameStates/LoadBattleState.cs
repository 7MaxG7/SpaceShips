using System;
using Abstractions;
using Abstractions.Services;
using Abstractions.Ui;
using Services;
using Sounds;
using Utils;
using Zenject;


namespace Infrastructure
{
    internal sealed class LoadBattleState : ILoadBattleState
    {
        public event Action OnStateChange;

        private readonly ISceneLoader _sceneLoader;
        private readonly IShipsInteractor _shipsInteractor;
        private readonly ILocationFinder _locationFinder;
        private readonly IShipsFactory _shipsFactory;
        private readonly IWeaponFactory _weaponFactory;
        private readonly IAmmoPool _ammoPool;
        private readonly IBattleUi _battleUi;
        private readonly IAssetsProvider _assetsProvider;
        private readonly IModuleFactory _moduleFactory;
        private readonly IShipsInitializer _shipsInitializer;
        private readonly ISoundPlayer _soundPlayer;


        [Inject]
        public LoadBattleState(ISceneLoader sceneLoader, IShipsInteractor shipsInteractor, ILocationFinder locationFinder
            , IShipsFactory shipsFactory, IWeaponFactory weaponFactory, IAmmoPool ammoPool, IBattleUi battleUi
            , IAssetsProvider assetsProvider, IModuleFactory moduleFactory, IShipsInitializer shipsInitializer
            , ISoundPlayer soundPlayer)
        {
            _sceneLoader = sceneLoader;
            _shipsInteractor = shipsInteractor;
            _locationFinder = locationFinder;
            _shipsFactory = shipsFactory;
            _weaponFactory = weaponFactory;
            _ammoPool = ammoPool;
            _battleUi = battleUi;
            _assetsProvider = assetsProvider;
            _moduleFactory = moduleFactory;
            _shipsInitializer = shipsInitializer;
            _soundPlayer = soundPlayer;
        }

        public void Enter()
            => _sceneLoader.LoadScene(Constants.BATTLE_SCENE_NAME, PrepareScene);

        public void Exit()
        {
        }

        private void PrepareScene()
        {
            _assetsProvider.PrepareBattleRoots();
            _battleUi.PrepareUi(_shipsInitializer.Ships);
            PrepareOpponents();
            
            OnStateChange?.Invoke();
        }

        private void PrepareOpponents()
        {
            _ammoPool.Init();
            foreach (var opponent in _shipsInitializer.Ships)
            {
                var position = _locationFinder.GetOpponentLocation(opponent.Key, out var rotation);
                if (!position.HasValue)
                    continue;
                var view = _shipsFactory.GenerateView(opponent.Value, position.Value, rotation);

                var weapons = opponent.Value.WeaponBattery;
                for (var i = 0; i < weapons.MaxEquipmentsAmount; i++)
                {
                    var weapon = weapons.GetEquipment(i);
                    if (weapon != null)
                        _weaponFactory.GenerateView(weapon, weapons.GetSlotTransform(i));
                }
                weapons.OnShoot += _soundPlayer.PlayShoot;

                var modules = opponent.Value.ShipModules;
                for (var i = 0; i < modules.MaxEquipmentsAmount; i++)
                {
                    var module = modules.GetEquipment(i);
                    if (module != null)
                        _moduleFactory.GenerateView(module, modules.GetSlotTransform(i));
                }

                _shipsInteractor.AddShip(opponent.Value, view);
            }
        }
    }
}