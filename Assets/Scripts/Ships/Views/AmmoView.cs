using UnityEngine;

namespace Ships.Views
{
    public class AmmoView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        public Rigidbody2D Rigidbody => _rigidbody;
    }
}