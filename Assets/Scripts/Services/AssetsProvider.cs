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
        private readonly Transform _shipsParent;
        private readonly Transform _ammosParent;
        private Transform _rootCanvas;


        [Inject]
        public AssetsProvider(IStaticDataService staticDataService, SoundConfig soundConfig, UiConfig uiConfig)
        {
            _staticDataService = staticDataService;
            _soundConfig = soundConfig;
            _uiConfig = uiConfig;
            _shipsParent = new GameObject(Constants.SHIPS_PARENT_NAME).transform;
            _ammosParent = new GameObject(Constants.AMMOS_PARENT_NAME).transform;
        }

        public ShipSetupMenuView CreateShipSetupMenu()
        {
            _rootCanvas = Instantiate(_uiConfig.RootCanvas, _rootCanvas);
            return _uiConfig.ShipSetupMenu;
        }

        public SoundPlayerView CreateSoundPlayer()
        {
            return Instantiate(_soundConfig.SoundPlayerPrefab);
        }

        public ShipView CreateShip(ShipType shipType, Vector3 position, Quaternion rotation)
        {
            return Instantiate(_staticDataService.GetShipData(shipType).Prefab, position, rotation, _shipsParent);
        }

        public AmmoView CreateAmmo(WeaponType weaponType)
        {
            return Instantiate(_staticDataService.GetWeaponData(weaponType).AmmoPrefab, _ammosParent);
        }

        public SlotUiView CreateComponentUiSlot(Transform parent)
        {
            return Instantiate(_uiConfig.EquipSlotUiPrefab, parent);
        }

        public Sprite GetWeaponIcon(WeaponType weaponType)
        {
            return _staticDataService.GetWeaponData(weaponType).Icon;
        }

        public Sprite GetModuleIcon(ModuleType moduleType)
        {
            return _staticDataService.GetModuleData(moduleType).Icon;
        }

        public WeaponView CreateWeapon(WeaponType weaponType, Transform parent)
        {
            return Instantiate(_staticDataService.GetWeaponData(weaponType).Prefab, parent);
        }

        public ModuleView CreateModule(ModuleType moduleType, Transform parent)
        {
            return Instantiate(_staticDataService.GetModuleData(moduleType).Prefab, parent);
        }
    }
}