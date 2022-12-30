using System;
using System.Collections.Generic;
using System.Linq;
using Abstractions.Services;
using Abstractions.Ships;
using Configs;
using Enums;
using Infrastructure;
using UnityEngine;

namespace Ui.ShipSetup.Controllers
{
    internal class ShipSetupMenuController : ICleanable
    {
        public event Action OnSetupComplete;

        private readonly IAssetsProvider _assetsProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly UiConfig _uiConfig;
        private readonly Dictionary<OpponentId, ShipPanelController> _shipPanels = new();
        private readonly ICoroutineRunner _coroutineRunner;
        private ShipSetupMenuView _shipSetupMenuView;
        private Dictionary<OpponentId, IShip> _ships;


        public ShipSetupMenuController(IAssetsProvider assetsProvider, IStaticDataService staticDataService, UiConfig uiConfig
            , ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
            _assetsProvider = assetsProvider;
            _staticDataService = staticDataService;
            _uiConfig = uiConfig;
        }

        public void PrepareUi(Dictionary<OpponentId, IShip> ships)
        {
            _ships = ships;
            if (_shipSetupMenuView == null)
                _shipSetupMenuView = _assetsProvider.CreateShipSetupMenu();

            _shipSetupMenuView.Init();
            _shipSetupMenuView.WeaponSelectPanel.Init(_assetsProvider, _coroutineRunner, _uiConfig.FadeAnimDuration);
            _shipSetupMenuView.ModuleSelectPanel.Init(_assetsProvider, _coroutineRunner, _uiConfig.FadeAnimDuration);
            _shipSetupMenuView.WeaponSelectPanel.SetupWeaponSelectPanel(_staticDataService.GetAllEnabledWeaponsData());
            _shipSetupMenuView.ModuleSelectPanel.SetupModuledSelectPanel(_staticDataService.GetAllEnabledModulesData());
            _shipSetupMenuView.WeaponSelectPanel.OnEquipmentSelect += SelectShipWeapon;
            _shipSetupMenuView.ModuleSelectPanel.OnEquipmentSelect += SelectShipModule;
            foreach (var opponentId in ships.Keys)
            {
                var shipPanelView = _shipSetupMenuView.ShipPanels.FirstOrDefault(view => view.OpponentId == opponentId);
                if (shipPanelView == null)
                {
                    Debug.LogError($"{this}: No ship setup panel for opponent {opponentId}");
                    continue;
                }

                var shipPanel = new ShipPanelController(_assetsProvider, shipPanelView);
                shipPanel.Init(ships[opponentId]);
                shipPanel.OnWeaponSelectClick += ShowSelectWeaponPanel;
                shipPanel.OnModuleSelectClick += ShowSelectModulePanel;
                _shipPanels.Add(opponentId, shipPanel);
            }

            _shipSetupMenuView.OnHideAllPanelsClick += _shipSetupMenuView.HideUnnecessaryPanels;
            _shipSetupMenuView.OnSetupComplete += InvokeSetupComplete;
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

            foreach (var ship in _ships.Values)
            {
                ship.CleanUpView();
            }

            _shipSetupMenuView.WeaponSelectPanel.OnEquipmentSelect -= SelectShipWeapon;
            _shipSetupMenuView.ModuleSelectPanel.OnEquipmentSelect -= SelectShipModule;
            _shipSetupMenuView.OnHideAllPanelsClick -= _shipSetupMenuView.HideUnnecessaryPanels;
            _shipSetupMenuView.OnSetupComplete -= InvokeSetupComplete;
        }

        private void InvokeSetupComplete()
            => OnSetupComplete?.Invoke();

        private void ShowSelectWeaponPanel(OpponentId opponentId, int index)
        {
            _shipSetupMenuView.HideModuleSelectPanel();
            var anchor = _shipPanels[opponentId].GetEquipmentSelectAnchor(EquipmentType.Weapon, index);
            _shipSetupMenuView.ShowWeaponSelectPanel(opponentId, index, anchor);
        }

        private void ShowSelectModulePanel(OpponentId opponentId, int index)
        {
            _shipSetupMenuView.HideWeaponSelectPanel();
            var anchor = _shipPanels[opponentId].GetEquipmentSelectAnchor(EquipmentType.Module, index);
            _shipSetupMenuView.ShowModuleSelectPanel(opponentId, index, anchor);
        }

        private void SelectShipWeapon(OpponentId opponentId, int slot, WeaponType weaponType)
        {
            _shipSetupMenuView.HideWeaponSelectPanel();
            _shipPanels[opponentId].SwitchEquipmentIcon(slot, EquipmentType.Weapon, (int)weaponType);
            _ships[opponentId].WeaponBattery.SetEquipment(slot, weaponType);
        }

        private void SelectShipModule(OpponentId opponentId, int slot, ModuleType moduleType)
        {
            _shipSetupMenuView.HideModuleSelectPanel();
            _shipPanels[opponentId].SwitchEquipmentIcon(slot, EquipmentType.Module, (int)moduleType);
            _ships[opponentId].ShipModules.SetEquipment(slot, moduleType);
        }
    }
}