using System.Collections.Generic;
using Abstractions.Services;
using Abstractions.Ships;
using Enums;
using Zenject;

namespace Services
{
    internal class AmmoPool : IAmmoPool
    {
        private readonly Dictionary<WeaponType, Stack<IAmmo>> _ammos = new();
        private readonly List<IAmmo> _spawnedAmmos = new();
        private readonly IAmmoFactory _ammoFactory;


        [Inject]
        public AmmoPool(IAmmoFactory ammoFactory)
        {
            _ammoFactory = ammoFactory;
        }

        public IAmmo SpawnAmmo(WeaponType weaponType)
        {
            IAmmo ammo;
            if (!_ammos.ContainsKey(weaponType))
                _ammos.Add(weaponType, new Stack<IAmmo>());

            var ammos = _ammos[weaponType];
            if (ammos.Count == 0)
            {
                ammo = _ammoFactory.CreateAmmo(weaponType);
                ammo.OnTargetReached += ReturnObject;
            }
            else
            {
                ammo = ammos.Pop();
            }

            _spawnedAmmos.Add(ammo);
            return ammo;
        }

        private void ReturnObject(IAmmo ammo)
        {
            _spawnedAmmos.Remove(ammo);
            ammo.Deactivate();
            _ammos[ammo.WeaponType].Push(ammo);
        }
    }
}