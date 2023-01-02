using System.Collections.Generic;
using System.Threading.Tasks;
using Configs.Data;
using Enums;
using Ships;

namespace Ui.ShipSetup
{
    public sealed class WeaponSelectPanelController : AbstractEquipmentSelectController<WeaponType>
    {
        public WeaponSelectPanelController(WeaponSelectView weaponSelectView, Dictionary<OpponentId,ShipModel> shipModels)
            : base(weaponSelectView, shipModels) { }

        public async Task SetupWeaponSelectPanelAsync(WeaponData[] weaponDatas)
        {
            foreach (var data in weaponDatas)
            {
                var button = await EquipmentSelectView.AddEquipmentSelectSlot(data.WeaponType);
                button.onClick.AddListener(() => SelectWeapon(data.WeaponType));
            }
            
            EquipmentSelectView.AjustSize();
        }

        private void SelectWeapon(WeaponType weaponType)
        {
            ShipModels[OpponentId].SetWeapon(SlotIndex, weaponType);
            Hide();
        }
    }
}