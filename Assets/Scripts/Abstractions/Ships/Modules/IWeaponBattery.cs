using System;
using System.Threading.Tasks;
using Enums;
using Infrastructure;
using UnityEngine;

namespace Abstractions.Ships
{
    public interface IWeaponBattery : IAbstractEquipments<IWeapon, WeaponType>, IUpdatable
    {
        event Action<WeaponType> OnShoot;
        float ReloadRate { get; }

        void Init(IShip ship);
        Task SetEquipmentAsync(int index, WeaponType weaponType);
        void SetSlots(Transform[] weaponSlots);
        void ToggleShooting(bool isActive);
        void SetEquipmentSync(int index, WeaponType weaponType);
    }
}