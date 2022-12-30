using System.Collections.Generic;
using System.Linq;
using Abstractions.Services;
using Abstractions.Ships;
using Enums;
using Infrastructure;
using Zenject;

namespace Services
{
    internal class AmmoPool : IAmmoPool
    {
        private readonly Dictionary<WeaponType, Stack<IAmmo>> _ammos = new();
        private readonly List<IAmmo> _spawnedAmmos = new();
        private readonly IAmmoFactory _ammoFactory;
        private readonly IShipsInteractor _shipsInteractor;

        private bool _isCleaned;


        [Inject]
        public AmmoPool(IAmmoFactory ammoFactory, IShipsInteractor shipsInteractor, ICleaner cleaner)
        {
            _ammoFactory = ammoFactory;
            _shipsInteractor = shipsInteractor;
            cleaner.AddCleanable(this);
        }

        public void Init()
        {
            _shipsInteractor.OnAmmoHit += ReturnObject;
            _isCleaned = false;
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
                ammo.OnReachedDamagable += _shipsInteractor.HandleAmmoHit;
            }
            else
                ammo = ammos.Pop();

            _spawnedAmmos.Add(ammo);
            return ammo;
        }

        public void CleanUp()
        {
            if (_isCleaned)
                return;
            
            _isCleaned = true;
            _shipsInteractor.OnAmmoHit -= ReturnObject;
            foreach (var ammo in _spawnedAmmos)
            {
                ammo.OnReachedDamagable -= _shipsInteractor.HandleAmmoHit;
                ammo.CleanUp();
            }
            foreach (var ammo in _ammos.SelectMany(ammos => ammos.Value))
            {
                ammo.OnReachedDamagable -= _shipsInteractor.HandleAmmoHit;
                ammo.CleanUp();
            }
            _spawnedAmmos.Clear();
            foreach (var stack in _ammos.Values)
            {
                stack.Clear();
            }
            _ammos.Clear();
        }

        private void ReturnObject(IAmmo ammo)
        {
            _spawnedAmmos.Remove(ammo);
            ammo.Deactivate();
            _ammos[ammo.WeaponType].Push(ammo);
        }
    }
}