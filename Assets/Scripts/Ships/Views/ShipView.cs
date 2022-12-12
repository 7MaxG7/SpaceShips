using UnityEngine;

namespace Ships
{
    internal class ShipView : MonoBehaviour
    {
        [SerializeField] private Transform[] _weaponSlots;
        [SerializeField] private Transform[] _moduleSlots;

        public Transform[] WeaponSlots => _weaponSlots;

        public Transform[] ModuleSlots => _moduleSlots;
    }
}