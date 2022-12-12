using Enums;
using Infrastructure;
using UnityEngine;

namespace Abstractions.Ships
{
    public interface IWeapon : ICleaner
    {
        bool IsReady { get; }
        WeaponType WeaponType { get; }
        Transform AmmoBarrel { get; }
        Vector2 ShootDirection { get; }

        void Shoot();
        void ReduceCooldown(float deltaTime);
        void CleanUp();
    }
}