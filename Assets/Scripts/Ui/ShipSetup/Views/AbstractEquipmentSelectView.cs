using System;
using Enums;
using Infrastructure;
using UnityEngine;

namespace Ui.ShipSetup
{
    public abstract class AbstractEquipmentSelectView<T> : MonoBehaviour, ICleaner where T : Enum
    {
        [SerializeField] private AbstractEquipmentSlotUiView<T>[] _componentSlots;
        public event Action<OpponentId, int, T> OnEquipmentSelect;

        private OpponentId _opponentId;
        private int _slotIndex;
        
        
        public void Init()
        {
            foreach (var equipmentSlot in _componentSlots)
            {
                equipmentSlot.Init();
                equipmentSlot.OnSlotClick += InvokeEquipmentSelect;
            }
        }

        public void Setup(OpponentId opponentId, int slotIndex)
        {
            _slotIndex = slotIndex;
            _opponentId = opponentId;
        }

        public void Show()
            => gameObject.SetActive(true);

        public void Hide()
            => gameObject.SetActive(false);

        public void CleanUp()
        {
            foreach (var equipmentSlot in _componentSlots) 
                equipmentSlot.OnSlotClick -= InvokeEquipmentSelect;
        }

        private void InvokeEquipmentSelect(T componentType)
            => OnEquipmentSelect?.Invoke(_opponentId, _slotIndex, componentType);
    }
}