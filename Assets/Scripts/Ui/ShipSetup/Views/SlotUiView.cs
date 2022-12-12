using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.ShipSetup
{
    public class SlotUiView : MonoBehaviour
    {
        [SerializeField] private Button _selectButton;
        [SerializeField] private Image _icon;

        public event Action<int> OnSlotClick;
        
        public int Index { get; private set; }

        
        public void Init(int index)
        {
            CleanUp();
            _selectButton.onClick.AddListener(InvokeWeaponSelect);
            Index = index;
            SetIcon(null);
        }

        private void InvokeWeaponSelect()
        {
            OnSlotClick?.Invoke(Index);
        }

        private void CleanUp()
        {
            _selectButton.onClick.RemoveAllListeners();
        }

        public void SetIcon(Sprite icon)
        {
            _icon.gameObject.SetActive(icon != null);
            _icon.sprite = icon;
        }
    }
}