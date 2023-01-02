using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstractions.Services;
using Configs;
using Enums;
using Infrastructure;
using Ships;
using UnityEngine;

namespace Ui.ShipSetup.Controllers
{
    public sealed class ShipSetupMenuController : ISceneCleanable
    {
        public event Action OnSetupComplete;

        private IStaticDataService _staticDataService;
        private readonly IShipConfigurationsHolder _configurationsHolder;
        private ICoroutineRunner _coroutineRunner;
        private IUiFactory _uiFactory;
        private UiConfig _uiConfig;
        
        private WeaponSelectPanelController _weaponSelectPanel;
        private ModuleSelectPanelController _moduleSelectPanel;
        private readonly ShipSetupMenuView _shipSetupMenuView;
        private readonly Dictionary<OpponentId,ShipModel> _shipModels;
        private readonly Dictionary<OpponentId, ShipPanelController> _shipPanels = new();


        public ShipSetupMenuController(ShipSetupMenuView view, Dictionary<OpponentId,ShipModel> shipModels)
        {
            _shipSetupMenuView = view;
            _shipModels = shipModels;
        }

        public void CleanUp()
        {
            foreach (var panel in _shipPanels.Values)
            {
                panel.OnWeaponSelectClick -= ShowSelectWeaponPanel;
                panel.OnModuleSelectClick -= ShowSelectModulePanel;
                panel.CleanUp();
            }
            _shipPanels.Clear();

            _weaponSelectPanel.CleanUp();
            _moduleSelectPanel.CleanUp();

            _shipSetupMenuView.SetupCompleteButton.onClick.RemoveAllListeners();
            _shipSetupMenuView.HideAllButton.onClick.RemoveAllListeners();
        }

        public void Init(IStaticDataService staticDataService, IUiFactory uiFactory, ICoroutineRunner coroutineRunner
            , UiConfig uiConfig)
        {
            _staticDataService = staticDataService;
            _uiFactory = uiFactory;
            _coroutineRunner = coroutineRunner;
            _uiConfig = uiConfig;
        }

        public async Task SetupUiAsync(IEnumerable<OpponentId> opponentIds)
        {
            await SetupWeaponSelectPanelAsync();
            await SetupModuleSelectPanelAsync();

            foreach (var opponentId in opponentIds) 
                await InitShipPanelAsync(opponentId);

            _shipSetupMenuView.SetupCompleteButton.onClick.AddListener(() => OnSetupComplete?.Invoke());
            _shipSetupMenuView.HideAllButton.onClick.AddListener(HideSelectPanels);
        }

        private async Task SetupModuleSelectPanelAsync()
        {
            _moduleSelectPanel = new ModuleSelectPanelController(_shipSetupMenuView.ModuleSelectPanel, _shipModels);
            _shipSetupMenuView.ModuleSelectPanel.Init(_uiFactory, _coroutineRunner, _uiConfig.FadeAnimDuration);
            await _moduleSelectPanel.SetupModuledSelectPanelAsync(_staticDataService.GetAllEnabledModulesData());
        }

        private async Task SetupWeaponSelectPanelAsync()
        {
            _weaponSelectPanel = new WeaponSelectPanelController(_shipSetupMenuView.WeaponSelectPanel, _shipModels);
            _shipSetupMenuView.WeaponSelectPanel.Init(_uiFactory, _coroutineRunner, _uiConfig.FadeAnimDuration);
            await _weaponSelectPanel.SetupWeaponSelectPanelAsync(_staticDataService.GetAllEnabledWeaponsData());
        }

        private async Task InitShipPanelAsync(OpponentId opponentId)
        {
            var shipPanelView = _shipSetupMenuView.ShipPanels.FirstOrDefault(view => view.OpponentId == opponentId);
            if (shipPanelView == null)
            {
                Debug.LogError($"{this}: No ship setup panel for opponent {opponentId}");
                return;
            }

            var weaponIcons = _staticDataService.GetAllEnabledWeaponsData()
                .ToDictionary(data => data.WeaponType, data => data.Icon);
            var moduleIcons = _staticDataService.GetAllEnabledModulesData()
                .ToDictionary(data => data.ModuleType, data => data.Icon);

            var shipPanel = new ShipPanelController(shipPanelView, _uiFactory, weaponIcons, moduleIcons);
            var shipData = _staticDataService.GetShipData(_shipModels[opponentId].ShipType);
            await shipPanel.InitAsync(_shipModels[opponentId], shipData.WeaponSlotsAmount, shipData.ModuleSlotsAmount);
            shipPanel.OnWeaponSelectClick += ShowSelectWeaponPanel;
            shipPanel.OnModuleSelectClick += ShowSelectModulePanel;
            _shipPanels.Add(opponentId, shipPanel);
        }

        private void HideSelectPanels()
        {
            _weaponSelectPanel.Hide();
            _moduleSelectPanel.Hide();
        }

        private void ShowSelectWeaponPanel(OpponentId opponentId, int index)
        {
            _moduleSelectPanel.Hide();
            var anchor = _shipPanels[opponentId].GetEquipmentSelectAnchor(EquipmentType.Weapon, index);
            _weaponSelectPanel.Show(opponentId, index, anchor.position);
        }

        private void ShowSelectModulePanel(OpponentId opponentId, int index)
        {
            _weaponSelectPanel.Hide();
            var anchor = _shipPanels[opponentId].GetEquipmentSelectAnchor(EquipmentType.Module, index);
            _moduleSelectPanel.Show(opponentId, index, anchor.position);
        }
    }
}