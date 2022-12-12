using Ui;
using Ui.ShipSetup;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/" + nameof(UiConfig), fileName = nameof(UiConfig))]
    public class UiConfig : ScriptableObject
    {
        [SerializeField] private Transform _rootCanvas;
        [SerializeField] private ShipSetupMenuView _shipSetupMenu;
        [SerializeField] private SlotUiView _equipSlotUiPrefab;

        public ShipSetupMenuView ShipSetupMenu => _shipSetupMenu;
        public SlotUiView EquipSlotUiPrefab => _equipSlotUiPrefab;

        public Transform RootCanvas => _rootCanvas;
    }
}