using System.Threading.Tasks;
using Enums;
using Ui;
using Ui.Battle;
using Ui.ShipSetup;
using Ui.ShipSetup.Controllers;
using UnityEngine;

namespace Abstractions.Services
{
    public interface IUiFactory
    {
        Task PrepareCanvasAsync();
        Task<CurtainView> CreateCurtainAsync();
        Task<ShipSetupMenuController> CreateShipSetupMenuAsync();
        Task<BattleUiController> CreateBattleUiAsync();
        Task<SlotUiView> CreateSelectWeaponUiSlotAsync(WeaponType weaponType, Transform parent);
        Task<SlotUiView> CreateSelectModuleUiSlotAsync(ModuleType moduleType, Transform parent);
        Task<ShipSlotUiView> CreateEquipmentUiSlotAsync(Transform parent);
    }
}