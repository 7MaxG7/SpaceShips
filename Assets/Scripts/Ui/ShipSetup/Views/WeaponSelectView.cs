using System.Collections.Generic;
using Configs.Data;
using Enums;

namespace Ui.ShipSetup
{
    public class WeaponSelectView : AbstractEquipmentSelectView<WeaponType>
    {
        public void SetupWeaponSelectPanel(IEnumerable<WeaponData> weaponDatas)
        {
            foreach (var data in weaponDatas)
            {
                var weaponSlot = AssetsProvider.CreateSelectEquipmentUiSlot(EquipmentsContent);
                weaponSlot.SetIcon(AssetsProvider.GetWeaponIcon(data.WeaponType));
                weaponSlot.SelectButton.onClick.AddListener(() => InvokeEquipmentSelect(data.WeaponType));
                EquipmentsSlots.Add(weaponSlot);
            }
            AjustSize();
        }
    }
}