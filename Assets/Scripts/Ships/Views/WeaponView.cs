using Abstractions.Ships;
using UnityEngine;

namespace Ships.Views
{
    public class WeaponView : MonoBehaviour, IWeaponView
    {
        [SerializeField] private Transform _barrel;
        [SerializeField] private Transform _shootDirection;

        public Transform Barrel => _barrel;

        public Transform ShootDirection => _shootDirection;
    }
}