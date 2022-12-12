using System;
using System.Collections.Generic;
using System.Linq;
using Abstractions.Services;
using Abstractions.Ships;
using Abstractions.Ui;
using Enums;
using UnityEngine;
using Zenject;

namespace Ui.ShipSetup.Controllers
{
    internal class ShipSetupMenuController : IShipSetupMenuController
    {
        public event Action OnSetupComplete;

        private readonly IAssetsProvider _assetsProvider;
        private ShipSetupMenuView _shipSetupMenuView;
        private readonly Dictionary<OpponentId, ShipPanelController> _shipPanels = new();
        private Dictionary<OpponentId, IShip> _ships;


        [Inject]
        public ShipSetupMenuController(IAssetsProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }

        public void PrepareUi(Dictionary<OpponentId, IShip> ships)
        {
            CleanUp();
            
            _ships = ships;
            if (_shipSetupMenuView == null)
                _shipSetupMenuView = _assetsProvider.CreateShipSetupMenu();
            
            _shipSetupMenuView.Init();
            _shipSetupMenuView.WeaponSelectPanel.OnComponentSelect += SelectShipWeapon;
            _shipSetupMenuView.ModuleSelectPanel.OnComponentSelect += SelectShipModule;
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
            _shipSetupMenuView.HideUnnecessaryPanels();
        }

        public void CleanUp()
        {
            foreach (var panel in _shipPanels.Values)
            {
                panel.CleanUp();
            }
            _shipPanels.Clear();
            
            _shipSetupMenuView.OnHideAllPanelsClick -= _shipSetupMenuView.HideUnnecessaryPanels;
            _shipSetupMenuView.OnSetupComplete -= InvokeSetupComplete;
        }

        private void InvokeSetupComplete()
        {
            OnSetupComplete?.Invoke();
        }

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