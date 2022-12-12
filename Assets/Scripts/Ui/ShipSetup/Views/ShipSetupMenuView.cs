using System;
using Enums;
using Infrastructure;
using Ui.ShipSetup;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class ShipSetupMenuView : MonoBehaviour, ICleaner
    {
        [SerializeField] private ShipSetupPanelView[] _shipPanels;
        [SerializeField] private WeaponSelectView _weaponSelectPanel;
        [SerializeField] private ModuleSelectView _moduleSelectPanel;
        [SerializeField] private Button _setupCompleteButton;
        [SerializeField] private Button _hideAllButton;

        public event Action OnHideAllPanelsClick;
        public event Action OnSetupComplete;

        public ShipSetupPanelView[] ShipPanels => _shipPanels;
        public WeaponSelectView WeaponSelectPanel => _weaponSelectPanel;
        public ModuleSelectView ModuleSelectPanel => _moduleSelectPanel;


        public void Init()
        {
            _weaponSelectPanel.Init();
            _moduleSelectPanel.Init();
            _hideAllButton.onClick.AddListener(() => OnHideAllPanelsClick?.Invoke());
            _setupCompleteButton.onClick.AddListener(() => OnSetupComplete?.Invoke());
        }

        public void ShowWeaponSelectPanel(OpponentId opponentId, int index, Transform anchor)
        {
            _weaponSelectPanel.Setup(opponentId, index);
            _weaponSelectPanel.Show();
            _weaponSelectPanel.transform.SetParent(anchor, false);
        }

        public void ShowModuleSelectPanel(OpponentId opponentId, int index, Transform anchor)
        {
            _moduleSelectPanel.Setup(opponentId, index);
            _moduleSelectPanel.Show();
            _moduleSelectPanel.transform.SetParent(anchor, false);
        }

        public void HideWeaponSelectPanel()
            => _weaponSelectPanel.Hide();

        public void HideModuleSelectPanel()
            => _moduleSelectPanel.Hide();

        public void HideUnnecessaryPanels()
        {
            HideWeaponSelectPanel();
            HideModuleSelectPanel();
        }

        public void CleanUp()
        {
            _hideAllButton.onClick.RemoveAllListeners();
            _setupCompleteButton.onClick.RemoveAllListeners();
        }
    }
}