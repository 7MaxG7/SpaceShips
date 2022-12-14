using Abstractions.Services;
using Abstractions.Ships;
using Enums;
using Ships.Modules;
using Zenject;

namespace Services
{
    internal class AmmoFactory : IAmmoFactory
    {
        private readonly IAssetsProvider _assetsProvider;


        [Inject]
        public AmmoFactory(IAssetsProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }
        
        
        public IAmmo CreateAmmo(WeaponType weaponType)
        {
            var ammoView = _assetsProvider.CreateAmmo(weaponType);
            return new Ammo(ammoView, weaponType);
        }
    }
}