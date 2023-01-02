using System.Collections.Generic;
using System.Threading.Tasks;
using Configs.Data;
using Enums;
using Ships;

namespace Ui.ShipSetup
{
    public sealed class ModuleSelectPanelController : AbstractEquipmentSelectController<ModuleType>
    {
        public ModuleSelectPanelController(ModuleSelectView moduleSelectView, Dictionary<OpponentId, ShipModel> shipModels)
            : base(moduleSelectView, shipModels) { }

        public async Task SetupModuledSelectPanelAsync(ModuleData[] moduleDatas)
        {
            foreach (var data in moduleDatas)
            {
                var button = await EquipmentSelectView.AddEquipmentSelectSlot(data.ModuleType);
                button.onClick.AddListener(() => SelectModule(data.ModuleType));
            }
            
            EquipmentSelectView.AjustSize();
        }

        private void SelectModule(ModuleType moduleType)
        {
            ShipModels[OpponentId].SetModule(SlotIndex, moduleType);
            Hide();
        }
    }
}