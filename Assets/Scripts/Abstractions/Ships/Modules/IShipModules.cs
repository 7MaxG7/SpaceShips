using System;
using Enums;
using UnityEngine;

namespace Abstractions.Ships
{
    public interface IShipModules : IAbstractEquipments<IModule, ModuleType>
    {
        event Action<IModule> OnModuleEquiped;
        event Action<IModule> OnModuleUnequip;

        void SetSlots(Transform[] moduleSlots);
        void SetEquipment(int slot, ModuleType moduleType);
        IModule GetEquipment(int index);
        Transform GetSlotTransform(int slotIndex);
    }
}