using System;
using Abstractions.Ships;
using Enums;
using Services;

namespace Ships
{
    internal class ShipModules : AbstractEquipments<IModule, ModuleType>, IShipModules
    {
        public event Action<IModule> OnModuleEquiped;
        public event Action<IModule> OnModuleUnequip;


        public ShipModules(int amount, IModuleFactory moduleFactory) : base(amount, moduleFactory) { }

        public override void SetEquipment(int index, ModuleType equipType)
        {
            base.SetEquipment(index, equipType);
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