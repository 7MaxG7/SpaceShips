using Enums;
using UnityEngine;

namespace Ui
{
    public class ShipSetupPanelView : MonoBehaviour
    {
        [SerializeField] private OpponentId _opponentId;
        [SerializeField] private Transform _weaponSlotsContent;
        [SerializeField] private Transform _moduleSlotsContent;
        [SerializeField] private Transform[] _weaponSelectAnchors;
        [SerializeField] private Transform[] _moduleSelectAnchors;

        public OpponentId OpponentId => _opponentId;
        public Transform WeaponSlotsContent => _weaponSlotsContent;
        public Transform ModuleSlotsContent => _moduleSlotsContent;

        public Transform GetWeaponSelectAnchor(int index)
        {
            return _weaponSelectAnchors.Length > index ? _weaponSelectAnchors[index] : null;
        }

        public Transform GetModuleSelectAnchor(int index)
        {
            return _moduleSelectAnchors.Length > index ? _moduleSelectAnchors[index] : null;
        }
    }
}