using System;
using System.Collections.Generic;
using Abstractions.Services;
using Abstractions.Ships;

namespace Services
{
    internal class ShipsInteractor : IShipsInteractor
    {
        public event Action<IShip> OnWinnerDefined;
        public event Action<IAmmo> OnAmmoHit;

        public Dictionary<IDamagableView, IShip> Ships { get; } = new();
        

        public void AddShip(IShip ship, IDamagableView view)
        {
            if (Ships.ContainsKey(view))
                return;

            ship.OnDied += DefineWinner;
            Ships.Add(view, ship);
        }

        private void DefineWinner(IShip looser)
        {
            IShip winner = null;
            foreach (var ship in Ships.Values)
            {
                if (ship == looser)
                {
                    ship.Kill();
                    continue;
                }
                winner = ship;
            }
            OnWinnerDefined?.Invoke(winner);
        }

        public void HandleAmmoHit(IAmmo ammo, IWeapon shooter, IDamagableView target)
        {
            if (!Ships.TryGetValue(target, out var targetShip))
                return;

            if (!shooter.TryDealDamage(targetShip))
                return;

            OnAmmoHit?.Invoke(ammo);
        }

        public void CleanUp()
        {
            foreach (var ship in Ships.Values) 
                ship.OnDied -= DefineWinner;
            Ships.Clear();
        }
    }
}