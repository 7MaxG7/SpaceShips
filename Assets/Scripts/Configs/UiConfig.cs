using Ui;
using Ui.ShipSetup;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/" + nameof(UiConfig), fileName = nameof(UiConfig))]
    public class UiConfig : ScriptableObject
    {
        [SerializeField] private CurtainView _curtainPrefab;
        [SerializeField] private Transform _rootCanvas;
        [SerializeField] private ShipSetupMenuView _shipSetupMenu;
        [SerializeField] private SlotUiView _equipSlotUiPrefab;
        [SerializeField] private BattleUiView _battleUiPrefab;
        [SerializeField] private float _curtainAnimDuration;

        public ShipSetupMenuView ShipSetupMenu => _shipSetupMenu;
        public SlotUiView EquipSlotUiPrefab => _equipSlotUiPrefab;
        public BattleUiView BattleUiPrefab => _battleUiPrefab;
        public Transform RootCanvas => _rootCanvas;
        public CurtainView CurtainPrefab => _curtainPrefab;

        public float CurtainAnimDuration => _curtainAnimDuration;
    }
}