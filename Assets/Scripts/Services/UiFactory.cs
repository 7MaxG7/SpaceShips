using System.Threading.Tasks;
using Abstractions.Services;
using Configs;
using Enums;
using Ui;
using Ui.Battle;
using Ui.ShipSetup;
using Ui.ShipSetup.Controllers;
using UnityEngine;
using Zenject;

namespace Services
{
    public sealed class UiFactory : IUiFactory
    {
        private readonly IAssetsProvider _assetsProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IShipConfigurationsHolder _configurationsHolder;
        private readonly UiConfig _uiConfig;
        
        private Transform _rootCanvas;


        [Inject]
        public UiFactory(IAssetsProvider assetsProvider, IStaticDataService staticDataService
            , IShipConfigurationsHolder configurationsHolder, UiConfig uiConfig)
        {
            _assetsProvider = assetsProvider;
            _staticDataService = staticDataService;
            _configurationsHolder = configurationsHolder;
            _uiConfig = uiConfig;
        }
        
        public async Task PrepareCanvasAsync()
        {
            if (_rootCanvas == null)
                _rootCanvas = (await _assetsProvider.CreateInstanceAsync(_uiConfig.RootCanvas)).transform;
        }

        public async Task<CurtainView> CreateCurtainAsync()
        {
            var curtainView = await _assetsProvider.CreateInstanceAsync<CurtainView>(_uiConfig.CurtainPrefab, isDontDestroyAsset: true);
            curtainView.Init(_uiConfig.CurtainAnimDuration);
            return curtainView;
        }

        public async Task<ShipSetupMenuController> CreateShipSetupMenuAsync()
        {
            var view = await _assetsProvider.CreateInstanceAsync<ShipSetupMenuView>(_uiConfig.ShipSetupMenu, _rootCanvas);
            return new ShipSetupMenuController(view, _configurationsHolder.ShipModels);
        }

        public async Task<BattleUiController> CreateBattleUiAsync()
        {
            var view = await _assetsProvider.CreateInstanceAsync<BattleUiView>(_uiConfig.BattleUiPrefab, _rootCanvas);
            return new BattleUiController(view);
        }

        public async Task<SlotUiView> CreateSelectWeaponUiSlotAsync(WeaponType weaponType, Transform parent)
        {
            var slot = await CreateSelectEquipmentUiSlotAsync(parent);
            slot.SetIcon(_staticDataService.GetWeaponData(weaponType).Icon);
            return slot;
        }

        public async Task<SlotUiView> CreateSelectModuleUiSlotAsync(ModuleType moduleType, Transform parent)
        {
            var slot = await CreateSelectEquipmentUiSlotAsync(parent);
            slot.SetIcon(_staticDataService.GetModuleData(moduleType).Icon);
            return slot;
        }

        public async Task<ShipSlotUiView> CreateEquipmentUiSlotAsync(Transform parent) 
            => await _assetsProvider.CreateInstanceAsync<ShipSlotUiView>(_uiConfig.ShipSlotUiPrefab, parent);

        private async Task<SlotUiView> CreateSelectEquipmentUiSlotAsync(Transform parent) 
            => await _assetsProvider.CreateInstanceAsync<SlotUiView>(_uiConfig.SlotUiPrefab, parent);
    }
}