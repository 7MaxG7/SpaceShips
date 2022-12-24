using System;
using Enums;
using Infrastructure;
using Ships;

namespace Abstractions.Ships
{
    public interface IShip : ICleaner
    {
        event Action<IShip> OnDied;
        
        ShipType ShipType { get; }
        IHealth Health { get; }
        IWeaponBattery WeaponBattery { get; }
        IShipModules ShipModules { get; }
        string Name { get; }

        void SetView(ShipView shipView);
        void PrepareToBattle();
        void TakeDamage(int damage);
        void Kill();
        void CleanUpView();
    }
}