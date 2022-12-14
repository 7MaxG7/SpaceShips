using System;
using Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.ShipSetup
{
    public class SlotUiView : MonoBehaviour, ICleaner
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

        public void CleanUp()
        {
            Index = -1;
            _selectButton.onClick.RemoveAllListeners();
        }

        private void InvokeWeaponSelect() 
            => OnSlotClick?.Invoke(Index);

        public void SetIcon(Sprite icon)
        {
            _icon.gameObject.SetActive(icon != null);
            _icon.sprite = icon;
        }
    }
}