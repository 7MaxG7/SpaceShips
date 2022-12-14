using System;
using Enums;
using Infrastructure;
using UnityEngine;

namespace Abstractions.Ships
{
    public interface IWeaponBattery : IAbstractEquipments<IWeapon, WeaponType>, IUpdater
    {
        event Action<WeaponType> OnShoot;
        float ReloadRate { get; }

        void Init(IShip ship);
        IWeapon GetEquipment(int index);
        void SetEquipment(int index, WeaponType weaponType);
        void SetSlots(Transform[] weaponSlots);
        void ToggleShooting(bool isActive);
        Transform GetSlotTransform(int slotIndex);
    }
}