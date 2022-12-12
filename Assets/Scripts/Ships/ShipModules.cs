using System;
using Abstractions.Ships;
using Enums;
using Services;

namespace Ships
{
    internal class ShipModules : AbstractEquipments<IModule, ModuleType>, IShipModules
    {
        public event Action<IModule> OnModuleInstall;
        public event Action<IModule> OnModuleUninstall;


        public ShipModules(int amount, IModuleFactory moduleFactory)
        {
            MaxEquipmentsAmount = amount;
            EquipmentsFactory = moduleFactory;
        }

        public override void SetEquipment(int index, ModuleType equipType)
        {
            base.SetEquipment(index, equipType);
            Equipments[index].OnModuleUninstall += InvokeModuleUninstall;
            OnModuleInstall?.Invoke(Equipments[index]);
        }

        private void InvokeModuleUninstall(IModule module)
        {
            module.OnModuleUninstall -= InvokeModuleUninstall;
            OnModuleUninstall?.Invoke(module);
        }
    }
}