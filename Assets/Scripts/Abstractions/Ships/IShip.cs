using System;
using Enums;
using Infrastructure;
using Ships;

namespace Abstractions.Ships
{
    internal interface IShip : ICleaner
    {
        event Action<IShip> OnDied;
        
        ShipType ShipType { get; }
        IHealth Health { get; }
        IWeaponBattery WeaponBattery { get; }
        IShipModules ShipModules { get; }

        void SetView(ShipView shipView);
    }
}