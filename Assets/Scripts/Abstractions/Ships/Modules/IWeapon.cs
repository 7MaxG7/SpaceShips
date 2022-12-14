using Enums;
using Ships.Views;

namespace Abstractions.Ships
{
    public interface IWeapon : IEquipment
    {
        bool IsReady { get; }
        WeaponType WeaponType { get; }

        void Init(IShip owner);
        void Shoot();
        void ReduceCooldown(float deltaTime);
        void SetView(WeaponView view);
        bool TryDealDamage(IShip target);
        void Reload();
    }
}