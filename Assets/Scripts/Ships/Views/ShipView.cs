using Abstractions.Ships;
using UnityEngine;

namespace Ships
{
    public class ShipView : MonoBehaviour, IDamagableView
    {
        [SerializeField] private Transform[] _weaponSlots;
        [SerializeField] private Transform[] _moduleSlots;

        public Transform[] WeaponSlots => _weaponSlots;
        public Transform[] ModuleSlots => _moduleSlots;
    }
}