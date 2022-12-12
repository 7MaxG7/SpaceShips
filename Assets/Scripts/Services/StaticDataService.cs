using System.Collections.Generic;
using System.Linq;
using Abstractions.Services;
using Configs.Data;
using Enums;
using Ships;
using Ships.Views;
using UnityEngine;
using Utils;

namespace Services
{
    internal class StaticDataService : IStaticDataService
    {
        private Dictionary<ShipType, ShipData> _shipDatas;
        private Dictionary<WeaponType,WeaponData> _weaponDatas;
        private Dictionary<ModuleType,ModuleData> _moduleDatas;


        public void Init()
        {
            _shipDatas = Resources
                .LoadAll<ShipData>(Constants.SHIP_DATA_PATH)
                .ToDictionary(data => data.ShipType, data => data);
            _weaponDatas = Resources
                .LoadAll<WeaponData>(Constants.WEAPON_DATA_PATH)
                .ToDictionary(data => data.Weapon, data => data);
            _moduleDatas = Resources
                .LoadAll<ModuleData>(Constants.MODULE_DATA_PATH)
                .ToDictionary(data => data.ModuleType, data => data);
        }

        public ShipView GetShipPrefab(ShipType shipType)
        {
            return GetShipData(shipType).Prefab;
        }

        public AmmoView GetAmmoPrefab(WeaponType weaponType)
        {
            return GetWeaponData(weaponType).AmmoPrefab;
        }

        public ShipData GetShipData(ShipType shipType)
        {
            return _shipDatas.TryGetValue(shipType, out var data)
                ? data
                : null;
        }

        public WeaponData GetWeaponData(WeaponType weapon)
        {
            return _weaponDatas.TryGetValue(weapon, out var data)
                ? data
                : null;
        }

        public ModuleData GetModuleData(ModuleType module)
        {
            return _moduleDatas.TryGetValue(module, out var data)
                ? data
                : null;
        }
    }
}