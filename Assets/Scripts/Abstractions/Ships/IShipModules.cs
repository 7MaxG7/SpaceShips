using System;
using Enums;
using UnityEngine;

namespace Abstractions.Ships
{
    internal interface IShipModules
    {
        event Action<IModule> OnModuleInstall;
        event Action<IModule> OnModuleUninstall;

        int MaxEquipmentsAmount { get; }

        void SetSlots(Transform[] moduleSlots);
        void SetEquipment(int slot, ModuleType moduleType);
        IModule GetEquipment(int index);
    }
}