﻿using Abstractions.Services;

namespace Ships
{
    public sealed class WeaponBattery : AbstractWeaponBattery
    {
        public WeaponBattery(int amount, IWeaponFactory weaponFactory) : base(amount, weaponFactory)
        {
            ReloadRate = 1;
        }
    }
}