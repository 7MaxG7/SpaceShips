using System;
using Abstractions.Services;
using Abstractions.Ships;
using Enums;
using Ships.Views;
using Object = UnityEngine.Object;

namespace Ships.Modules
{
    internal class Weapon : IWeapon
    {
        public WeaponType WeaponType { get; }
        public bool IsReady => _cooldownTimer <= 0;

        private WeaponView _weaponView;
        private float _cooldownTimer;
        private float _shootCooldown;
        private readonly int _damage;
        private readonly float _ammoSpeed;
        private readonly IAmmoPool _ammoPool;
        private IShip _owner;


        public Weapon(float cooldown, int damage, float ammoSpeed, WeaponType weaponType, IAmmoPool ammoPool)
        {
            _shootCooldown = cooldown;
            _damage = damage;
            _ammoSpeed = ammoSpeed;
            _ammoPool = ammoPool;
            WeaponType = weaponType;
        }

        public void Init(IShip owner)
        {
            _owner = owner;
        }

        public void SetView(WeaponView view)
            => _weaponView = view;

        public bool TryDealDamage(IShip target)
        {
            if (target == _owner)
                return false;

            target.TakeDamage(_damage);
            return true;
        }

        public void Reload() 
            => _cooldownTimer = 0;

        public void Shoot()
        {
            var ammo = _ammoPool.SpawnAmmo(WeaponType);
            ammo.Activate(_weaponView.Barrel, this);
            ammo.Rigidbody.AddForce(_weaponView.Barrel.up * _ammoSpeed);
            RestoreCooldown();
        }

        public void ReduceCooldown(float deltaTime)
            => _cooldownTimer -= Math.Min(deltaTime, _cooldownTimer);

        private void RestoreCooldown() 
            => _cooldownTimer += _shootCooldown;

        public void Unequip()
        {
            Object.Destroy(_weaponView.gameObject);
        }
    }
}