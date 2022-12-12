using Enums;
using Infrastructure;
using UnityEngine;

namespace Abstractions.Ships
{
    internal interface IWeaponBattery : IUpdater
    {
        int MaxEquipmentsAmount { get; }
        
        IWeapon GetEquipment(int index);
        void SetEquipment(int index, WeaponType weaponType);
        void SetSlots(Transform[] weaponSlots);
    }
}