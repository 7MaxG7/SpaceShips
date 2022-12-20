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
        [SerializeField] private SlotUiView _slotUiPrefab;
        [SerializeField] private ShipSlotUiView _shipSlotUiPrefab;
        [SerializeField] private BattleUiView _battleUiPrefab;
        [SerializeField] private float _fadeAnimDuration;

        public ShipSetupMenuView ShipSetupMenu => _shipSetupMenu;
        public ShipSlotUiView ShipSlotUiPrefab => _shipSlotUiPrefab;
        public SlotUiView SlotUiPrefab => _slotUiPrefab;
        public BattleUiView BattleUiPrefab => _battleUiPrefab;
        public Transform RootCanvas => _rootCanvas;
        public CurtainView CurtainPrefab => _curtainPrefab;
        public float FadeAnimDuration => _fadeAnimDuration;
    }
}