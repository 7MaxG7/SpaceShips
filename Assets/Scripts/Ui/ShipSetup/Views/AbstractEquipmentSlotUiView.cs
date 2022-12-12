using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.ShipSetup
{
    public class AbstractEquipmentSlotUiView<T> : MonoBehaviour where T : Enum
    {
        [SerializeField] private T _weaponType;
        [SerializeField] private Button _weaponSelectButton;

        public event Action<T> OnSlotClick;

        
        public void Init()
        {
            _weaponSelectButton.onClick.AddListener(InvokeSlotClick);
        }

        private void InvokeSlotClick()
        {
            OnSlotClick?.Invoke(_weaponType);
        }
    }
}