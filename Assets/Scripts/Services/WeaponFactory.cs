using Abstractions.Services;
using Abstractions.Ships;
using Enums;
using Ships.Modules;
using UnityEngine;
using Zenject;

namespace Services
{
    internal class WeaponFactory : IWeaponFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IAssetsProvider _assetsProvider;
        private readonly IAmmoPool _ammoPool;

        [Inject]
        public WeaponFactory(IStaticDataService staticDataService, IAssetsProvider assetsProvider, IAmmoPool ammoPool)
        {
            _staticDataService = staticDataService;
            _assetsProvider = assetsProvider;
            _ammoPool = ammoPool;
        }
        
        public IWeapon CreateEquipment(WeaponType weaponType, Transform parent)
        {
            var weaponData = _staticDataService.GetWeaponData(weaponType);
            var weaponView = _assetsProvider.CreateWeapon(weaponType, parent);
            return new Weapon(weaponView, weaponData.Cooldown, weaponData.Damage, weaponType, _ammoPool);
        }
    }
}