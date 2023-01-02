﻿using System;
using System.Threading.Tasks;
using Abstractions.Ships;
using Enums;
using Services;

namespace Ships
{
    public sealed class ShipModules : AbstractEquipments<IModule, ModuleType>, IShipModules
    {
        public event Action<IModule> OnModuleEquiped;
        public event Action<IModule> OnModuleUnequip;


        public ShipModules(int amount, IModuleFactory moduleFactory) : base(amount, moduleFactory) { }

        public override async Task SetEquipmentAsync(int index, ModuleType equipType)
        {
            await base.SetEquipmentAsync(index, equipType);
            Equipments[index].OnModuleUnequip += InvokeModuleUninstall;
            OnModuleEquiped?.Invoke(Equipments[index]);
        }

        private void InvokeModuleUninstall(IModule module)
        {
            module.OnModuleUnequip -= InvokeModuleUninstall;
            OnModuleUnequip?.Invoke(module);
        }
    }
}