using System;
using Enums;
using Infrastructure;
using UnityEngine;

namespace Abstractions.Ships
{
    public interface IAmmo : ICleaner
    {
        event Action<IAmmo, IWeapon, IDamagableView> OnReachedDamagable;
        
        WeaponType WeaponType { get; }
        Rigidbody2D Rigidbody { get; }

        void Activate(Transform startPosition, IWeapon shooter);
        void Deactivate();
    }
}