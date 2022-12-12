using System;
using Enums;
using UnityEngine;

namespace Ui.ShipSetup
{
    public abstract class AbstractEquipmentSelectView<T> : MonoBehaviour where T : Enum
    {
        [SerializeField] private AbstractEquipmentSlotUiView<T>[] _componentSlots;
        private OpponentId _opponentId;
        private int _slotIndex;
        public event Action<OpponentId, int, T> OnComponentSelect;

        public void Init()
        {
            foreach (var componentSlot in _componentSlots)
            {
                componentSlot.Init();
                componentSlot.OnSlotClick += InvokeComponentSelect;
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

        private void InvokeComponentSelect(T componentType)
        {
            OnComponentSelect?.Invoke(_opponentId, _slotIndex, componentType);
        }
    }
}