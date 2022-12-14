﻿using System;
using System.Linq;
using Abstractions.Services;
using Abstractions.Ships;
using Enums;

namespace Ships
{
    internal abstract class AbstractWeaponBattery : AbstractEquipments<IWeapon, WeaponType>, IWeaponBattery
    {
        public event Action<WeaponType> OnShoot;

        public float ReloadRate { get; protected set; }

        private IShip _owner;
        private bool _isActive;


        protected AbstractWeaponBattery(IWeaponBattery baseWeaponBattery) : base(baseWeaponBattery) { }

        protected AbstractWeaponBattery(int amount, IWeaponFactory weaponFactory) : base(amount, weaponFactory) { }

        public void OnUpdate(float deltaTime)
        {
            if (!_isActive)
                return;

            var deltaCooldown = deltaTime / ReloadRate;
            foreach (var weapon in Equipments.Values.Where(weapon => !weapon.IsReady))
                weapon.ReduceCooldown(deltaCooldown);

            foreach (var weapon in Equipments.Values.Where(weapon => weapon.IsReady))
            {
                weapon.Shoot();
                OnShoot?.Invoke(weapon.WeaponType);
            }
        }
        
        public void Init(IShip owner)
        {
            _owner = owner;
        }

        public override void SetEquipment(int index, WeaponType equipType)
        {
            base.SetEquipment(index, equipType);
            Equipments[index].Init(_owner);
        }

        public void ToggleShooting(bool isActive)
        {
            _isActive = isActive;
            foreach (var weapon in Equipments.Values)
                weapon.Reload();
        }
    }
}