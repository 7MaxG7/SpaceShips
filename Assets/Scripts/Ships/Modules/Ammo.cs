using System;
using Abstractions.Ships;
using Enums;
using Ships.Views;
using UnityEngine;

namespace Ships.Modules
{
    internal class Ammo : IAmmo
    {
        public event Action<IAmmo> OnTargetReached;
        
        public WeaponType WeaponType { get; }
        public Transform Transform => _ammoView.transform;
        public Rigidbody2D Rigidbody => _ammoView.Rigidbody;

        private readonly AmmoView _ammoView;

        
        public Ammo(AmmoView view, WeaponType weaponType)
        {
            WeaponType = weaponType;
            _ammoView = view;
        }

        public void Activate(Transform source)
        {
            _ammoView.gameObject.SetActive(true);
            Transform.position = source.position;
            Transform.rotation = source.rotation;
        }

        public void Deactivate()
        {
            Transform.position = Vector3.zero;
            Rigidbody.velocity = Vector2.zero;
            _ammoView.gameObject.SetActive(false);
        }

        public void CleanUp()
        {
            
        }
    }
}