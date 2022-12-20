using System;
using System.Collections.Generic;
using System.Linq;
using Abstractions.Services;
using Abstractions.Ships;
using Enums;
using Infrastructure;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui.ShipSetup
{
    internal class ShipPanelController : ICleaner
    {
        public event Action<OpponentId, int> OnWeaponSelectClick;
        public event Action<OpponentId, int> OnModuleSelectClick;

        private readonly IAssetsProvider _assetsProvider;
        private readonly ShipSetupPanelView _shipPanelView;
        private readonly List<ShipSlotUiView> _weaponSlots = new();
        private readonly List<ShipSlotUiView> _moduleSlots = new();
        private bool _isCleaned;

        private OpponentId OpponentId => _shipPanelView.OpponentId;
        

        public ShipPanelController(IAssetsProvider assetsProvider, ShipSetupPanelView shipPanelView)
        {
            _assetsProvider = assetsProvider;
            _shipPanelView = shipPanelView;
        }

        public void Init(IShip ship)
        {
            InitWeaponSlots(ship.WeaponBattery);
            InitModuleSlots(ship.ShipModules);
        }

        public void SwitchEquipmentIcon(int slotIndex, EquipmentType type, int typeIndex)
        {
            SlotUiView slot = null;
            Sprite icon = null;
            switch (type)
            {
                case EquipmentType.Weapon:
                    slot = _weaponSlots.FirstOrDefault(item => item.Index == slotIndex);
                    icon = _assetsProvider.GetWeaponIcon((WeaponType)typeIndex);
                    break;
                case EquipmentType.Module:
                    slot = _moduleSlots.FirstOrDefault(item => item.Index == slotIndex);
                    icon = _assetsProvider.GetModuleIcon((ModuleType)typeIndex);
                    break;
                default:
                    Debug.LogError($"{this}: No icon for type {type.ToString()}");
                    break;
            }
            if (slot != null)
                slot.SetIcon(icon);
        }

        public Transform GetEquipmentSelectAnchor(EquipmentType type, int index)
        {
            switch (type)
            {
                case EquipmentType.Weapon:
                    return _weaponSlots.FirstOrDefault(slot => slot.Index == index)?.SelectPanelAnchor;
                case EquipmentType.Module:
                    return _moduleSlots.FirstOrDefault(slot => slot.Index == index)?.SelectPanelAnchor;
                default:
                    Debug.LogError($"{this}: No anchor for type {type.ToString()} index {index}");
                    return null;
            }
        }

        public void CleanUp()
        {
            if (_isCleaned)
                return;
            _isCleaned = true;
            
            foreach (var slot in _weaponSlots)
            {
                slot.SelectButton.onClick.RemoveAllListeners();
                if (slot != null && slot.gameObject != null)
                    Object.Destroy(slot.gameObject);
            }
            _weaponSlots.Clear();
            
            foreach (var slot in _moduleSlots)
            {
                slot.SelectButton.onClick.RemoveAllListeners();
                if(slot != null && slot.gameObject != null)
                    Object.Destroy(slot.gameObject);
            }
            _moduleSlots.Clear();
        }

        private void InitWeaponSlots(IWeaponBattery shipWeapons)
        {
            CheckSlotsAmount(shipWeapons.MaxEquipmentsAmount, _weaponSlots, _shipPanelView.WeaponSlotsContent, out var newSlots);
            foreach (var slot in newSlots)
            {
                var shipSlot = slot;
                shipSlot.SelectButton.onClick.AddListener(() => InvokeWeaponSelect(shipSlot.Index));
            }
            for (var i = 0; i < _weaponSlots.Count; i++)
            {
                if (!TryInitSlot(shipWeapons.MaxEquipmentsAmount, _weaponSlots[i], i))
                    continue;
                var weapon = shipWeapons.GetEquipment(i);
                if (weapon == null)
                    continue;

                _weaponSlots[i].SetIcon(_assetsProvider.GetWeaponIcon(weapon.WeaponType));
            }
        }

        private void InitModuleSlots(IShipModules shipModules)
        {
            CheckSlotsAmount(shipModules.MaxEquipmentsAmount, _moduleSlots, _shipPanelView.ModuleSlotsContent, out var newSlots);
            foreach (var slot in newSlots)
            {
                var shipSlot = slot;
                shipSlot.SelectButton.onClick.AddListener(() => InvokeModuleSelect(shipSlot.Index));
            }
            for (var index = 0; index < _moduleSlots.Count; index++)
            {
                if (!TryInitSlot(shipModules.MaxEquipmentsAmount, _moduleSlots[index], index))
                    continue;
                var module = shipModules.GetEquipment(index);
                if (module == null)
                    continue;

                _moduleSlots[index].SetIcon(_assetsProvider.GetModuleIcon(module.ModuleType));
            }
        }

        private bool TryInitSlot(int maxAmount, ShipSlotUiView slot, int index)
        {
            if (index >= maxAmount)
            {
                slot.SelectButton.onClick.RemoveAllListeners();
                slot.gameObject.SetActive(false);
                return false;
            }

            slot.Init(OpponentId, index);
            return true;
        }

        private void CheckSlotsAmount(int amount, List<ShipSlotUiView> currentSlots, Transform content, out List<ShipSlotUiView> newSlots)
        {
            newSlots = new List<ShipSlotUiView>();
            currentSlots.ForEach(view => view.gameObject.SetActive(true));
            while (currentSlots.Count < amount)
            {
                var slot = _assetsProvider.CreateEquipmentUiSlot(content);
                currentSlots.Add(slot);
                newSlots.Add(slot);
            }
        }

        private void InvokeWeaponSelect(int slotIndex)
            => OnWeaponSelectClick?.Invoke(OpponentId, slotIndex);
   
        private void InvokeModuleSelect(int slotIndex)
            => OnModuleSelectClick?.Invoke(OpponentId, slotIndex);
    }
}