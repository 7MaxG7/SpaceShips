using UnityEngine;

namespace Ships.Views
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private Transform _barrel;

        public Transform Barrel => _barrel;
    }
}