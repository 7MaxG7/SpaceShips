using System;
using System.Threading.Tasks;
using Enums;
using UnityEngine;

namespace Abstractions.Ships
{
    public interface IShipModules : IAbstractEquipments<IModule, ModuleType>
    {
        event Action<IModule> OnModuleEquiped;
        event Action<IModule> OnModuleUnequip;

        void SetSlots(Transform[] moduleSlots);
        Task SetEquipmentAsync(int slot, ModuleType moduleType);
        void SetEquipmentSync(int slot, ModuleType moduleType);
    }
}