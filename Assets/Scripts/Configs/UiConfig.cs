using Ui;
using Ui.ShipSetup;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/" + nameof(UiConfig), fileName = nameof(UiConfig))]
    public class UiConfig : ScriptableObject
    {
        [SerializeField] private Transform _rootCanvas;
        [SerializeField] private float _fadeAnimDuration;
        [Header("Curtain")]
        [SerializeField] private CurtainView _curtainPrefab;
        [SerializeField] private float _curtainAnimDuration;
        [Header("Ship setup scene")]
        [SerializeField] private ShipSetupMenuView _shipSetupMenu;
        [SerializeField] private SlotUiView _slotUiPrefab;
        [SerializeField] private ShipSlotUiView _shipSlotUiPrefab;
        [Header("Battle scene")]
        [SerializeField] private BattleUiView _battleUiPrefab;

        public ShipSetupMenuView ShipSetupMenu => _shipSetupMenu;
        public ShipSlotUiView ShipSlotUiPrefab => _shipSlotUiPrefab;
        public SlotUiView SlotUiPrefab => _slotUiPrefab;
        public BattleUiView BattleUiPrefab => _battleUiPrefab;
        public Transform RootCanvas => _rootCanvas;
        public CurtainView CurtainPrefab => _curtainPrefab;
        public float CurtainAnimDuration => _curtainAnimDuration;
        public float FadeAnimDuration => _fadeAnimDuration;
    }
}