using System.Collections.Generic;
using Configs.Data;
using Enums;

namespace Ui.ShipSetup
{
    public class ModuleSelectView : AbstractEquipmentSelectView<ModuleType>
    {
        public void SetupModuledSelectPanel(IEnumerable<ModuleData> modulesData)
        {
            Show();
            foreach (var data in modulesData)
            {
                var weaponSlot = AssetsProvider.CreateSelectEquipmentUiSlot(EquipmentsContent);
                weaponSlot.SetIcon(AssetsProvider.GetModuleIcon(data.ModuleType));
                weaponSlot.SelectButton.onClick.AddListener(() => InvokeEquipmentSelect(data.ModuleType));
                EquipmentsSlots.Add(weaponSlot);
            }
            AjustSize();
        }
    }
}