using System;
using System.Collections.Generic;
using Abstractions.Ships;
using Infrastructure;

namespace Abstractions.Services
{
    internal interface IShipsInteractor : ICleaner
    {
        event Action<IShip> OnWinnerDefined;
        event Action<IAmmo> OnAmmoHit;
        
        Dictionary<IDamagableView, IShip> Ships { get; }

        void HandleAmmoHit(IAmmo ammo, IWeapon shooter, IDamagableView target);
        void AddShip(IShip ship, IDamagableView view);
    }
}