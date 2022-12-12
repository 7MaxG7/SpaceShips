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
        private readonly List<SlotUiView> _weaponSlots = new();
        private readonly List<SlotUiView> _moduleSlots = new();
        private bool _isCleaned;

        private OpponentId OpponentId => _shipPanelView.OpponentId;
        

        public ShipPanelController(IAssetsProvider assetsProvider, ShipSetupPanelView shipPanelView)
        {
            _assetsProvider = assetsProvider;
            _shipPanelView = shipPanelView;
        }

        public void CleanUp()
        {
            if (_isCleaned)
                return;
            _isCleaned = true;
            
            foreach (var obj in _weaponSlots.Where(slot => slot != null && slot.gameObject != null))
                Object.Destroy(obj);
            foreach (var slot in _weaponSlots)
                slot.OnSlotClick -= InvokeWeaponSelect;
            _weaponSlots.Clear();
            foreach (var obj in _moduleSlots.Where(slot => slot != null && slot.gameObject != null))
                Object.Destroy(obj);
            foreach (var slot in _moduleSlots)
                slot.OnSlotClick -= InvokeWeaponSelect;
            _moduleSlots.Clear();
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
                    return _shipPanelView.GetWeaponSelectAnchor(index);
                case EquipmentType.Module:
                    return _shipPanelView.GetModuleSelectAnchor(index);
                default:
                    Debug.LogError($"{this}: No anchor for type {type.ToString()}");
                    return null;
            }
        }

        private void InitWeaponSlots(IWeaponBattery shipWeapons)
        {
            CheckSlotsAmount(shipWeapons.MaxEquipmentsAmount, _weaponSlots, _shipPanelView.WeaponSlotsContent, out var newSlots);
            foreach (var slot in newSlots)
            {
                slot.OnSlotClick += InvokeWeaponSelect;
            }
            for (var i = 0; i < _weaponSlots.Count; i++)
            {
                var slot = _weaponSlots[i];
                slot.Init(i);
                var weapon = shipWeapons.GetEquipment(i);
                if (weapon == null)
                    continue;

                slot.SetIcon(_assetsProvider.GetWeaponIcon(weapon.WeaponType));
            }
        }

        private void InitModuleSlots(IShipModules shipModules)
        {
            CheckSlotsAmount(shipModules.MaxEquipmentsAmount, _moduleSlots, _shipPanelView.ModuleSlotsContent, out var newSlots);
            foreach (var slot in newSlots)
            {
                slot.OnSlotClick += InvokeModuleSelect;
            }
            for (var i = 0; i < _weaponSlots.Count; i++)
            {
                var slot = _weaponSlots[i];
                slot.Init(i);
                var module = shipModules.GetEquipment(i);
                if (module == null)
                    continue;

                slot.SetIcon(_assetsProvider.GetModuleIcon(module.ModuleType));
            }
        }

        private void CheckSlotsAmount(int amount, List<SlotUiView> currentSlots, Transform content, out List<SlotUiView> newSlots)
        {
            newSlots = new List<SlotUiView>();
            currentSlots.ForEach(view => view.gameObject.SetActive(true));
            while (currentSlots.Count < amount)
            {
                var slot = _assetsProvider.CreateComponentUiSlot(content);
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