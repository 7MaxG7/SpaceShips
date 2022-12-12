using System;
using Enums;
using Infrastructure;
using UnityEngine;

namespace Abstractions.Ships
{
    internal interface IAmmo : ICleaner
    {
        event Action<IAmmo> OnTargetReached;
        
        WeaponType WeaponType { get; }
        Transform Transform { get; }
        Rigidbody2D Rigidbody { get; }

        void Activate(Transform source);
        void Deactivate();
    }
}