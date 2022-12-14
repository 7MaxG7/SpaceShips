using Abstractions.Services;
using Configs;
using Enums;
using Ships;
using Ships.Views;
using Sounds;
using Ui;
using Ui.ShipSetup;
using UnityEngine;
using Utils;
using Zenject;
using static UnityEngine.Object;


namespace Services
{
    internal sealed class AssetsProvider : IAssetsProvider
    {
        private readonly IStaticDataService _staticDataService;
        private readonly SoundConfig _soundConfig;
        private readonly UiConfig _uiConfig;
        private Transform _shipsParent;
        private Transform _ammosParent;
        private Transform _rootCanvas;


        [Inject]
        public AssetsProvider(IStaticDataService staticDataService, SoundConfig soundConfig, UiConfig uiConfig)
        {
            _staticDataService = staticDataService;
            _soundConfig = soundConfig;
            _uiConfig = uiConfig;
        }

        public CurtainView CreateCurtain()
        {
            return Instantiate(_uiConfig.CurtainPrefab);
        }

        public void PrepareSetupShipRoots()
        {
            if (_rootCanvas == null)
                _rootCanvas = Instantiate(_uiConfig.RootCanvas);
            if (_shipsParent == null)
                _shipsParent = new GameObject(Constants.SHIPS_PARENT_NAME).transform;
        }

        public void PrepareBattleRoots()
        {
            if (_rootCanvas == null)
                _rootCanvas = Instantiate(_uiConfig.RootCanvas);
            if (_shipsParent == null)
                _shipsParent = new GameObject(Constants.SHIPS_PARENT_NAME).transform;
            if (_ammosParent == null)
                _ammosParent = new GameObject(Constants.AMMOS_PARENT_NAME).transform;
        }

        public ShipSetupMenuView CreateShipSetupMenu() 
            => Instantiate(_uiConfig.ShipSetupMenu, _rootCanvas);

        public BattleUiView CreateBattleUi()
            => Instantiate(_uiConfig.BattleUiPrefab, _rootCanvas);

        public SoundPlayerView CreateSoundPlayer()
            => Instantiate(_soundConfig.SoundPlayerPrefab);

        public ShipView CreateShip(ShipType shipType, Vector3 position, Quaternion rotation)
            => Instantiate(_staticDataService.GetShipData(shipType).Prefab, position, rotation, _shipsParent);

        public AmmoView CreateAmmo(WeaponType weaponType)
            => Instantiate(_staticDataService.GetWeaponData(weaponType).AmmoPrefab, _ammosParent);

        public SlotUiView CreateEquipmentUiSlot(Transform parent)
            => Instantiate(_uiConfig.EquipSlotUiPrefab, parent);

        public Sprite GetWeaponIcon(WeaponType weaponType)
            => _staticDataService.GetWeaponData(weaponType).Icon;

        public Sprite GetModuleIcon(ModuleType moduleType)
            => _staticDataService.GetModuleData(moduleType).Icon;

        public WeaponView CreateWeapon(WeaponType weaponType, Transform parent)
            => Instantiate(_staticDataService.GetWeaponData(weaponType).Prefab, parent);

        public ModuleView CreateModule(ModuleType moduleType, Transform parent)
            => Instantiate(_staticDataService.GetModuleData(moduleType).Prefab, parent);
    }
}