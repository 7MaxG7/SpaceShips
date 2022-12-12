using Abstractions.Services;
using Abstractions.Ships;
using Enums;
using Ships.Modules;
using Zenject;

namespace Services
{
    internal class AmmoFactory : IAmmoFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IAssetsProvider _assetsProvider;


        [Inject]
        public AmmoFactory(IStaticDataService staticDataService, IAssetsProvider assetsProvider)
        {
            _staticDataService = staticDataService;
            _assetsProvider = assetsProvider;
        }
        
        
        public IAmmo CreateAmmo(WeaponType weaponType)
        {
            var ammoData = _staticDataService.GetWeaponData(weaponType);
            var ammoView = _assetsProvider.CreateAmmo(weaponType);
            return new Ammo(ammoView, weaponType);
        }
    }
}