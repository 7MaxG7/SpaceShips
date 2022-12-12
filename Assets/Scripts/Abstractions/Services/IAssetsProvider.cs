using Enums;
using Ships;
using Ships.Views;
using Sounds;
using Ui;
using Ui.ShipSetup;
using UnityEngine;

namespace Abstractions.Services
{
    internal interface IAssetsProvider
    {
        ShipSetupMenuView CreateShipSetupMenu();
        SoundPlayerView CreateSoundPlayer();
        ShipView CreateShip(ShipType shipType, Vector3 position, Quaternion rotation);
        AmmoView CreateAmmo(WeaponType weaponType);
        SlotUiView CreateComponentUiSlot(Transform parent);
        Sprite GetWeaponIcon(WeaponType weaponType);
        Sprite GetModuleIcon(ModuleType moduleType);
        WeaponView CreateWeapon(WeaponType weaponType, Transform parent);
        ModuleView CreateModule(ModuleType moduleType, Transform parent);
    }
}