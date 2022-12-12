using UnityEngine;

namespace Abstractions.Ships
{
    internal interface IWeaponView
    {
        Transform Barrel { get; }
    }
}