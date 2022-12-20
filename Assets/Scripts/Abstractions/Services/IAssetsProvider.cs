using Enums;
using Ships;
using Ships.Views;
using Sounds;
using Ui;
using Ui.ShipSetup;
using UnityEngine;

namespace Abstractions.Services
{
    public interface IAssetsProvider
    {
        CurtainView CreateCurtain();
        void PrepareSetupShipRoots();
        void PrepareBattleRoots();
        ShipSetupMenuView CreateShipSetupMenu();
        BattleUiView CreateBattleUi();
        SoundPlayerView CreateSoundPlayer();
        ShipView CreateShip(ShipType shipType, Vector3 position, Quaternion rotation);
        AmmoView CreateAmmo(WeaponType weaponType);
        SlotUiView CreateSelectEquipmentUiSlot(Transform equipmentsContent);
        ShipSlotUiView CreateEquipmentUiSlot(Transform parent);
        Sprite GetWeaponIcon(WeaponType weaponType);
        Sprite GetModuleIcon(ModuleType moduleType);
        WeaponView CreateWeapon(WeaponType weaponType, Transform parent);
        ModuleView CreateModule(ModuleType moduleType, Transform parent);
    }
}