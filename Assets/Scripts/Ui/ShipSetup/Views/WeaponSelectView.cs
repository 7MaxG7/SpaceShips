using System.Threading.Tasks;
using Enums;

namespace Ui.ShipSetup
{
    public sealed class WeaponSelectView : AbstractEquipmentSelectView<WeaponType>
    {
        protected override async Task<SlotUiView> CreateSelectUiSlot(WeaponType weaponType) 
            => await UiFactory.CreateSelectWeaponUiSlotAsync(weaponType, EquipmentsContent);
    }
}