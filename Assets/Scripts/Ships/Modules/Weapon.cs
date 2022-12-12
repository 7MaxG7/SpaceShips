using System;
using Abstractions.Services;
using Abstractions.Ships;
using Enums;
using Ships.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ships.Modules
{
    internal class Weapon : IWeapon
    {
        public WeaponType WeaponType { get; }
        public bool IsReady => _cooldownTimer <= 0;
        public Transform AmmoBarrel => _weaponView.Barrel;
        public Vector2 ShootDirection { get; }

        private readonly WeaponView _weaponView;
        private float _cooldownTimer;
        private float _shootCooldown;
        private readonly int _damage;
        private readonly IAmmoPool _ammoPool;


        public Weapon(WeaponView view, float cooldown, int damage, WeaponType weaponType, IAmmoPool ammoPool)
        {
            _weaponView = view;
            _shootCooldown = cooldown;
            _damage = damage;
            _ammoPool = ammoPool;
            WeaponType = weaponType;
            ShootDirection = (_weaponView.ShootDirection.position - _weaponView.Barrel.position).normalized;
        }

        public void Shoot()
        {
            var ammo = _ammoPool.SpawnAmmo(WeaponType);
            ammo.Activate(AmmoBarrel);
            ammo.Rigidbody.AddForce(ShootDirection);
            RestoreCooldown();
        }

        public void ReduceCooldown(float deltaTime)
        {
            _cooldownTimer -= Math.Min(deltaTime, _cooldownTimer);
        }

        public void RestoreCooldown()
        {
            _cooldownTimer += _shootCooldown;
        }

        public void CleanUp()
        {
            Object.Destroy(_weaponView);
        }
    }
}